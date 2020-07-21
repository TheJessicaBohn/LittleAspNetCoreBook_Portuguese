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
  - 'cd AspNetCoreTodo' Entra na respectiva pasta, lembrando que, se o nome da mesma foi modificado, deve-se escrever o nome escolhido após cd;
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
   
   e após isso abra o navegado em http://localhost:5000, e ele deve aparecer a pagina com " Welcome Learn about building Web apps with ASP.NET Core." , pra apar o serviço
   Ctrl + C;
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
  - E cole o seguinte código:
  
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
  - Substitua por 
  ```
  public IActionResult Index()
    {// Get to-do items from database
    // Put items into a model
    // Render view using the model    }
     }
  ```
- ## Comandos: Usando o Git ou GitHub 
  - **Por segurança e facilidade de compartilhamento, entre outras funcionalidades é utilizado o Github, além disso ele serve como o seu curriculo de programador;**
  - 'cd ..' saia da pasta do projeto;
  - 'git init' inicia um novo repositório na pasta raiz do projeto. Caso ocorra erro volte nos dowloads e baixe e configure o Git Bash. Ele deve criar uma pasta .git.
 
  
 
 
  
 

