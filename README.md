# LittleAspNetCoreBook_Portuguese 
- Esse repositório visa seguir as lições do Little Asp Net Core Book disponível em : https://s3.amazonaws.com/recaffeinate-files/LittleAspNetCoreBook.pdf , em portugues, para contribuir com a comunidade brasileira; 
- ## O que você vai aprender a criar com as lições do livro de acordo com o prórpio livro :
  - Um servidor de aplicativos da web (às vezes chamado de "back-end") usando o ASP.NET Core, C # e o padrão MVC;
  - Um banco de dados para armazenar itens de tarefas do usuário usando o mecanismo de banco de dados SQLite e um sistema chamado Entity Framework Core;
  - Páginas da Web e uma interface com a qual o usuário irá interagir via navegador, usando HTML, CSS e JavaScript (chamado de "front-end");
  - Um formulário de login e verificações de segurança para manter a lista de tarefas de cada usuário.
  
- Tudo o que estiver dentro de 'aspas simples' são comandos a serem digitados nos terminais sem as aspas;
- ## Dicas: 
  - Após as palavras chaves das linhas de comando (cd, ls, mkdir, etc), pode-se digitar a primeira letra da pasta ou arquivo e a tecla Tab da o autocompletar;
  - Se você está utilizando o Visual Studio Code : e for abrir a raiz pasta que está o projeto e for solicitado a instalação de arquivos ausentes, clique em sim;
  - F5 para executar e breakpoints: no projeto aberto, clique na margem a esquerda do projeto ao lado da linha de código que se deseja analizar e pressione F5 para executar o projeto no modo de depuração;
  - Lâmpada :bulb:: se o seu código contiver riscos vermelhos, isso indica erros no compilador, entao pause o mouse sobre código vermelho e procure o ícone da lâmpada na margem esquerda e veja o que o menu da lâmpada sugere;
  - Compilação rápida: Command-Shift-B ou Control-Shift-B para executar a tarefa Build, que faz o mesmo que dotnetbuild.
  -  A documentação oficial do ASP.NET Core se encontra em (athttps: //docs.asp.net).
- Dowloads :  
  - Editor utilizado (dica, pode ser outro de sua prefência ou o VS mesmo) :Visual Studio Community  a partir do 2017 version 15.3 or mais atualizada https://visualstudio.microsoft.com/pt-br/vs/community/  ou  VSCode https://code.visualstudio.com/download, e na hora de instalar colocar as opções de .net e C#;
  - .NET Core SDK, incluindo o runtime https://dotnet.microsoft.com/download/dotnet-core?utm_source=getdotnetcorecli&utm_medium=referral;
- Após as instalações verifique por 'dotnet --version' e 'dotnet --info' no seu terminal ou power shell se ele foi instalado corretamente, se ele não retornar nenum erro ou comando desconhecido, está ok;
- Git Bash caso utilize o github https://git-scm.com/downloads, e depois nas variaveis de ambiente e na variavel Path adicione  C:\Program Files\Git\bin\ e  C:\Program Files\Git\cmd\
- ## Comandos : Criando um Hello World
  - 'cd Documents' (no lugar de Documents a pasta que você deseja cria);
  - 'dotnet new console -o CsharpHelloWorld' , note que ele cria uma pasta de mesmo nome
  - 'cd CsharpHelloWorld',  isso dentro da pasta repare que ele cria um arquivo .csproj e um .cs, se caso não estiver dentro de um editor de o comando 'ls' para visualizar;
  - 'dotnet run' lembrando que você de estar exatamente dentro da pasta do projeto. No terminal deve retornar Hello Word!.
- ## Comandos : Criando um ASP.NET Core project
  - 'cd ..' para sair da pasta do CsharpHelloWorld;
  - 'mkdir AspNetCoreTodo', cria uma nova pasta onde após o mkdir é o nome e pode-se criar com outro nome de sua preferência;
  - 'cd AspNetCoreTodo' Entra na respectiva pasta, lembrando que, se o nome da mesma foi modificado, deve-se escrever o nome escolhido após o comando cd;
  - 'dotnet new mvc --auth Individual -o AspNetCoreTodo', para criar um projeto com comando MVC;
  - 'cd AspNetCoreTodo' para entrar na nova pasta criada;
  - 'dotnet run' executa o programa e deve retornar:
     
      **info: Microsoft.Hosting.Lifetime[0]<br />**
      **Now listening on: https://localhost:5001<br />**
      **info: Microsoft.Hosting.Lifetime[0]<br />**
      **Now listening on: http://localhost:5000<br />**
      **info: Microsoft.Hosting.Lifetime[0]<br />**
      **Application started. Press Ctrl+C to shut down.<br />**
      **info: Microsoft.Hosting.Lifetime[0]<br />**
      **Hosting environment: Development<br />**
      **info: Microsoft.Hosting.Lifetime[0]<br />**
      **Content root path: C:\Users\xxxx\Desktop\littleAspNet\AspNetCoreTodo\AspNetCoreTodo<br />**
   
   e após isso abra o navegado em http://localhost:5000, e ele deve aparecer a pagina com " Welcome Learn about building Web apps with ASP.NET Core." , para parar o serviço 'Ctrl + C';
 - Conteúdo : ASP.NET Core project
   - **Program.cs** e **Startup.cs** são classes que configuram o servidor web e ASP.NET Core pipeline;
   - **Models, Views**, e **Controllers** são diretórios contêm os componentes da arquitetura Model-View-Controller (MVC);
   - **wwwroot** contém ativos estáticos que podem ser agrupados e compactados automaticamente, como CSS, JavaScript e arquivos de imagem;
   - **appsettings.json** contém as configuração de inicialização que o ASP.NETCore carrega; 

- ## Basico de MVC :

  **Por padrão o funcionamento dos elementos ocorre da seguinte forma:**<br />
  - O **Controller**:
    - Recebe uma solicitação e consulta algumas informações no banco de dados,
    - Cria um modelo com as informações e o anexa a uma view;
    - A **View** é renderizada e exibida no navegador do usuário;
    - Então o usuário clica em um botão ou envia um formulário, que envia uma nova solicitação ao controlador e o ciclo se repete;

- ## Continuando o projeto ASP.NET Core : Controller
  **Agora que sabemos o que é um controller vamos contruir um:**<br />
  - Se abrirmos a pasta Controllers, veremos que ja existe um HomeController.cs que inclui três métodos de ação (Index, About, e Contact) que são mapeados pelo ASP.NET Core para esses URLs de rota;
  - Pelo VS Code clicando na pasta, você pode criar um "new file" chamado TodoController não se esqueça da exentensão .cs;
  - E escreva o seguinte código:
  
 ```
 using System;
 using System.Collections.Generic;
 using System.Linq;using System.Threading.Tasks;
 using Microsoft.AspNetCore.Mvc;

  namespace AspNetCoreTodo.Controllers
  {
    public class TodoController : Controller    
    {
    // Actions go here
     }
  }
  ```
   - Substitua "//Actions " go here por 
  ```
  public IActionResult Index()
    {// Get to-do items from database
    // Put items into a model
    // Render view using the model    }
     }
  ```
  - O objetivo do método IActionResult é retornar códigos de status HTTP como 200 (OK) e 404 (não encontrado), views, ou dados JSON.

- **O Model**:
  - Vamos criar um modelo que represente um item de tarefa pendente armazenado no banco de dados (às vezes chamado de entidade) e o modelo que será combinado com uma visualização (o MV no MVC) e enviado de volta ao navegador do usuário. 
  - Primeiro criamos uma Classe chamada TodoItem.cs, dentro da pasta Models;
  - Ela define o que o banco de dados precisará armazenar para cada item de tarefa: 
    - Um ID : um guid ou um identificador globalmente exclusivos e são gerados aleatoriamente, para que você não precise se preocupar com o incremento automático
    - Um título ou nome : valor do texto). Isso conterá a descrição do nome do item de pendências. O atributo [Required] informa que o campo é obrigatório.
    - IsDone : valor booleano. 
    - DueAt: informa se o item está completo e qual é a data do vencimento.
    - Agora não importa qual seja a tecnologia de banco de dados subjacente. Pode ser SQL Server, MySQL, MongoDB, Redis ou algo mais exótico. Esse modelo define como será a linha ou entrada do banco de dados em C #, para que você não precise se preocupar com as coisas de baixo nível do banco de dados em seu código. Esse estilo simples de modelo às vezes é chamado de "objeto C # antigo simples" ou POCO.
  - E escreva o seguinte código:
  
  ```
  using System;
  using System.ComponentModel.DataAnnotations;
  namespace AspNetCoreTodo.Models 
  {
    public class TodoItem    
    {
        public Guid Id { get; set; }
        public bool IsDone { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTimeOffset? DueAt { get; set; }   
    }
  }
  ```
 
 
- **A View model:**  
  - Geralmente o usuario costuma procastinar então o modelo (entidade), que não exatamente o mesmo que o modelo que você deseja usar no MVC (o modelo de exibição)mas a exibição pode ser necessário exibir dois, dez ou cem itens de tarefas pendentes, Por esse motivo, o modelo de exibição deve ser uma classe separada que contém uma matriz de TodoItem;
  - Crie uma classe em Models chamada TodoViewModel.cs
  - E escreva o seguinte código:
 ```  
  namespace AspNetCoreTodo.Models 
{
    public class TodoViewModel    {
        public TodoItem[] Items { get; set;}    
  }
}
```
- **A View:** 
  - Uma Views no ASP.NET Core são criados usando a linguagem de modelagem Razor, que combina código HTML e C #.
  - No começo da classe vemos,"@model" que diz diz ao Razor qual modelo esperar que a view está vinculada.
  - Se houver itens de pendências no Model.Items, a declaração de cada loop fará um loop sobre cada item de pendência e renderizará uma linha da tabela (elemento <tr>) contendo o nome e a data de vencimento do item. Uma caixa de seleção está desativada, permitindo que o usuário marque o item como completo.
  - Crie uma pasta "Todo" dentro do diretório Views;
  - E dentro da pasta Todo crie um arquivo "Index.cshtml"
  - E escreva o seguinte código:
  ```  
  @model TodoViewModel

  @{    
    ViewData["Title"] = "Manage your todo list";
  }
  <divclass="panel panel-default todo-panel">
    <divclass="panel-heading">@ViewData["Title"]</div>
    
  <tableclass="table table-hover">
    <thead>
        <tr>
            <td>&#x2714;</td>
            <td>Item</td>
            <td>Due</td>
        </tr>
    </thead>      
    
    @foreach (var item in Model.Items)      {
        <tr>
            <td>
                <inputtype="checkbox"class="done-checkbox">
                </td>
                <td>@item.Title</td>
                <td>@item.DueAt</td>
        </tr>      
    }
  </table>

  <divclass="panel-footer add-item-form">
  <!-- TODO: Add item form -->
  </div>
  </div>
  ```
  
-  ## Layout:
  - Sobre o o restante do HTML, está na pasta Views/Shared/_Layout.cshtml, com templates Bootstrap e jQuery;
  - Também contem algumas configurações simples de CSS
  - O stylesheet está na pasta wwwroot/css
  - E escreva o seguinte código para adicionar algumas novas features no final do código:
   
   ```
  div.todo-panel {
  margin-top: 15px;
  }
  tabletr.done {
  text-decoration: line-through;
  color: #888;
 }
   ```
  
- ## Criando uma classe:
  - Pode-se faze-la diretamente no Controller porém por boas praticas e no mundo real o ideal é que o código seja separado, pois as classes seram muito maiores, deixando dificil a manipulação podento ter as seguintes preocupações:
    - **Renderização de views** e manipulção de dados recebidos: é isso que o seu controlador já faz.
    - **Executar lógica business**, ou código e lógica relacionados ao objetivo e "negócios" da sua aplicação. Por exemplo : lógica de negócios incluem o cálculo de um custo total com base nos preços e taxas de produtos ou verificar se um jogador tem pontos suficientes para subir de nível em um jogo. 
    - **Manipulação de um banco de dados**.
  - O ideal de um projeto organizado é mante-lo nas arquiteturas multi-tier ou n-tier;
  - Neste projeto, você usaremos duas camadas de aplicativos:
    - Uma camada de apresentação(**presentation layer**) composta pelos controladores e viwes que interagem com o usuário 
    - E uma camada de serviço(**service layer**) que contém lógica de negócios e código do banco de dados. 
    - como a camada de apresentação já existe vamos criar um serviço que lide com a lógica de negócios de tarefas pendentes e salva itens de tarefas pendentes em um banco de dados.
 - Criando a interface:
   - Em C# tem a concepção de **Interface**, onde as interfaces facilitam manter suas classes separadas e fáceis de testar.
   - Então por convenção, as interfaces são prefixadas com "I".
   - Crie um novo diretório chamado Services e dentro dele um arquivo  chamado ITodoItemService.cs :
   - Escreva o seguinte código: 
  
  ```
  
  ```
  
  
   
   
  
- ## Comandos: Usando o Git ou GitHub 
  - **Por segurança e facilidade de compartilhamento, entre outras funcionalidades é utilizado o Github, além disso ele serve como o seu curriculo de programador;**
  - 'cd ..' saia da pasta do projeto;
  - 'git init' inicia um novo repositório na pasta raiz do projeto. Caso ocorra erro volte nos dowloads e baixe e configure o Git Bash. Ele deve criar uma pasta .git.

- ## Termos:
  - **Guids (orGUIDs)** são longas sequências de letras e números, como 43ec09f2-7f70-4f4b-9559-65011d5781bb. Como os guias são aleatórios e é improvável que sejam duplicados acidentalmente, eles são comumente usados como IDs únicos. Você também pode usar um número (inteiro) como ID da entidade do banco de dados, mas precisará configurar seu banco de dados para sempre aumentar o número quando novas linhas forem adicionadas ao banco de dados.
 - **Booleano** (valor verdadeiro / falso), Por padrão, será falso para todos os novos itens. Posteriormente, pode-se mudar essa propriedade para true quando o usuário clicar na caixa de seleção de um item na visualização.
 - **Required** informa que o campo é obrigatório ao ASP.NET Core que essa sequência não pode ser nula ou vazia.
 - **DueAt** é um DateTimeOffset, que é um tipo de C# que armazena um carimbo de data/hora junto com um deslocamento de fuso horário do UTC. Armazenar o deslocamento de data, hora e fuso horário juntos facilita o agendamento de datas com precisão em sistemas em fusos horários diferentes. Além disso temos o "?" ponto de interrogação após o tipo DateTimeOffset ? Essa marca a propriedade DueAt como anulável ou opcional. Se o "?" não foi incluído, todos os itens de pendências precisam ter uma data de vencimento.
 - **Strings** em C# são sempre anuláveis, portanto, não há necessidade de marca-lás como anulável. As strings C # podem ser nulas, vazias ou conter texto.
 - **get; set; ou (getter e setter)** leitura / gravação.
 - ** Arquitetura n-tier**: A maioria dos projetos maiores usa uma arquitetura de três camadas: uma camada de apresentação, uma camada de lógica de serviço e uma camada de repositório de dados. Um repositório é uma classe que é focada apenas no código do banco de dados (sem lógica de negócios). Neste aplicativo, você os combinará em uma única camada de serviço por simplicidade, mas fique à vontade para experimentar diferentes maneiras de arquitetar o código.
 
 
  
 
 
  
 

