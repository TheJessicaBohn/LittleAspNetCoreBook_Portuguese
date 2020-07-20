<h1 align="center"> LittleAspNetCoreBook_Portuguese </h1>
<p align="justify">
- Esse repositório visa seguir as lições do Little Asp Net Core Book disponível em : https://s3.amazonaws.com/recaffeinate-files/LittleAspNetCoreBook.pdf , em portugues, para contribuir com a comunidade brasileira;  </p>
- Tudo o que estiver dentro de 'aspas simples' são comandos a serem digitados nos terminais sem as aspas;
- Dica: após as palavras chaves das linhas de comando (cd, ls, mkdir, etc), pode-se digitar a primeira letra da pasta ou arquivo e a tecla Tab da o autocompletar;
- Dowloads :  
  - Editor utilizado (dica, pode ser outro de sua prefência ou o VS mesmo) :Visual Studio Community  a partir do 2017 version 15.3 or mais atualizada https://visualstudio.microsoft.com/pt-br/vs/community/  ou  VSCode https://code.visualstudio.com/download, e na hora de instalar colocar as opções de .net e C#;
  - .NET Core SDK, incluindo o runtime https://dotnet.microsoft.com/download/dotnet-core?utm_source=getdotnetcorecli&utm_medium=referral;
- Após as instalações verifique por 'dotnet --version' e 'dotnet --info' no seu terminal ou power shell se ele foi instalado corretamente, se ele não retornar nenum erro ou comando desconhecido, está ok;
- ## Comandos : Criando um Hello World
  - 'cd Documents' (no lugar de Documents a pasta que você deseja cria);
  - 'dotnet new console -o CsharpHelloWorld' , note que ele cria uma pasta de mesmo nome
  - 'cd CsharpHelloWorld',  isso dentro da pasta repare que ele cria um arquivo .csproj e um .cs, se caso não estiver dentro de um editor de o comando 'ls' para visualizar;
  - 'dotnet run' lembrando que você de estar exatamente dentro da pasta do projeto. No terminal deve retornar Hello Word!.
- ## Comandos : Criando um ASP.NET Core project
  - 'cd ..' para sair da pasta do CsharpHelloWorld;
  - 'mkdir AspNetCoreTodo', cria uma nova pasta onde após o mkdir é o nome e pode-se criar com outro nome de sua preferência;
  - 'cd AspNetCoreTodo' Entra na respectiva pasta, lembrando que, se o nome da mesma foi modificado, deve-se escrever o nome escolhido após cd;
  - 'dotnet new mvc --auth Individual -o AspNetCoreTodo', para criar um comando MVC;
  - 'cd AspNetCoreTodo' para entrar na nova pasta criada;
  - 'dotnet run' executa o programa e deve retornar:
      __info: Microsoft.Hosting.Lifetime[0] __
      __Now listening on: https://localhost:5001 __
      __info: Microsoft.Hosting.Lifetime[0] __
      __Now listening on: http://localhost:5000 __
      __info: Microsoft.Hosting.Lifetime[0] __
      __Application started. Press Ctrl+C to shut down. __
      __info: Microsoft.Hosting.Lifetime[0] __
      __Hosting environment: Development __
      __info: Microsoft.Hosting.Lifetime[0] __
      __Content root path: C:\Users\xxxxx\Desktop\littleAspNet\AspNetCoreTodo\AspNetCoreTodo __
   e após isso abra o navegado em http://localhost:5000, e ele deve aparecer a pagina com " Welcome Learn about building Web apps with ASP.NET Core." , pra apar o serviço
   Ctrl + C;
 - Conteúdo : ASP.NET Core project
   - Program.cs e Startup.cs são classes que configuram o servidor web e ASP.NET Core pipeline.
   - Models, Views, e Controllers são diretórios contêm os componentes da arquitetura Model-View-Controller (MVC).
   - 
   

 
