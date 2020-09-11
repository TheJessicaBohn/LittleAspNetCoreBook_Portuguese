## Implementando com Docker

- Se você não estiver usando uma plataforma como o Azure, as tecnologias de conteinerização como o Docker podem facilitar a implantação de aplicativos da web em seus próprios servidores. Em vez de gastar tempo configurando um servidor com as dependências de que ele precisa para executar seu aplicativo, copiando arquivos e reiniciando processos, você pode simplesmente criar uma imagem Docker que descreve tudo que seu aplicativo precisa para ser executado e girá-lo como um contêiner em qualquer Docker hospedeiro.
- O Docker também pode tornar o escalonamento do seu aplicativo em vários servidores mais fácil. Depois de obter uma imagem, usá-la para criar 1 contêiner é o mesmo processo que criar 100 contêineres.
- Antes de começar, você precisa do Docker CLI instalado em sua máquina de desenvolvimento. Pesquise "get docker for (mac / windows / linux)" e siga as instruções no site oficial do Docker. Você pode verificar se ele está instalado corretamente com`docker version`.

### Adicionar um Dockerfile

- A primeira coisa de que você precisa é um Dockerfile, que é como uma receita que diz ao Docker o que seu aplicativo precisa para construir e executar.
- Crie um arquivo chamado `Dockerfile` (sem extensão) na raiz da pasta` AspNetCoreTodo` de nível superior. Abra-o em seu editor favorito. Escreva a seguinte linha:

```dockerfile
FROM microsoft/dotnet:2.0-sdk AS build
```

### Adicionar um Dockerfile

- Um Dockerfile, que é como uma receita que diz ao Docker o que seu aplicativo precisa para construir e executar.
- Então vamos criar um um arquivo chamado `Dockerfile` (sem extensão) na raiz da pasta` AspNetCoreTodo` de nível superior. Abra-o em seu editor favorito. Escreva a seguinte linha:

```dockerfile
COPY AspNetCoreTodo/*.csproj ./app/AspNetCoreTodo/
```
O comando `COPY` copia o arquivo do projeto` .csproj` para a imagem no caminho `/ app / AspNetCoreTodo /`. Observe que nenhum código real (arquivos `.cs`) foi copiado para a imagem ainda. 

```dockerfile
WORKDIR /app/AspNetCoreTodo
RUN dotnet restore
```

- O comando`WORKDIR` é o equivalente do Docker a` cd`. Isso significa que qualquer comando executado a seguir será executado de dentro do diretório `/ app / AspNetCoreTodo` que o comando` COPY` criou na última etapa.
- Executar o comando `dotnet restore` restaura os pacotes NuGet de que o aplicativo precisa, definidos no arquivo` .csproj`. Ao restaurar pacotes dentro da imagem **before** de adicionar o resto do código, o Docker pode armazenar em cache os pacotes restaurados. 
- Então, se você fizer alterações no código (mas não alterar os pacotes definidos no arquivo de projeto), reconstruir a imagem do Docker será muito rápido.

Agora é hora de copiar o resto do código e compilar o aplicativo:

```dockerfile
COPY AspNetCoreTodo/. ./AspNetCoreTodo/
RUN dotnet publish -o out /p:PublishWithAspNetCoreTargetManifest="false"
```
- O comando `dotnet publish` compila o projeto, e o sinalizador` -o out` coloca os arquivos compilados em um diretório chamado `out`.
- Esses arquivos compilados serão usados ​​para executar o aplicativo com os poucos comandos finais:
```dockerfile
FROM microsoft/dotnet:2.0-runtime AS runtime
ENV ASPNETCORE_URLS http://+:80
WORKDIR /app
COPY --from=build /app/AspNetCoreTodo/out ./
ENTRYPOINT ["dotnet", "AspNetCoreTodo.dll"]
```

- O comando `FROM` é usado novamente para selecionar uma imagem menor que possui apenas as dependências necessárias para executar o aplicativo. O comando `ENV` é usado para definir variáveis ​​de ambiente no contêiner, e a variável de ambiente` ASPNETCORE_URLS` informa ao ASP.NET Core a qual interface de rede e porta ele deve se ligar (neste caso, porta 80).
- E o comando `ENTRYPOINT` permite que o Docker saiba que o contêiner deve ser iniciado como um executável executando` dotnet AspNetCoreTodo.dll`. Isso diz ao `dotnet` para iniciar sua aplicação a partir do arquivo compilado criado pelo` dotnet publish` anteriormente. (Quando você executa `dotnet run` durante o desenvolvimento, está realizando a mesma coisa em uma única etapa.)

O Dockerfile completo tem a seguinte aparência no **Dockerfile**:

```dockerfile
FROM microsoft/dotnet:2.0-sdk AS build
COPY AspNetCoreTodo/*.csproj ./app/AspNetCoreTodo/
WORKDIR /app/AspNetCoreTodo
RUN dotnet restore

COPY AspNetCoreTodo/. ./
RUN dotnet publish -o out /p:PublishWithAspNetCoreTargetManifest="false"

FROM microsoft/dotnet:2.0-runtime AS runtime
ENV ASPNETCORE_URLS http://+:80
WORKDIR /app
COPY --from=build /app/AspNetCoreTodo/out ./
ENTRYPOINT ["dotnet", "AspNetCoreTodo.dll"]
```
### Criando uma imagem
- Certifique-se de que o Dockerfile esteja salvo e, em seguida, use o `docker build` para criar uma imagem:`docker build -t aspnetcoretodo .`
Não perca o período final! Isso diz ao Docker para procurar um Dockerfile no diretório atual.
- Depois que a imagem é criada, você pode executar `docker images` para listar todas as imagens disponíveis em sua máquina local. Para testá-lo em um contêiner, execute
`docker run --name aspnetcoretodo_sample --rm -it -p 8080:80 aspnetcoretodo`
- O sinalizador `-it` diz ao Docker para executar o contêiner no modo interativo (enviando para o terminal, em vez de rodar em segundo plano). Quando você quiser parar o recipiente, pressione Control-C.
- Lembra da variável `ASPNETCORE_URLS` que disse ao ASP.NET Core para escutar na porta 80? A opção `-p 8080: 80` diz ao Docker para mapear a porta 8080 em * sua * máquina para a porta 80 do * contêiner *. Abra seu navegador e navegue até http: // localhost: 8080 para ver o aplicativo em execução no contêiner !

### Configurando o Nginx

- Será preciso usar um proxy reverso como o Nginx para enviar solicitações ao Kestrel. Você também pode usar o Docker para isso.
- A arquitetura geral consistirá em dois contêineres: um contêiner Nginx ouvindo na porta 80, encaminhando solicitações para o contêiner que você acabou de construir e que hospeda seu aplicativo com Kestrel.
- O contêiner Nginx precisa de seu próprio Dockerfile. Para evitar que ele entre em conflito com o Dockerfile que você acabou de criar, crie um novo diretório na raiz do aplicativo da web:`mkdir nginx`
- Crie um novo Dockerfile e adicione estas linhas em **nginx/Dockerfile**:

```dockerfile
FROM nginx
COPY nginx.conf /etc/nginx/nginx.conf
```
- Em seguida, crie um arquivo `nginx.conf` em **nginx/nginx.conf**:

```
events { worker_connections 1024; }

http {
    server {
        listen 80;
        location / {
          proxy_pass http://kestrel:80;
          proxy_http_version 1.1;
          proxy_set_header Upgrade $http_upgrade;
          proxy_set_header Connection 'keep-alive';
          proxy_set_header Host $host;
          proxy_cache_bypass $http_upgrade;
        }
    }
}
```
- Este arquivo de configuração diz ao Nginx para proxy de solicitações de entrada para `http: // kestrel: 80`. (Você verá por que `kestrel` funcionará como um nome de host em instantes.)
- Quando você implanta sua aplicação em um ambiente de produção, você deve adicionar a diretiva `server_name` e validar e restringir o cabeçalho do host para valores bons conhecidos. 
- Para enteder mais, veja em:  https://github.com/aspnet/Announcements/issues/295

### Configurar Docker Compose

Há mais um arquivo para criar. No diretório raiz, crie `docker-compose.yml` em **docker-compose.yml **:

```yaml
nginx:
    build: ./nginx
    links:
        - kestrel:kestrel
    ports:
        - "80:80"
kestrel:
    build: .
    ports:
        - "80"
```

- O Docker Compose é uma ferramenta que ajuda a criar e executar aplicativos de vários contêineres. Este arquivo de configuração define dois contêineres: `nginx` da receita`. / Nginx / Dockerfile` e `kestrel` da receita`. / Dockerfile`. Os contêineres são explicitamente vinculados para que possam se comunicar.
- Você pode tentar ativar todo o aplicativo de vários contêineres executando:
```
docker-compose up
```
Tente abrir um navegador e navegar para http: // localhost (porta 80, não 8080!). O Nginx está escutando na porta 80 (a porta HTTP padrão) e fazendo proxy de solicitações para seu aplicativo ASP.NET Core hospedado pelo Kestrel.

### Dicas Finais : Configurando um servidor Docker

- As instruções de configuração específicas estão fora do escopo deste livro, mas qualquer sabor moderno de Linux (como o Ubuntu) pode ser usado para configurar um host Docker. Por exemplo, você pode criar uma máquina virtual com Amazon EC2 e instalar o serviço Docker. Você pode pesquisar por "amazon ec2 set up docker" (por exemplo) para obter instruções.
- O autor intica o DigitalOcean porque eles tornaram muito fácil começar. O DigitalOcean tem uma máquina virtual Docker pré-construída e tutoriais detalhados para colocar o Docker em funcionamento (pesquise por "docker digitalocean").
