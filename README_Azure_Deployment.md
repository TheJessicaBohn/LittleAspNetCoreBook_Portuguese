# Deploy 
- Os aplicativos ASP.NET Core podem ser executados no Windows, Mac ou Linux, há várias maneiras diferentes de implantar sua aplicação. 

## Opções de implantação

Os aplicativos ASP.NET Core são normalmente implantados em um destes ambientes:

* ** Um host Docker **. Qualquer máquina capaz de hospedar contêineres Docker pode ser usada para hospedar um aplicativo ASP.NET Core. Criar uma imagem Docker é uma maneira muito rápida de implantar seu aplicativo, especialmente se você estiver familiarizado com o Docker. (Se não estiver, não se preocupe! Abordarei as etapas mais tarde.)

* ** Azure **. O Microsoft Azure tem suporte nativo para aplicativos ASP.NET Core. Se você tiver uma assinatura do Azure, precisará apenas criar um Web App e carregar seus arquivos de projeto. Abordarei como fazer isso com a CLI do Azure na próxima seção.

* ** Linux (com Nginx) **. Se você não quiser seguir a rota do Docker, ainda pode hospedar seu aplicativo em qualquer servidor Linux (isso inclui Amazon EC2 e máquinas virtuais DigitalOcean). É comum emparelhar o ASP.NET Core com o proxy reverso Nginx. (Mais sobre o Nginx abaixo.)

* **Janelas**. Você pode usar o servidor Web IIS no Windows para hospedar aplicativos ASP.NET Core. Geralmente é mais fácil (e mais barato) apenas implantar no Azure, mas se você preferir gerenciar servidores Windows por conta própria, funcionará perfeitamente.

## Kestrel e proxies reversos

- O ASP.NET Core inclui um servidor da Web rápido e leve chamado Kestrel. É o servidor que você está usando toda vez que executa `dotnet run` e acessa` http: // localhost: 5000`. Quando você implanta seu aplicativo em um ambiente de produção, ele ainda usa o Kestrel nos bastidores. No entanto, é recomendável que você coloque um proxy reverso na frente do Kestrel, porque o Kestrel ainda não tem balanceamento de carga e outros recursos que os servidores da web mais maduros têm.
- No Linux (e em contêineres Docker), você pode usar o Nginx ou o servidor da web Apache para receber solicitações de entrada da Internet e encaminhá-las para seu aplicativo hospedado com Kestrel. Se você estiver no Windows, o IIS faz a mesma coisa.
- Se você estiver usando o Azure para hospedar seu aplicativo, tudo isso será feito automaticamente. 

## Implementar no Azure

A implantação de seu aplicativo ASP.NET Core no Azure leva apenas algumas etapas. Você pode fazer isso por meio do portal da web do Azure ou na linha de comando usando a CLI do Azure. Vou cobrir o último.

### O que você precisará

* Git (use `git --version` para se certificar de que está instalado)
* A CLI do Azure (siga as instruções de instalação em https://github.com/Azure/azure-cli)
* Uma assinatura do Azure (a assinatura gratuita é adequada)
* Um arquivo de configuração de implantação na raiz do seu projeto

### Crie um arquivo de configuração de implantação

Como há vários projetos em sua estrutura de diretório (o aplicativo Web e dois projetos de teste), o Azure não saberá qual deles publicar. Para corrigir isso, crie um arquivo chamado `.deployment` bem no topo da sua estrutura de diretório:

**.desdobramento, desenvolvimento**
```ini
[config]
project = AspNetCoreTodo/AspNetCoreTodo.csproj
```
- Certifique-se de salvar o arquivo como `.deployment` sem outras partes do nome. (No Windows, você pode precisar colocar o nome do arquivo entre aspas, como `" .deployment "`, para evitar que uma extensão `.txt` seja adicionada.)
- Se você `ls` ou` dir` em seu diretório de nível superior, deverá ver estes itens:
```
.deployment
AspNetCoreTodo
AspNetCoreTodo.IntegrationTests
AspNetCoreTodo.UnitTests
```
### Configurando os recursos do Azure

- Se você acabou de instalar a CLI do Azure pela primeira vez, execute `az login`
- e siga as instruções para fazer login em sua máquina. Em seguida, crie um novo Grupo de Recursos para este aplicativo: `az group create -l westus -n AspNetCoreTodoGroup`

- Esse comando cria um Grupo de Recursos na região Oeste dos EUA. 
- Como estamos no Brasil vamos precisar `az account list-locations` para obter uma lista de locais e encontrar um mais perto.
- Em seguida, crie um plano de serviço de aplicativo no grupo que você acabou de criar: `az appservice plan create -g AspNetCoreTodoGroup -n AspNetCoreTodoPlan --sku F1`

> F1 é o plano de aplicativo gratuito. Se você quiser usar um nome de domínio personalizado com seu aplicativo, use o plano D1 ($ 10 / mês) ou superior.

- Agora crie um aplicativo da Web no plano de serviço de aplicativo: `az webapp create -g AspNetCoreTodoGroup -p AspNetCoreTodoPlan -n MyTodoApp`
- O nome do aplicativo (`MyTodoApp` acima) deve ser globalmente exclusivo no Azure. Assim que o aplicativo for criado, ele terá um URL padrão no formato: http://mytodoapp.azurewebsites.net

### Implante seus arquivos de projeto no Azure

- Você pode usar o Git para enviar seus arquivos de aplicativo para o aplicativo Web do Azure. Se seu diretório local ainda não for rastreado como um repositório Git, execute estes comandos para configurá-lo:
```
git init
git add .
git commit -m "First commit!"
```
- Em seguida, crie um nome de usuário e senha do Azure para implantação: `az webapp deployment user set --user-name nate`
- Agora siga as instruções para criar uma senha. Em seguida, use `config-local-git` para jogar um URL Git: 
```
az webapp deployment source config-local-git -g AspNetCoreTodoGroup -n MyTodoApp --out tsv

https://nate@mytodoapp.scm.azurewebsites.net/MyTodoApp.git
```
- Copie o URL para a área de transferência e use-o para adicionar um remoto Git ao seu repositório local: `git remote add azure <paste>`
- Você só precisa executar essas etapas uma vez. Agora, sempre que você quiser enviar seus arquivos de aplicativo para o Azure, faça check-in com Git e execute: `git push azure master`
- Será visto um fluxo de mensagens de log conforme o aplicativo é implantado no Azure.

Quando estiver concluído, navegue até http://yourappname.azurewebsites.net para verificar o aplicativo!
