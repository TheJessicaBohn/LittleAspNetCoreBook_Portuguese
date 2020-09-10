

 # Comandos : Criando um ASP.NET Core project
  - `cd ..` para sair da pasta do CsharpHelloWorld;
  - `mkdir AspNetCoreTodo`, cria uma nova pasta onde após o mkdir é o nome e pode-se criar com outro nome de sua preferência;
  - `cd AspNetCoreTodo` Entra na respectiva pasta, lembrando que, se o nome da mesma foi modificado, deve-se escrever o nome escolhido após o comando cd;
  - `dotnet new mvc --auth Individual -o AspNetCoreTodo`, para criar um projeto com comando MVC;
  - `cd AspNetCoreTodo` para entrar na nova pasta criada;
  - `dotnet run` executa o programa e deve retornar:
     
      **info: Microsoft.Hosting.Lifetime[0]<br/>**
      **Now listening on: https://localhost:5001<br/>**
      **info: Microsoft.Hosting.Lifetime[0]<br/>**
      **Now listening on: http://localhost:5000<br/>**
      **info: Microsoft.Hosting.Lifetime[0]<br/>**
      **Application started. Press Ctrl+C to shut down.<br/>**
      **info: Microsoft.Hosting.Lifetime[0]<br/>**
      **Hosting environment: Development<br/>**
      **info: Microsoft.Hosting.Lifetime[0]<br/>**
      **Content root path: C:\Users\xxxx\Desktop\littleAspNet\AspNetCoreTodo\AspNetCoreTodo<br/>**
   
   e após isso abra o navegado em http://localhost:5000, e ele deve aparecer a pagina com " Welcome Learn about building Web apps with ASP.NET Core." , para parar o serviço `Ctrl + C`;
 - **Conteúdo : ASP.NET Core project**
   - **Program.cs** e **Startup.cs** são classes que configuram o servidor web e ASP.NET Core pipeline;
   - **Models, Views**, e **Controllers** são diretórios contêm os componentes da arquitetura Model-View-Controller (MVC);
   - **wwwroot** contém ativos estáticos que podem ser agrupados e compactados automaticamente, como CSS, JavaScript e arquivos de imagem;
   - **appsettings.json** contém as configuração de inicialização que o ASP.NETCore carrega; 

 # Basico de MVC :

  **Por padrão o funcionamento dos elementos ocorre da seguinte forma:**<br />
  - :video_game: O **Controller**:
    - Recebe uma solicitação e consulta algumas informações no banco de dados;
    - Cria um modelo com as informações e o anexa a uma view;
    - A **View** é renderizada e exibida no navegador do usuário;
    - Então o usuário clica em um botão ou envia um formulário, que envia uma nova solicitação ao controlador e o ciclo se repete.

 ## Continuando o projeto ASP.NET Core : Controller
  **Agora que sabemos o que é um controller vamos contruir um:**<br />
  - Se abrirmos a pasta Controllers, veremos que ja existe um HomeController.cs que inclui três métodos de ação (Index, About, e Contact) que são mapeados pelo ASP.NET Core para esses URLs de rota;
  - Pelo VS Code clicando na pasta Controllers, você pode criar um "new file" chamado TodoController não se esqueça da exentensão .cs;
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

- :dancer: **O Model**:
  - Vamos criar um modelo que represente um item de tarefa pendente armazenado no banco de dados (às vezes chamado de entidade) e o modelo que será combinado com uma visualização (o MV no MVC) e enviado de volta ao navegador do usuário. 
  - Primeiro criamos uma Classe chamada TodoItem.cs, dentro da pasta Models;
  - Ela define o que o banco de dados precisará armazenar para cada item de tarefa: 
    - Um ID : um guid ou um identificador globalmente exclusivos e são gerados aleatoriamente, para que você não precise se preocupar com o incremento automático
    - Um título ou nome : valor do texto). Isso conterá a descrição do nome do item de pendências. O atributo [Required] informa que o campo é obrigatório.
    - IsDone : valor booleano. 
    - DueAt: informa se o item está completo e qual é a data do vencimento.
    - Agora não importa qual seja a tecnologia de banco de dados implícito. Pode ser SQL Server, MySQL, MongoDB, Redis ou algo mais exótico. Esse modelo define como será a linha ou entrada do banco de dados em C #, para que você não precise se preocupar com as coisas de baixo nível do banco de dados em seu código. Esse estilo simples de modelo às vezes é chamado de "objeto C # antigo simples" ou POCO.
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
 
- :sunglasses: **A View model:**  
  - Geralmente o usuario costuma procastinar então o modelo (entidade), que não exatamente o mesmo que o modelo que você deseja usar no MVC (o modelo de exibição), mas a exibição pode ser necessário exibir dois, dez ou cem itens de tarefas pendentes, Por esse motivo, o modelo de exibição deve ser uma classe separada que contém uma matriz de TodoItem;
  - Crie uma classe em Models chamada TodoViewModel.cs
  - E escreva o seguinte código:
 ```cshap=
  namespace AspNetCoreTodo.Models 
{
    public class TodoViewModel    {
        public TodoItem[] Items { get; set;}    
  }
}
```
-:eyeglasses: **A View:** 
  - Uma View no ASP.NET Core são criados usando a linguagem de modelagem Razor, que combina código HTML e C#.
  - No começo da classe vemos,"@model" que diz diz ao Razor qual modelo esperar que a view está vinculada.
  - Se houver itens de pendências no Model.Items, a declaração de cada loop fará um loop sobre cada item de pendência e renderizará uma linha da tabela (elemento <tr>) contendo o nome e a data de vencimento do item. Uma caixa de seleção está desativada, permitindo que o usuário marque o item como completo.
  - Crie uma pasta "Todo" dentro do diretório Views;
  - E dentro da pasta Todo crie um arquivo "Index.cshtml"
  - E escreva o seguinte código:
  
  ```cshap=  
  @model TodoViewModel

  @{    
    ViewData["Title"] = "Manage your todo list";
  }
  <div class="panel panel-default todo-panel">
    <div class="panel-heading">@ViewData["Title"]</div>
    
  <table class="table table-hover">
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

  <div class="panel-footer add-item-form">
  <!-- TODO: Add item form -->
  </div>
  </div>
  ```
  
-  :book: **Layout:**
   - Sobre o o restante do HTML, está na pasta Views/Shared/_Layout.cshtml, com templates Bootstrap e jQuery;
   - Também contem algumas configurações simples de CSS
   - O stylesheet está na pasta wwwroot/css
   - E escreva o seguinte código para adicionar algumas novas features no final do código do arquivo site.css :
   
 ```css=
 div.todo-panel {
 margin-top: 15px;
 }
 tabletr.done {
 text-decoration: line-through;
 color: #888;
 }
```
  
 ## Criando uma classe de serviço: :construction_worker:
  - Pode-se fazê-la diretamente no Controller porém por boas praticas e no mundo real o ideal é que o código seja separado, pois as classes seram muito maiores, deixando dificil a manipulação podento ter as seguintes preocupações:
    - **Renderização de views** e manipulção de dados recebidos: é isso que o seu controlador já faz.
    - **Executar lógica business**, ou código e lógica relacionados ao objetivo e "negócios" da sua aplicação. Por exemplo: lógica de negócios incluem o cálculo de um custo total com base nos preços e taxas de produtos ou verificar se um jogador tem pontos suficientes para subir de nível em um jogo. 
    - **Manipulação de um banco de dados**.
  - O ideal de um projeto organizado é mante-lo nas arquiteturas multi-tier ou n-tier;
  - Neste projeto, você usaremos duas camadas de aplicaçãos:
    - Uma camada de apresentação(**presentation layer**) composta pelos controladores e viwes que interagem com o usuário 
    - E uma camada de serviço(**service layer**) que contém lógica de negócios e código do banco de dados. 
    - como a camada de apresentação já existe vamos criar um serviço que lide com a lógica de negócios de tarefas pendentes e salva itens de tarefas pendentes em um banco de dados.
 - Criando a interface:
   - Em C# tem a concepção de **Interface**, onde as interfaces facilitam manter suas classes separadas e fáceis de testar.
   - Então por convenção, as interfaces são prefixadas com "I".
   - Crie um novo diretório chamado Services e dentro dele um arquivo  chamado ITodoItemService.cs :
   - Escreva o seguinte código: 
  
 ```cshap=
 using System;
 using System.Collections.Generic;
 using System.Threading.Tasks;
 using AspNetCoreTodo.Models;

 namespace AspNetCoreTodo.Services
 {
 public interface ITodoItemService   
 {        
 
 Task<TodoItem[]> GetIncompleteItemsAsync();     
    }
  }
 
 ```
  - Veja que o namespace desse arquivo é AspNetCoreTodo.Services. Em .NET e é comum que o namespace siga o diretório em que o arquivo está armazenado, pois Namespaces são uma maneira de organizar arquivos de código.
  - Com interface está definida, será criada a classe de serviço atual.
  - Na pasta Services crie um arquivo chamado "FakeTodoItemService.cs" e escreva o seginte código:
   ```cshap=
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using AspNetCoreTodo.Models;

  namespace AspNetCoreTodo.Services
  {
      public class FakeTodoItemService : ITodoItemService    
      {
         public Task<TodoItem[]> GetIncompleteItemsAsync()       
          {
               var item1 = new TodoItem
                  {
                    Title = "Learn ASP.NET Core", 
                    DueAt = DateTimeOffset.Now.AddDays(1)  
                  };
               var item2 = new TodoItem
                 {
                      Title = "Build awesome apps",
                     DueAt = DateTimeOffset.Now.AddDays(2)
                  };
                  return Task.FromResult(new[] { item1, item2 }); 
        }   
      }
    }
   ```
   - Para testar  o controlador e a visualização e, em seguida, adicionar o código de banco de dados real, podemos ver que FakeTodoItemService implementa a interface ITodoItemService, mas sempre retorna o mesmo array de dois TodoItems. 
   
 ## Usando injeção de dependência: :syringe:
  - É utilizada quando uma solicitação chega e é roteada para o TodoController, o ASP.NET Core examina os serviços disponíveis e fornece automaticamente o FakeTodoItemService quando o controlador solicita umITodoItemService. Como os serviços são "injetados" no contêiner de serviços, esse padrão é chamado de injeção de dependência;
  - Vamos voltar em TodoController, tarabalhar com o ITodoItemService e escreva o seginte código:
     
   ```cshap=
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AspNetCoreTodo.Services;
    using Microsoft.AspNetCore.Mvc;

    namespace AspNetCoreTodo.Controllers
    {
      public class TodoController : Controller    
      {
        private readonly ITodoItemService _todoItemService;
        public TodoController(ITodoItemService todoItemService) 
        {
                _todoItemService = todoItemService;    
        }
        public IActionResult Index()
         {
        
         }
      }
   }
   ```
   - Variavél ITodoItemServic, deixa usar o serviço do metódo Index;
   - A linha public TodoController(ITodoItemService todoItemService), define o construtor da classe;
   - Para configurar os serviços vá a classe chamada Startup.cs e modifique:
   ```cshap=
   using AspNetCoreTodo.Services;
   public void ConfigureServices(IServiceCollection services)
   {
      services.AddMvc();
      services.AddSingleton<ITodoItemService, FakeTodoItemService>();
   }        
   ```
   - O método ConfigureServices adiciona coisas ao servicecontainer ou à coleção de serviços que o ASP.NET Core conhece;
   - A linha `services.AddMvc` adiciona os serviços internos do ASP.NETCore. Qualquer outro serviço que você deseja usar em seu aplicação deve ser adicionado ao contêiner de serviço aqui em ConfigureServices.
   - A linha `services.AddSingleton<ITodoItemService, FakeTodoItemService>( );` informa ao ASP.NET  para usar o FakeTodoItemService quando a interface ITodoItemService é solicitada em um construtor (ou em qualquer outro lugar);

 - ## Terminando o Controller:
    - A última etapa é terminar o código do controlador. O controlador agora tem uma lista de itens de tarefas pendentes da camada de serviço e precisa colocar esses itens em um TodoViewModel e vincular esse modelo à visualização que você criou anteriormente:
  ```charp=
    using AspNetCoreTodo.Services;
    using AspNetCoreTodo.Models;
    
    public async Task<IActionResult> Index()
      {
        var items = await _todoItemService.GetIncompleteItemsAsync();

        var model = new TodoViewModel()    
       {       
             Items = items    
        };
       return View(model);
```

   ## Testando:
   - Agora para testar o projeto abra um terminal no seu VSCode e digite 'dotnet run';
   - Ele deve retrornar da seguinte forma no terminal:
   
        **info: Microsoft.Hosting.Lifetime[0]<br/>**
      **Now listening on: https://localhost:5001<br/>**
    **info: Microsoft.Hosting.Lifetime[0]<br/>**
     **Now listening on: http://localhost:5000<br/>**
    **info: Microsoft.Hosting.Lifetime[0]<br/>**
      **Application started. Press Ctrl+C to shut down.<br/>**
    **info: Microsoft.Hosting.Lifetime[0]<br/>**
      **Hosting environment: Development<br/>**
    **info: Microsoft.Hosting.Lifetime[0]<br/>**
      **Content root path: C:\Users\User\Desktop\LittleAspnet\AspNetCoreTodo\AspNetCoreTodo<br/>**
     
   - Na pagina http://localhost:5000/ vai aparecer a seguinte mensagem  My to-dos na barra de navegação. Para fazer isso, você pode editar o arquivo de layout compartilhado.
  
   ## Atualizando o Layout:
- No arquivo de layout Views/Shared/_Layout.cshtml contém o HTML "base" para cada view. Dessa Forma podemos colocar novos elementos aos layout substituindo o seguinte código por:
```html=
     <ul class="navbar-nav flex-grow-1">
      <li class="nav-item"><a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a></li>
      <li class="nav-item"><a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a></li>
     </ul>
``` 
 - Substituir por
```html=
     <ul class="nav navbar-nav">
         <li><a asp-area="" asp-controller="Home" asp-action="Index">Home</a></li>
         <li><a asp-area="" asp-controller="Home" asp-action="About">About</a></li>
         <li><a asp-area="" asp-controller="Home" asp-action="Contact">Contact</a></li>
          <li><a asp-controller="Todo"asp-action="Index">My to-dos</a></li>
     </ul>
```

## Adicinar pacotes externos
- Vamos utilizar o Nuget, que possui link em Dowloads;
- Existem pacotes disponíveis no NuGet para tudo, desde analisar XML para aprendizado de máquina até postar no Twitter. O ASP.NET Core propriamente dito nada mais é do que uma coleção de pacotes NuGet que são adicionados ao seu projeto;
    - **Instalação:**
      - Na documentação vamos no link: https://docs.microsoft.com/en-us/nuget/install-nuget-client-tools e baixe o nuget.exe
      - Coloque-o numa pasta adequada, ex.C: ;
      - E por fim adicione à variavel de ambientes PATH.
      - Após isso rode o comando 'dotnet add package Humanizer' no teminal do VSCode;
      - Então no AspNetCoreTodo.csproj, deve aparecer na referência a seguine linha:
      ```
      <PackageReference Include="Humanizer" Version="2.8.26" />
       ```
    - **Utilização:**
      - Para utilizar o package no código precisamos utilizar um "using"
      - Então em Views/Todo/Index.cshtml coloque:
```charp=
       @model TodoViewModel
       @using Humanizer
```
    - E atualize a linha
```charp=
      <td>@item.DueAt.()</td>
      para
      <td>@item.DueAt.Humanize()</td>
```

- Se atualizarmos o navegador poderemos ver a diferença da forma que os dados estão sendo apresentados, pois agora ele não fala mais a hora e data mas sim, quanto tempo foi passado.
  
   ## Uso de Banco de Dados
- O bancos de dados pode se conectar com SQLServer, PostgreSQL e MySQL, mas também funciona com bancos de dados NoSQL como Mongo. Mas aqui usaremos SQLite neste projeto para tornar as coisas fáceis de configurar;
    - **Conectando ao Banco de Dados**
      - Vamos precisar de:
        - **1.Os pacotes do Entity Framework Core**: Eles estão incluídos por padrão em todos os projetos ASP.NET Core.
        - **2.Um banco de dados**. Pelo comando 'dotnet new mvc --auth Individual -o AspNetCoreTodo ' já é criado um pequeno banco de dados SQLite, na raiz do projeto chamado app.db;
        - **3.Uma classe de contexto de banco de dados**: O contexto do banco de dados é uma classe C# que fornece um ponto de entrada no banco de dados para que assim seu código poderá interagir com o banco de dados para ler e salvar itens. Já existe uma classe de contexto básica no arquivo Data/ApplicationDbContext.cs
        - **4.Uma string de conexão** Esteja você se conectando a um banco de dados de arquivos local (como SQLite) ou a um banco de dados hospedado em outro lugar, você definirá uma string que contém o nome ou endereço do banco de dados ao qual se conectar. Isso já está configurado por defautl no appsettings.jsonfile: a string de conexão para o banco de dados SQLite isDataSource = app.db.
       - Entity Framework Core, usa o contexto do banco de dados, junto com a string de conexão, para estabelecer uma conexão com o banco de dados. Então vamos precisar dizer para o Entity Framework Core qual contexto, string de conexão e provedor de banco de dados escrendo o seguinte código no método ConfigureServices da classe Startup.cs:
```charp=
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(        
            Configuration.GetConnectionString("DefaultConnection")));
                    ...
          }
```
  - Este código adiciona o ApplicationDbContext ao contêiner de serviço dizendo ao Entity Framework Core para usar o provedor de banco de dados SQLite, com a string de conexão da configuração (appsettings.json). O banco de dados está configurado e pronto para ser usado porém sem tabelas;

 ## Atualizando o Contexto
  - No arquivo Data/ApplicationDbContext.cs faça as modificações:
```charp=
  public class ApplicationDbContext : IdentityDbContext
  {
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        public DbSet<TodoItem> Items { get; set; }
            protected override void OnModelCreating(ModelBuilder builder)
       {
           base.OnModelCreating(builder);
           // ...
```
  - Um DbSet representa uma tabela ou coleção no banco de dados. Ao criar uma propriedade DbSet <TodoItem> chamada Items, você está dizendo ao Entity Framework Core que deseja armazenar entidades TodoItem em um item chamado de tabela então para atualizar o banco de dados para refletir a alteração que foi feita é preciso criar uma migração.
- ## Criando uma Migration
  - Como não existe no banco de dados, precisamos criar uma migration para atualizar o banco de dados com o seguinte comando 'dotnet ef migrations add AddItems'. AddItems é o nome da migration.
  - No terminal deve retornar:
  **Build started...<br/>**
  **Build succeeded.<br/>**
  **Done. To undo this action, use 'ef migrations remove'<br/>**
  - Podemos verificar em Data/Migrations que alguns arquivos:
 
  ![Alt text](https://github.com/TheJessicaBohn/LittleAspNetCoreBook_Portuguese/blob/master/Images/migrations.png)
  - Com o 'comando dotnet ef migrations list', pode-ser ver as Migrations criadas em lista
  ![Alt text](https://github.com/TheJessicaBohn/LittleAspNetCoreBook_Portuguese/blob/master/Images/Migrations_list.png)
  - Quando foi executado o dotnet new o primeiro arquivo de migração (com um nome como 00000_CreateIdentitySchema.cs) foi criado e aplicado, como demonstra na imagem acima. Sua nova migração AddItem é prefixada com um timestamp.
  - Se você abrir seu arquivo de migration Data/Migrations/_AddItems.cs, verá dois métodos chamados Up  e Down:
 ```charp=
 public partial class AddItems : Migration
 {
   protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(name: "Items",
        columns: table => new
            {
                 Id = table.Column<Guid>(nullable: false),
                 IsDone = table.Column<bool>(nullable: false),
                 Title = table.Column<string>(nullable: false),
                 DueAt = table.Column<DateTimeOffset>(nullable: true)
           },
               constraints: table =>
           {
                 table.PrimaryKey("PK_Items", x => x.Id);
         });
         }
protected override void Down(MigrationBuilder migrationBuilder)
          {
             migrationBuilder.DropTable(
                  name: "Items");
          }
      }
  }
  
 ```
  - O método Up é executado na migração ao banco de dados. Visto que você adicionou um DbSet <TodoItem> ao contexto do banco de dados, EntityFramework Core criará uma tabela de itens (com colunas que correspondem a umTodoItem) quando a migration é aplicada. O método Down faz o oposto: se você precisar reverter a migration, a tabela de itens será descartada.
 
 ### Solução alternativa para limitações do SQLite
  - Existem algumas limitações do SQLite que atrapalham se você tentar executar a migration
  - Uma soluçaõ paletiva (caso necessário): Comente ou remova as linhas "migrationBuilder.AddForeignKey" no método Up; 
  - Comente ou remova quaisquer linhas migrationBuilder.DropForeignKey no método Down.
 ## Aplicando a Migration a migração
  - A etapa final após criar uma (ou mais) migrations é aplicá-las de fato ao banco de dados, podemos usar o comando 'dotnet ef database update' que fará com que o Entity Framework Core crie o Itemstable no banco de dados.
  - Caso queira reverter o banco de dados, deve-se saber o nome da migration anterior: 'dotnet ef database update CreateIdentitySchema';
    - Isso executará os métodos Down de qualquer migration mais recente do que a que você especificou.
  - Se você precisar apagar completamente o banco de dados e reiniciar, execute 'dotnet ef database drop' e após 'dotnet ef database update' ;
    -  Isso irá refazer o scaffold do banco de dados e trazê-lo para a migration atual;
  - Agora o tanto o banco de dados quanto o contexto estão prontos para uso.

 ## Criando uma nova classe de serviço
  - Voltando ao capítulo de MVC, onde foi criado o FakeTodoItemService que continha itens de tarefas embutidos em código e tem-se agora um databasecontext, 
    - Pode-se então criar uma nova classe de serviço que usará o Entity Framework Core para obter os itens reais do banco de dados. Exclua o arquivo FakeTodoItemService.cs e crie um novo arquivo em Services/TodoItemService.cs:
 ```cshap=
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

  namespace AspNetCoreTodo.Services
  {
          public class TodoItemService : ITodoItemService    
          {
              private readonly ApplicationDbContext _context;
              public TodoItemService(ApplicationDbContext context) 
              {           
                  _context = context;       
              }
             public async Task<TodoItem[]> GetIncompleteItemsAsync()       
              {
                return await _context.Items               
                 .Where(x => x.IsDone == false)           
                 .ToArrayAsync();        
              }    
        
         }
  }
  ```
 
   - Notará-se o mesmo padrão de injeção de dependência que o capítulo de MVC,mas agora é o ApplicationDbContext que está sendo injetado. 
      - O ApplicationDbContext já está sendo adicionado ao contêiner de serviço no método ConfigureServices, portanto, está disponível para injeção aqui.
  - No código do método GetIncompleteItemsAsync é usado primeiro, a propriedade Items do contexto para acessar todos os itens de tarefas no DbSet: **var items = await _context.Items**;
  - Então, o método Where é usado para filtrar apenas os itens que não estão completos: **.Where(x => x.IsDone == false)**
  - E por ultimo o método ToArrayAsync solicita ao Entity Framework Core para obter todas as entidades que correspondem ao filtro e retorná-las como uma matriz. Esse metódo é assíncrono (retorna uma Tarefa), portanto, deve ser aguardado para obter seu valor.
  
 ## Atualizando o contêiner de serviço
  - Como foi excluída a classe FakeTodoItemService, será necessário atualizar a linha em ConfigureServices que está conectando a interface ITodoItemService em AspNetCoreTodo\Startup.cs:

```cshap=
services.AddScoped<ITodoItemService, TodoItemService>();
```
  - AddScoped adiciona seu serviço ao contêiner de serviço usando o scopedlifecycle. Isso significa que uma nova instância da classe TodoItemService será criada durante cada solicitação da web. Isso é necessário para classes de serviço que interagem com um banco de dados.
 - TodoController que depende de um ITodoItemService injetado ficará felizmente inconsciente da mudança nas classes de serviços, mas no futuro estará usando o Entity Framework Core e se comunicando com um banco de dados real.
- ### Testando
  - Inicie aplicação e abra o navegador no http://localhost:5000/todo. Os itens falsos sumiram e seu aplicação está fazendo consultas reais no banco de dados. 
- ## Adicionar mais recursos, New To-do items  
  - Agora vamos adicionar novos itens de tarefas usando um formulário;
  - Etapas: 
    - Adicionar um formulário à visualização; 
    - Criar uma nova ação no controlador para lidar com o formulário; 
    - Adicionar código à camada de serviço para atualizar o banco de dados.
  
  - ### Adicionando um formulário:
    - O Views/Todo/Index.cshtml tem um espaço reservado para o formulário Adicionar item:
    ```
    <div class="panel-footer add-item-form">
          <!-- TODO: Add item form -->
    </div>
    ```
   - Para manter as coisas separadas e organizadas, você criará o formulário como uma visualização parcial. Uma visualização parcial é uma pequena parte de uma visualização maior que fica em um arquivo separado.
   -  crie um novo arquivo em Views/Todo/AddItemPartial.cshtml, com o seguinte código:
 ```html=
  @model TodoItem

  <form asp-action="AddItem" method="POST">
     <label asp-for="Title">Add a new item:</label>
     <input asp-for="Title">
      <button type="submit">Add</button>
  </form>
  ```
  - O **asp-action** pode gerar uma URL para o formulário,mas nesse caso, os auxiliares asp-action substituídos pelo caminho real para a rota AddItem:
  ```html=
  <form action="/Todo/AddItem" method="POST">
  ```
  - Ao adicionar a tag asp ao elemento <form> também adiciona um campo oculto ao formulário que contém um token de verificação. Este token pode ser usado para evitar ataques de falsificação de solicitação entre sites (CSRF). Isso criou a view parcial. Agora, façamos a view principal Todo:
  - Edite o seguinte campo em Views/Todo/Index.cshtml:
  
  ```html=
    <div class="panel-footer add-item-form">
    @await Html.PartialAsync("AddItemPartial", new TodoItem())
    </div>
  ```

  - ## Adicionando Ações
  - Quando um usuário clicar no formulário criado, o navegador irá construir uma solicitação POST em /Todo/AddItem em sua aplicação. Mas se você tentar agora, o ASP.NET Core retornará um erro 404 Not Found, pois não há nenhuma ação que possa manipular a rota /Todo/AddItem.
  - Você precisará criar uma nova ação chamada AddItem na classe TodoController: 
 
 ```csharp=
   [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem newItem)
        {
          if (!ModelState.IsValid)    
          {
            return RedirectToAction("Index");    
          }
          var successful = await _todoItemService.AddItemAsync(newItem);

          if (!successful)    
          {
            return BadRequest("Could not add item.");   
          }
          return RedirectToAction("Index");
        }
  ```
  - Pode-se ver que o AddItem aceita um parâmetro TodoItem. Quando é usado aqui como um parâmetro de ação, o ASP.NET Core executará automaticamente um processo chamado **model binding** (vinculação de modelo);
 - O atributo **[ValidateAntiForgeryToken]** antes da ação informa ao ASP.NET Core que ele deve procurar (e verificar) o token de verificação oculto que foi adicionado ao formulário pelo auxiliar de tag asp-action. Esta é uma medida de segurança importante para evitar falsificação de solicitação entre sites(CSRF) ataques, em que seus usuários podem ser enganados para enviar dados de um site malicioso. O token de verificação garante que seu aplicação seja realmente aquele que processou e enviou o formulário;
 - Por contada linha @model, a visão parcial espera receber um objetoTodoItem quando for renderizado. Passar um novo TodoItem via html. PartialAsync inicializa o formulário com um item vazio.
 - Depois de vincularmos os dados da solicitação ao modelo, o ASP.NET Core também realiza a validação do modelo. A validação verifica se os dados vinculados ao modelo a partir da solicitação de entrada fazem sentido ou são válidos.
 - O atributo [Required] na propriedade Title informa ao validador de modelo do ASP.NET Core para considerar o Title inválido se estiver ausente ou em branco. Dê uma olhada no código da ação AddItem: o primeiro bloco verifica se o ModelState (o resultado da validação do modelo) é válido. É comum fazer esta verificação de validação logo no início da ação:
 ```csharp=
if (!ModelState.IsValid)
{
    return RedirectToAction("Index");
}
```
- Se o ModelState for inválido por qualquer motivo, o navegador será redirecionado para a rota /Todo/Index, que atualiza a página. Em seguida, o controlador chama a camada de serviço para fazer a operação real do banco de dados de salvar o novo item de tarefa:
```csharp=
var successful = await _todoItemService.AddItemAsync(newItem);
if (!successful)
{
    return BadRequest(new { error = "Could not add item." });
}
```
- Já o método AddItemAsync retornará verdadeiro ou falso dependendo se o item foi adicionado com sucesso ao banco de dados. Se falhar por algum motivo, ele retornará um erro HTTP 400 Bad Request somado a um objeto que contém uma mensagem de erro. 
- E por fim, se tudo for concluído sem erros, a ação redireciona o navegador para a /Todo/Index, que atualiza a página e exibe a nova lista atualizada de itens de tarefas para o usuário;

 ### Adicionando um método de serviço
- Muito provavelmente seu código dará linhas vermelhas em AddItemAsync porque o método ainda não existe. Como foi feito aneriormente, você precisa adicionar um método à camada de serviço na interface ITodoItemService:
```csharp=
public interface ITodoItemService
{
    Task<TodoItem[]> GetIncompleteItemsAsync();

    Task<bool> AddItemAsync(TodoItem newItem);
}
```
- Então, a implementação real em TodoItemService:
```csharp=
public async Task<bool> AddItemAsync(TodoItem newItem)
{
    newItem.Id = Guid.NewGuid();
    newItem.IsDone = false;
    newItem.DueAt = DateTimeOffset.Now.AddDays(3);

    _context.Items.Add(newItem);

    var saveResult = await _context.SaveChangesAsync();
    return saveResult == 1;
}
```
- A propriedade newItem.Title já foi definida pelo model binder do ASP.NET Core, portanto, esse método só precisa atribuir um ID e definir os valores padrão para as outras propriedades. Em seguida, o novo item é adicionado ao contexto do banco de dados. Na verdade, ele não é salvo até que você chame SaveChangesAsync(). Se a operação de salvamento foi bem sucedida, SaveChangesAsync () retornará 1.
 ### Testando
- Execute a aplicação com o comando `dotnet run` e de um `Ctrl+Click` na pagina para abrir.
- Clique em "My to-dos" no canto superior direito da tela e adicione alguns itens de teste à sua lista de tarefas com o formulário.
- Como os itens estão sendo armazenados no banco de dados, eles ainda estarão lá, mesmo depois de você parar e iniciar o aplicação novamente.

 ## Adicionando itens completos com uma caixa de seleção
- Na view Views /Todo/Index.cshtml, uma caixa de seleção é exibida para cada item de tarefa:
```charp=
<input type="checkbox" class="done-checkbox">
```
- Se clicarmos na caixa de seleção ainda não faz nada. Como feito anterioriormente, será adicionado esse comportamento usando formulários e ações.

 ### Adicionando elementos de formulário à vista
- Primeiro, atualize a view e envolva cada caixa de seleção com um elemento <form> em Views/Todo/Index.cshtml. Em seguida, adicione um elemento oculto contendo o ID do item:
  
  ```charp=
  <td>
      <form asp-action="MarkDone" method="POST">
          <input type="checkbox" class="done-checkbox">
          <input type="hidden" name="id" value="@item.Id">
      </form>
  </td>
  ```
- Quando o loop foreach é executado na view e imprime uma linha para cada item a fazer, uma cópia deste formulário existirá em cada linha. A entrada oculta que contém o ID do item de tarefa torna possível para o código do controlador informar qual caixa foi marcada. (Sem ele, você seria capaz de dizer que alguma caixa foi marcada, mas não qual.)
- Se executarmos a aplcação agora, as caixas de seleção ainda não farão nada, porque não há um botão de envio para dizer ao navegador para criar uma solicitação POST com os dados do formulário. O ideal é clicar na caixa de seleção para enviar o formulário automaticamente. Você pode conseguir isso adicionando algum JavaScript.

 ### Adicionando código JavaScript
- Encontre o arquivo site.js no diretório wwwroot/js e adicione este código:
```csharp=
$(document).ready(function() {

    // Wire up all of the checkboxes to run markCompleted()
    $('.done-checkbox').on('click', function(e) {
        markCompleted(e.target);
    });
});

function markCompleted(checkbox) {
    checkbox.disabled = true;

    var row = checkbox.closest('tr');
    $(row).addClass('done');

    var form = checkbox.closest('form');
    form.submit();
}
```  
- Este código primeiro usa jQuery (uma biblioteca auxiliar JavaScript) para anexar algum código ao clique, mesmo de todas as caixas de seleção na página com a caixa de seleção CSSclass done. Quando uma caixa de seleção é clicada, a função markCompleted( ) é executada.
- A função markCompleted( ) faz algumas coisas:
  - Adiciona o atributo disabled à caixa de seleção para que não possa ser clicado novamente. 
  - Adiciona a classe CSS concluída à linha pai que contém a caixa de seleção, o que altera a aparência da linha com base nas regras CSS em style.css 
  - Submete o formulário.
  
 ### Adicionando  uma ação ao controlador
- Vamos precisar adicionar uma ação chamada MarkDone no TodoController:
```csharp=
[ValidateAntiForgeryToken]
public async Task<IActionResult> MarkDone(Guid id)
{
    if (id == Guid.Empty)
    {
        return RedirectToAction("Index");
    }

    var successful = await _todoItemService.MarkDoneAsync(id);
    if (!successful)
    {
        return BadRequest("Could not mark item as done.");
    }

    return RedirectToAction("Index");
}
```
- Vamos percorrer cada linha desse método de ação. Primeiro, o método aceita um parâmetro Guid chamado id na assinatura do método. Exceto a ação AddItem, que usava um modelo e vinculação / validação de modelo, o parâmetro id é muito simples. Se os dados da solicitação de entrada incluem um chamado id, o ASP.NET Core tentará analisá-lo como um guid. Isso funciona porque o elemento oculto adicionado ao formulário da caixa de seleção é chamado id.
- Não está usando a vinculação de modelo, não há ModelState para verificar a validade. Em vez disso, pode-se verificar o valor guid diretamente para ter certeza de que é válido. Se por algum motivo o parâmetro id na solicitação estiver ausente ou não puder ser analisado como um guid, o id terá o valor Guid.Empty. Se for esse o caso, a ação informa ao navegador para redirecionar para /Todo/Index e atualizar a página.
- Em seguida, o controlador precisa chamar a camada de serviço para atualizar o banco de dados. Isso será tratado por um novo método chamado MarkDoneAsyncon da interface ITodoItemService, que retornará verdadeiro ou falso dependendo do sucesso da atualização:
```cshap=
var successful = await _todoItemService.MarkDoneAsync(id);
if (!successful)
{
    return BadRequest("Could not mark item as done.");
}
```
- Finalmente, se tudo estiver certo, o navegador é redirecionado para a ação /Todo/Index e a página é atualizada. Com a view e o controlador atualizados, tudo o que resta é adicionar o método de serviço faltante.

 ### Adicionando um método de serviço
- Primeiro, adicione MarkDoneAsync à definição da interface Services/ITodoItemService.cs:
```cshap=
Task<bool> MarkDoneAsync(Guid id);
```
- Agora, adicione a implementação concreta em Services/TodoItemService.cs:
```cshap=
public async Task<bool> MarkDoneAsync(Guid id)
{
    var item = await _context.Items
        .Where(x => x.Id == id)
        .SingleOrDefaultAsync();

    if (item == null) return false;

    item.IsDone = true;

    var saveResult = await _context.SaveChangesAsync();
    return saveResult == 1; // One entity should have been updated
}
```
- Este método usa Entity Framework Core e Where() para localizar um item por ID no banco de dados. O método SingleOrDefaultAsync() retornará o item ou nulo se ele não puder ser encontrado;
- Depois de ter certeza de que o item não é nulo, é uma simples questão de definir a propriedade IsDone:
```cshap=
item.IsDone = true;
```
- A alteração da propriedade afeta apenas a cópia local do item até queSaveChangesAsync() seja chamado para persistir a alteração no banco de dados. SalveChangesAsync() retorna um número que indica quantas entidades foram atualizadas durante a operação de salvamento. Nesse caso, será 1 (o item foi atualizado) ou 0 (se algo deu errado).

 #### Testando
- Execute o aplicação e tente marcar alguns itens da lista. Atualize a página e eles desaparecerão completamente, por causa do filtro Where() no método GetIncompleteItemsAsync().
- Execute o aplicação e tente marcar alguns itens da lista. Atualize a página e eles desaparecerão completamente, por causa do filtro Where () no método GetIncompleteItemsAsync().
- No momento, o aplicação contém uma única lista de tarefas compartilhada. Seria ainda mais útil se ele mantivesse o controle de listas de tarefas individuais para cada usuário.

 # Segurança e identidade
**A segurança é uma grande preocupação de qualquer aplicação da Web ou API moderno. É importante manter seus dados de usuário ou cliente protegidos e fora do alcance de invasores.**
- Este é um tópico muito amplo, envolvendo coisas como:
  - Sanitizando a entrada de dados para evitar ataques de injeção de SQL;
  - Prevenção de ataques de domínio cruzado (CSRF) em formulários;
  - Usando HTTPS (criptografia de conexão) para que os dados não possam ser interceptados enquanto viajam pela Internet;
  - Dando aos usuários uma maneira de fazer login com segurança com uma senha ou outras credenciais;
  - Projetando redefinição de senha, conta recuperação e fluxos de autenticação multifatorial.
- O modelo MVC + Individual Authentication que você usou para criar o scaffold do projeto inclui várias classes construídas sobre o ASP.NET CoreIdentity, um sistema de autenticação e identidade que faz parte do ASP.NETCore. Fora da caixa, isso adiciona a capacidade de fazer login com um e-mail e uma senha.

 ## O que é ASP.NET Core Identifica?
- ASP.NET Core Identity é o sistema de identidade fornecido com ASP.NETCore. Como tudo o mais no ecossistema ASP.NET Core, é um conjunto de pacotes NuGet que podem ser instalados em qualquer projeto (e já estão incluídos se você usar o modelo padrão).
- A identidade do ASP.NET Core cuida do armazenamento de contas de usuário, hashing e armazenamento de senhas e gerenciamento de funções para usuários. Suporta login de e-mail / senha, autenticação multifatorial, login social com provedores como Google e Facebook, bem como conexão com outros serviços usando protocolos como OAuth 2.0 e OpenID Connect.  
- As visualizações Register e Login que vêm com o modelo MVC + IndividualAuthentication já tiram proveito do ASP.NET CoreIdentity e já funcionam! Tente registrar uma conta e fazer o login.

 ## Requerimento de autenticação
- Freqüentemente, você vai querer exigir que o usuário efetue login antes de poder acessar certas partes do seu aplicação. Por exemplo, faz sentido mostrar a página inicial para todos (esteja você conectado ou não), mas apenas mostre sua lista de tarefas depois de se conectar.
- Você pode usar o atributo [Authorize] no ASP.NET Core para exigir que um usuário conectado para uma ação específica ou um controlador inteiro. Para requerer autenticação para todas as ações do TodoController, adicione o atributo acima da primeira linha do controlador em Controllers/TodoController.cs:
```cshap= 
using Microsoft.AspNetCore.Authorization;
 // ...
[Authorize]
public class TodoController : Controller
{
    // ...
}
```
- Tente executar o aplicação e acessar / todo sem estar logado. Você será redirecionado para a página de login automaticamente.
- O atributo [Authorize] está realmente fazendo uma verificação de autenticação aqui, não uma verificação de autorização (apesar do nome do atributo). Posteriormente, o atributo será usado para verificar a autenticação e autorização de bots.
 ### Usando ID no aplicação
- Os próprios itens da lista de tarefas pendentes ainda são compartilhados entre todos os usuários, porque as entidades armazenadas de tarefas não estão vinculadas a um usuário específico. Agora que o atributo [Authorize] garante que você deve estar conectado para ver a exibição de tarefas, você pode filtrar a consulta do banco de dados com base em quem está conectado;Primeiro, injete um UserManager <ApplicationUser> em Controllers/TodoController.cs:
  
 ```cshap=
   using Microsoft.AspNetCore.Authorization;
   using Microsoft.AspNetCore.Identity;
   // ...
    [Authorize]
    public class TodoController : Controller
    {
      private readonly ITodoItemService _todoItemService;
      private readonly UserManager<ApplicationUser> _userManager;

      public TodoController(ITodoItemService todoItemService,
          UserManager<ApplicationUser> userManager)
      {
          _todoItemService = todoItemService;
          _userManager = userManager;
      }

      // ...
   }
  ```

  - A classe UserManager faz parte da identidade do ASP.NET Core. Pode-se usá-la para obter o usuário atual na ação Índice:

```csharp=c
public async Task<IActionResult> Index()
{
    var currentUser = await _userManager.GetUserAsync(User);
    if (currentUser == null) return Challenge();

    var items = await _todoItemService
        .GetIncompleteItemsAsync(currentUser);

    var model = new TodoViewModel()
    {
        Items = items
    };

    return View(model);
}
```
- O novo código na parte superior do método de ação usa o UserManager para pesquisar o usuário atual a partir da propriedade User disponível na ação: var currentUser = await _userManager.GetUserAsync(User);
- Se houver um usuário conectado, a propriedade User contém um lightweighttobject com algumas (mas não todas) das informações do usuário. O UserManager usa isso para pesquisar os detalhes completos do usuário no banco de dados por meio do método GetUserAsync().
- O valor de currentUser nunca deve ser nulo, porque o atributo [Authorize] está presente no controlador. No entanto, é uma boa idéia fazer uma verificação de sanidade, apenas para garantir. Você pode usar o método Challenge() para forçar o usuário a fazer login novamente se suas informações estiverem ausentes: 
```cshap=
if (currentUser == null) return Challenge();
```
- Como agora você está passando um parâmetro ApplicationUser paraGetIncompleteItemsAsync(), será necessário atualizar a interface em Services/ITodoItemService.cs:
```cshap=
public interface ITodoItemService
{
    Task<TodoItem[]> GetIncompleteItemsAsync(
        ApplicationUser user);
    
    // ...
}
```
- Como você alterou a interface ITodoItemService, também precisa atualizar a assinatura do método GetIncompleteItemsAsync() em Services/TodoItemService:
```cshap=
public async Task<TodoItem[]> GetIncompleteItemsAsync(
    ApplicationUser user)
```
- A próxima etapa é atualizar a consulta do banco de dados e adicionar um filtro para mostrar apenas os itens criados pelo usuário atual. Antes de fazer isso, você precisa adicionar uma nova propriedade ao banco de dados.

### Atualizando o banco de dados
- Será preciso adicionar uma nova propriedade ao modelo de entidade TodoItem para que cada item possa "lembrar" o usuário que o possui em Models/TodoItem.cs:

```cshap=
      public string UserId { get; set; }
```

- Como o modelo de entidade usado pelo contexto do banco de dados, foi atualizado, também precisa migrar o banco de dados. Crie uma nova migração usando `dotnet ef` no terminal:

Isso cria uma nova migração chamada `AddItemUserId` que adicionará uma nova coluna à tabela` Items`, espelhando a mudança que você fez no modelo `TodoItem`.

Use dotnet ef novamente para aplicá-lo ao banco de dados: `dotnet ef database update`;

### Atualizando a classe de serviço

- Com o banco de dados e o contexto do banco de dados atualizados, agora você pode atualizar o método `GetIncompleteItemsAsync()` **Controllers/TodoController.cs** e adicionar outra cláusula à instrução` Where`:

```cshap=
  {
      return await _context.Items
          .Where(x => x.IsDone == false && x.UserId == user.Id)
          .ToArrayAsync();
}
```
- Se você executar o aplicação e se registrar ou efetuar login, verá uma lista de tarefas vazia mais uma vez. Infelizmente, todos os itens que você tentar adicionar desaparecem no éter, porque você ainda não atualizou a ação `AddItem` para ficar ciente do usuário.

### Atualizando as ações AddItem e MarkDone

Você precisará usar o `UserManager` para obter o usuário atual nos métodos de ação` AddItem` e `MarkDone`, assim como você fez em` Index`.

Aqui estão os dois métodos atualizados em **Controllers/TodoController.cs**:

```cshap=
[ValidateAntiForgeryToken]
public async Task<IActionResult> AddItem(TodoItem newItem)
{
    if (!ModelState.IsValid)
    {
        return RedirectToAction("Index");
    }

    var currentUser = await _userManager.GetUserAsync(User);
    if (currentUser == null) return Challenge();

    var successful = await _todoItemService
        .AddItemAsync(newItem, currentUser);

    if (!successful)
    {
        return BadRequest("Could not add item.");
    }

    return RedirectToAction("Index");
}

[ValidateAntiForgeryToken]
public async Task<IActionResult> MarkDone(Guid id)
{
    if (id == Guid.Empty)
    {
        return RedirectToAction("Index");
    }

    var currentUser = await _userManager.GetUserAsync(User);
    if (currentUser == null) return Challenge();

    var successful = await _todoItemService
        .MarkDoneAsync(id, currentUser);
    
    if (!successful)
    {
        return BadRequest("Could not mark item as done.");
    }

    return RedirectToAction("Index");
}
```
- Ambos os métodos de serviço agora devem aceitar um parâmetro ApplicationUser. Atualize a definição da interface em ITodoItemService:

```cshap=
Task<bool> AddItemAsync(TodoItem newItem, ApplicationUser user);

Task<bool> MarkDoneAsync(Guid id, ApplicationUser user);
```
- E, finalmente, atualize as implementações do método de serviço no `TodoItemService`. No método `AddItemAsync`, defina a propriedade` UserId` ao construir um `novo TodoItem`:
```cshap=
public async Task<bool> AddItemAsync(TodoItem newItem, ApplicationUser user)
{
    newItem.Id = Guid.NewGuid();
    newItem.IsDone = false;
    newItem.DueAt = DateTimeOffset.Now.AddDays(3);
    newItem.UserId = user.Id;

    // ...
}
```
- A cláusula `Where` no método` MarkDoneAsync` também precisa verificar o ID do usuário, então um usuário invasor não pode completar os itens de outra pessoa adivinhando seus IDs:
```
public async Task<bool> MarkDoneAsync(Guid id, ApplicationUser user)
{
    var item = await _context.Items
        .Where(x => x.Id == id && x.UserId == user.Id)
        .SingleOrDefaultAsync();

    // ...
}
```
## Autorização com funções

- As funções são uma abordagem comum para lidar com autorização e permissões em um aplicação da web. Por exemplo, é comum criar uma função de Administrador que conceda aos usuários administradores mais permissões ou poder do que os usuários normais.

- Neste projeto, você adicionará uma página Gerenciar usuários que apenas os administradores podem ver. Se usuários normais tentarem acessá-lo, verão um erro.

### Adicionar uma página de gerenciamento de usuários

Primeiro, crie um novo controlador em ** Controllers/ManageUsersController.cs **:

```cshap=
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<ApplicationUser>
            _userManager;
        
        public ManageUsersController(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = (await _userManager
                .GetUsersInRoleAsync("Administrator"))
                .ToArray();

            var everyone = await _userManager.Users
                .ToArrayAsync();

            var model = new ManageUsersViewModel
            {
                Administrators = admins,
                Everyone = everyone
            };

            return View(model);
        }
    }
}
```
- Definir a propriedade `Roles` no atributo` [Autorize] `irá garantir que o usuário deve estar logado ** e ** atribuído a função de Administrador para visualizar a página.

A seguir, crie um modelo de view em ** Controllers/ManageUsersController.cs **:

```cshap=
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<ApplicationUser>
            _userManager;
        
        public ManageUsersController(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = (await _userManager
                .GetUsersInRoleAsync("Administrator"))
                .ToArray();

            var everyone = await _userManager.Users
                .ToArrayAsync();

            var model = new ManageUsersViewModel
            {
                Administrators = admins,
                Everyone = everyone
            };

            return View(model);
        }
    }
}
```
Definir a propriedade `Roles` no atributo` [Autorizar] `irá garantir que o usuário deve estar logado e tribuído a função de Administrador para visualizar a página.

A seguir, crie um modelo de visualização em ** Models/ManageUsersViewModel.cs **:

```csharp=
using System.Collections.Generic;

namespace AspNetCoreTodo.Models
{
    public class ManageUsersViewModel
    {
        public ApplicationUser[] Administrators { get; set; }

        public ApplicationUser[] Everyone { get; set;}
    }
}
```
- Finalmente, crie uma pasta `Views/ManageUsers` e uma visão para a ação` Index` em ** Views/ManageUsers/Index.cshtml **:
```csharp=
@model ManageUsersViewModel

@{
    ViewData["Title"] = "Manage users";
}

<h2>@ViewData["Title"]</h2>

<h3>Administrators</h3>

<table class="table">
    <thead>
        <tr>
            <td>Id</td>
            <td>Email</td>
        </tr>
    </thead>
    
    @foreach (var user in Model.Administrators)
    {
        <tr>
            <td>@user.Id</td>
            <td>@user.Email</td>
        </tr>
    }
</table>

<h3>Everyone</h3>

<table class="table">
    <thead>
        <tr>
            <td>Id</td>
            <td>Email</td>
        </tr>
    </thead>
    
    @foreach (var user in Model.Everyone)
    {
        <tr>
            <td>@user.Id</td>
            <td>@user.Email</td>
        </tr>
    }
</table>
```
- Inicie o aplicação e tente acessar /ManageUsers enquanto estiver conectado como um usuário normal. Você verá esta página de acesso negado:
- Isso ocorre porque os usuários não são atribuídos à função Administrador automaticamente.

### Crie uma conta de administrador de teste

- Por razões de segurança óbvias, não é possível que ninguém registre uma nova conta de administrador. Na verdade, a função de Administrador ainda nem existe no banco de dados!

-  Você pode adicionar a função de Administrador mais uma conta de administrador de teste ao banco de dados na primeira vez que o aplicação for iniciado. Adicionar dados pela primeira vez ao banco de dados é chamado de inicialização ou ** propagação ** do banco de dados.

Crie uma nova classe na raiz do projeto chamada ** SeedData.cs **:

```csharp=
using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreTodo
{
    public static class SeedData
    {
        public static async Task InitializeAsync(
            IServiceProvider services)
        {
            var roleManager = services
                .GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = services
                .GetRequiredService<UserManager<ApplicationUser>>();
            await EnsureTestAdminAsync(userManager);
        }
    }
}
```

- O método `InitializeAsync ()` usa um `IServiceProvider` (a coleção de serviços que é configurada no método` Startup.ConfigureServices () `) para obter o` RoleManager` e o `UserManager` da identidade do ASP.NET Core.

- Adicione mais dois métodos abaixo do método `InitializeAsync ()`. Primeiro, o método `EnsureRolesAsync ()`:

```csharp=
private static async Task EnsureRolesAsync(
    RoleManager<IdentityRole> roleManager)
{
    var alreadyExists = await roleManager
        .RoleExistsAsync(Constants.AdministratorRole);
    
    if (alreadyExists) return;

    await roleManager.CreateAsync(
        new IdentityRole(Constants.AdministratorRole));
}
```
- Este método verifica se existe uma função de `Administrador` no banco de dados. Se não, ele cria um. Em vez de digitar repetidamente a string `" Administrador "`, crie uma pequena classe chamada ** Constants.cs ** para conter o valor:
```csharp=
namespace AspNetCoreTodo
{
    public static class Constants
    {
        public const string AdministratorRole = "Administrator";
    }
}
```

> Se desejar, você pode atualizar o `ManageUsersController` para usar este valor constante também.

Em seguida, escreva o método `EnsureTestAdminAsync ()` em ** SeedData.cs **:


```csharp=
private static async Task EnsureTestAdminAsync(
    UserManager<ApplicationUser> userManager)
{
    var testAdmin = await userManager.Users
        .Where(x => x.UserName == "admin@todo.local")
        .SingleOrDefaultAsync();

    if (testAdmin != null) return;

    testAdmin = new ApplicationUser
    {
        UserName = "admin@todo.local",
        Email = "admin@todo.local"
    };
    await userManager.CreateAsync(
        testAdmin, "NotSecure123!!");
    await userManager.AddToRoleAsync(
        testAdmin, Constants.AdministratorRole);
}
```
- Se ainda não houver um usuário com o nome de usuário admin@todo.local no banco de dados, este método irá criar um e atribuir uma senha temporária.
- Após fazer o login pela primeira vez, você deve alterar a senha da conta para algo seguro! Em seguida, você precisa dizer ao seu aplicação para executar esta lógica quando for inicializado. Modifique Program.cs e atualize `Main()` para chamar um novo método, `InitializeDatabase()` em **Program.cs**:
```csharp=
using Microsoft.Extensions.DependencyInjection;

public static void Main(string[] args)
{
    var host = BuildWebHost(args);
    InitializeDatabase(host);
    host.Run();
}
``` 

- Em seguida, adicione o novo método à classe abaixo de `Main ()`:
```csharp=
private static void InitializeDatabase(IWebHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            SeedData.InitializeAsync(services).Wait();
        }
        catch (Exception ex)
        {
            var logger = services
                .GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Error occurred seeding the DB.");
        }
    }
}
```
- Este método obtém a coleção de serviços que `SeedData.InitializeAsync()` precisa então executa o método para propagar o banco de dados. Se algo der errado, um erro será registrado.

> Como `InitializeAsync ()` retorna uma `Task`, o método` Wait () `deve ser usado para garantir que ele termine antes que o aplicação seja inicializado. Você normalmente usaria `await` para isso, mas por razões técnicas você não pode usar` await` na classe `Program`. Esta é uma rara exceção. Você deve usar `await` em qualquer outro lugar!

Da próxima vez que você iniciar o aplicação, a conta `admin @ todo.local` será criada e atribuída a função de Administrador. Tente fazer login com esta conta e navegar para `http: // localhost: 5000 / ManageUsers`. Você verá uma lista de todos os usuários registrados para o aplicação.

> Como um desafio extra, tente adicionar mais recursos de administração a esta página. Por exemplo, você pode adicionar um botão que dá ao administrador a capacidade de excluir uma conta de usuário.

### Verifique a autorização em uma vista

- O atributo `[Autorizar]` facilita a execução de uma verificação de autorização em um controlador ou método de ação, mas e se você precisar verificar a autorização em uma visualização? Por exemplo, seria bom exibir um link "Gerenciar usuários" na barra de navegação se o usuário conectado for um administrador.
- Você pode injetar o `UserManager` diretamente em uma visualização para fazer esses tipos de verificações de autorização. Para manter suas visualizações limpas e organizadas, crie uma nova visualização parcial que adicionará um item à barra de navegação no layout em ** Views/Shared/_AdminActionsPartial.cshtml **:
```csharp=
@using Microsoft.AspNetCore.Identity
@using AspNetCoreTodo.Models

@inject SignInManager<ApplicationUser> signInManager
@inject UserManager<ApplicationUser> userManager

@if (signInManager.IsSignedIn(User))
{
    var currentUser = await userManager.GetUserAsync(User);

    var isAdmin = currentUser != null
        && await userManager.IsInRoleAsync(
            currentUser,
            Constants.AdministratorRole);

    if (isAdmin)
    {
        <ul class="nav navbar-nav navbar-right">
            <li>
                <a asp-controller="ManageUsers" 
                   asp-action="Index">
                   Manage Users
                </a>
            </li>
        </ul>
    }
}
```
> É convencional nomear visualizações parciais compartilhadas começando com um sublinhado `_`, mas não é obrigatório.

- Esta view parcial usa primeiro o `SignInManager` para determinar rapidamente se o usuário está logado. Se não estiver, o resto do código da visão pode ser pulado. Se ** houver ** um usuário logado, o `UserManager` é usado para consultar seus detalhes e realizar uma verificação de autorização com` IsInRoleAsync() `. Se todas as verificações forem bem-sucedidas e o usuário for um administrador, um link ** Gerenciar usuários ** é adicionado à barra de navegação.

- Para incluir este parcial no layout principal, edite `_Layout.cshtml` e adicione-o na seção navbar em **Views/Shared/_Layout.cshtml**:
```
<div class="navbar-collapse collapse">
    <ul class="nav navbar-nav">
        <!-- existing code here -->
    </ul>
    @await Html.PartialAsync("_LoginPartial")
    @await Html.PartialAsync("_AdminActionsPartial")
</div>
```
- Ao fazer login com uma conta de administrador, você verá um novo item no canto superior direito:
![Manage Users link](manage-users.png)

### Mais recursos 
> O ASP.NET Core Identity ajuda você a adicionar recursos de segurança e identidade, como login e registro ao seu aplicação. Os novos modelos dotnet oferecem visualizações e controladores pré-construídos que lidam com esses cenários comuns para que você possa começar a trabalhar rapidamente. 
> Há muito mais que o ASP.NET Core Identity pode fazer, como redefinição de senha e login social. 
 #### Alternativas para ASP.NET Core Identity
> ASP.NET Core Identity não é a única maneira de adicionar funcionalidade de identidade. Outra opção é um serviço de identidade hospedado na nuvem, como por exemplo o AzureActive Directory B2C ou Okta, para lidar com a identificação da sua aplicação. 
- Você pode pensar nessas opções como parte de uma progressão: Segurança do tipo faça você mesmo: Não recomendado, a menos que você seja um especialista em segurança! 
- Identidade ASP.NET Core: Você obtém muitos códigos de graça com os modelos, o que o torna muito fácil de iniciar. 
- Você ainda precisará escrever algum código para cenários mais avançados e manter um banco de dados para armazenar informações do usuário. 
- Serviços de identidade hospedados em nuvem. O serviço lida com cenários simples e avançados (autenticação multifator, recuperação de conta, federação) e reduz significativamente a quantidade de código que você precisa escrever e manter em seu aplicação. 


 # Comandos: Usando o Git ou GitHub 
  - **Por segurança e facilidade de compartilhamento, entre outras funcionalidades é utilizado o Github, além disso ele serve como o seu curriculo de programador;**
  - 'cd ..' saia da pasta do projeto;
  - 'git init' inicia um novo repositório na pasta raiz do projeto. Caso ocorra erro volte nos dowloads e baixe e configure o Git Bash. Ele deve criar uma pasta .git.

# Termos:
  - **AddSingleton** adiciona seu serviço ao contêiner de serviço como um singleton. Isso significa que apenas uma cópia do  da classe FakeTodoItemService é criada e é reutilizada sempre que o serviço é solicitado.
  - **Arquitetura n-tier**: A maioria dos projetos maiores usa uma arquitetura de três camadas: uma camada de apresentação, uma camada de lógica de serviço e uma camada de repositório de dados. Um repositório é uma classe que é focada apenas no código do banco de dados (sem lógica de negócios). Neste aplicação, você os combinará em uma única camada de serviço por simplicidade, mas fique à vontade para experimentar diferentes maneiras de arquitetar o código.
  - **Authentication (Autenticação)** e **authorization (autorização)** são ideias distintas que costumam ser confundidas. A autenticação trata se um usuário está conectado, enquanto a autorização trata do que ele pode fazer após o login. Você pode pensar na autenticação como uma pergunta: "Eu sei quem é esse usuário?" Enquanto a autorização pergunta: "Este usuário tem permissão para fazer X?"
  - **Booleano** (valor verdadeiro / falso), Por padrão, será falso para todos os novos itens. Posteriormente, pode-se mudar essa propriedade para true quando o usuário clicar na caixa de seleção de um item na visualização.
  - **get; set; ou (getter e setter)** leitura / gravação.
  - **Guids (orGUIDs)** são longas sequências de letras e números, como 43ec09f2-7f70-4f4b-9559-65011d5781bb. Como os guias são aleatórios e é improvável que sejam duplicados acidentalmente, eles são comumente usados como IDs únicos. Você também pode usar um número (inteiro) como ID da entidade do banco de dados, mas precisará configurar seu banco de dados para sempre aumentar o número quando novas linhas forem adicionadas ao banco de dados.
  - **DueAt** é um DateTimeOffset, que é um tipo de C# que armazena um carimbo de data/hora junto com um deslocamento de fuso horário do UTC. Armazenar o deslocamento de data, hora e fuso horário juntos facilita o agendamento de datas com precisão em sistemas em fusos horários diferentes. Além disso temos o "?" ponto de interrogação após o tipo DateTimeOffset ? Essa marca a propriedade DueAt como anulável ou opcional. Se o "?" não foi incluído, todos os itens de pendências precisam ter uma data de vencimento.
  - **mapeador objeto-relacional (ORM)** torna mais fácil escrever códigos que interagem com um banco de dados, adicionando uma camada de abstração entre seu código e o próprio banco de dados. Hibernate em Java e ActiveRecord inRuby são dois ORMs bem conhecidos.Ainda existem vários ORMs para .NET, incluindo um criado pela Microsoft e incluído no ASP.NET Core por padrão: Entity Framework Core. EntityFramework Core torna mais fácil conectar-se a vários tipos de bancos de dados diferentes e permite usar o código C# para criar consultas de banco de dados que são mapeadas de volta para modelos C# (POCOs).
  - **Migrations** controlam as mudanças na estrutura do banco de dados ao longo do tempo, possibilitando reverter um conjunto de mudanças, ou criar um segundo banco de dados com a mesma estrutura do primeiro. Com as migrations, você tem um histórico completo de modificações, como adicionar ou remover colunas (e tabelas inteiras);
  - O **model binding** analisa os dados em uma solicitação e tenta combinar de forma inteligente os campos de entrada com as propriedades no modelo. Em outras palavras, quando o usuário envia este formulário e seus POSTs de navegador para essa ação, o ASP.NET Core irá obter as informações do formulário e colocá-las na variável newItem. Durante a vinculação do modelo, todas as propriedades do modelo que não podem ser combinadas com os campos da solicitação são ignoradas. Uma vez que o formulário inclui apenas um elemento de entrada Title, você pode esperar que as outras propriedades onTodoItem (o sinalizador IsDone, a data DueAt) estarão vazias ou conterão valores padrões.
  - **Required** informa que o campo é obrigatório ao ASP.NET Core que essa sequência não pode ser nula ou vazia.
  - **Strings** em C# são sempre anuláveis, portanto, não há necessidade de marca-lás como anulável. As strings C # podem ser nulas, vazias ou conter texto.
  - **SQLite** é um gerenciador banco de dados leve que não exige nenhuma instalação de ferramenta para pode ser executado
  - **Tag Helpers(tags de ajuda)**: Antes que a visualização seja renderizada, o ASP.NET Cor substitui esses auxiliares de tag por atributos HTML reais, onde o ASP.NET Core o gera para você automaticamente. Exemplos: Os atributos asp-controller e asp-action no elemento <a>.
  - **Using** são instruções que se encontrão na parte superior do arquivo para importaras informações de outras classes, e evitar mensagens de erros como: "The type or namespace name 'TodoItem' could not be found (are you missing a using directive or an assembly reference?)".
  -  **Parcial View ** é uma pequena parte de uma visualização maior que fica em um arquivo separado.
  - O método **Where** é um recurso do C # denominado LINQ (language integratedquery), que se inspira na programação funcional e facilita a expressão de consultas de banco de dados em código. Sob o capô, Entity Framework Core traduz o método Where em uma instrução como **SELECT * FROM Items WHERE IsDone = 0**, ou um documento de consulta equivalente em um banco de dados NoSQL.
 
 
 
 
 
  
 
 
  
 

