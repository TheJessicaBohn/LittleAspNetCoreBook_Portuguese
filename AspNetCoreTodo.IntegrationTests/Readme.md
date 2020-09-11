## Teste de integração

- Comparados aos testes de unidade, os testes de integração são muito maiores em escopo. exercitar toda a pilha de aplicativos. Em vez de isolar uma classe ou método, os testes de integração garantem que todos os componentes de seu aplicativo estejam funcionando juntos de maneira adequada: roteamento, controladores, serviços, código de banco de dados e assim por diante.
- Os testes de integração são mais lentos e mais complexos do que os testes de unidade, portanto, é comum que um projeto tenha muitos testes de unidade pequenos, mas apenas alguns testes de integração.
- Para testar a pilha inteira (incluindo o roteamento do controlador), os testes de integração normalmente fazem chamadas HTTP para seu aplicativo, como um navegador da web faria.
- Para realizar um teste de integração, você pode iniciar seu aplicativo e fazer solicitações manualmente para http: // localhost: 5000. No entanto, o ASP.NET Core oferece uma alternativa melhor: a classe `TestServer`. Essa classe pode hospedar seu aplicativo durante o teste e, em seguida, interrompê-lo automaticamente quando o teste for concluído.

### Crie um projeto de teste

- Se você está atualmente no diretório do seu projeto, `cd` um nível acima para o diretório raiz` AspNetCoreTodo`. Use este comando para criar um novo projeto de teste:`dotnet new xunit -o AspNetCoreTodo.IntegrationTests`
- Sua estrutura de diretório agora deve ser semelhante a esta:

```csharp
AspNetCoreTodo/
    AspNetCoreTodo/
        AspNetCoreTodo.csproj
        Controllers/
        (etc...)

    AspNetCoreTodo.UnitTests/
        AspNetCoreTodo.UnitTests.csproj

    AspNetCoreTodo.IntegrationTests/
        AspNetCoreTodo.IntegrationTests.csproj
```
- Pose-se manter os testes de unidade e testes de integração no mesmo projeto. Porém projetos grandes, é comum dividi-los, por isso é fácil executá-los separadamente.
- Como o projeto de teste usará as classes definidas em seu projeto principal, você precisará adicionar uma referência ao projeto principal: `dotnet add reference ../AspNetCoreTodo/AspNetCoreTodo.csproj`
- Você também precisará adicionar o pacote NuGet `Microsoft.AspNetCore.TestHost` com o comando:`dotnet add package Microsoft.AspNetCore.TestHost`
- Agora exclua o arquivo `UnitTest1.cs` criado por` dotnet new`. Você está pronto para escrever um teste de integração.

### Escreva um teste de integração

- Existem algumas coisas que precisam ser configuradas no servidor de teste antes de cada teste. Em vez de bagunçar o teste com este código de configuração, você pode manter essa configuração em uma classe separada. 
- Crie uma nova classe chamada `TestFixture`em **AspNetCoreTodo.IntegrationTests/TestFixture.cs **

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreTodo.IntegrationTests
{
    public class TestFixture : IDisposable  
    {
        private readonly TestServer _server;

        public HttpClient Client { get; }

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<AspNetCoreTodo.Startup>()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "..\\..\\..\\..\\AspNetCoreTodo"));
                    
                    config.AddJsonFile("appsettings.json");
                });

            _server = new TestServer(builder);

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:8888");
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
```
- Esta classe se encarrega de configurar um `TestServer`, e ajudará a manter os testes limpos e organizados.
- Crie uma nova classe chamada `TodoRouteShould`em **AspNetCoreTodo.IntegrationTests/TodoRouteShould.cs **

```csharp
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreTodo.IntegrationTests
{
    public class TodoRouteShould : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public TodoRouteShould(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task ChallengeAnonymousUser()
        {
            // Arrange
            var request = new HttpRequestMessage(
                HttpMethod.Get, "/todo");

            // Act: request the /todo route
            var response = await _client.SendAsync(request);

            // Assert: the user is sent to the login page
            Assert.Equal(
                HttpStatusCode.Redirect,
                response.StatusCode);

            Assert.Equal(
                "http://localhost:8888/Account" +
                "/Login?ReturnUrl=%2Ftodo",
                response.Headers.Location.ToString());
        }
    }
}
```
- Este teste faz uma solicitação anônima (não conectado) à rota `/ todo` e verifica se o navegador é redirecionado para a página de login.
- Este cenário é um bom candidato para um teste de integração, porque envolve vários componentes da aplicação: o sistema de roteamento, o controlador, o fato de o controlador estar marcado com `[Autorizar]` e assim por diante. Também é um bom teste porque garante que você nunca removerá acidentalmente o atributo `[Autorizar]` e tornará a visualização de tarefas acessível a todos.

## Executando o teste

- Execute o teste no terminal com `dotnet test`. Se tudo estiver funcionando bem, você verá uma mensagem de sucesso:
```csharp
Starting test execution, please wait...
 Discovering: AspNetCoreTodo.IntegrationTests
 Discovered:  AspNetCoreTodo.IntegrationTests
 Starting:    AspNetCoreTodo.IntegrationTests
 Finished:    AspNetCoreTodo.IntegrationTests

Total tests: 1. Passed: 1. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 2.0588 Seconds
```

