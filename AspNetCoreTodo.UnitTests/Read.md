# Testes Automatizados
- Testes são uma parte importanre desenvolvimento, pois evitam bugs, e deixa código mais fácil caso haja necessidade de refatoração caso por quebras de funcinalidade ou mesmo a introdução de novos problemas;
- **Teste de Unidade**, são pequenos estes para se poder ter certeza de que um metódo ou pedaço de código funcione adequadamente;
- **Teste de integração**, ou testes funcionais  são testes maiores que simulam a aplicaçao na vida real testando as multiplas layers ou partes da sua aplicaçao;

## Testes de Unidade
- Esses testes são pequenos, e testam o comportamento, de um metódo ou uma classe;
- Quando o código que você está testando depende de outros métodos ou classes, os testes de unidade contam com a simulação dessas outras classes para que o teste se concentre apenas em uma coisa de cada vez;
- Por exemplo, a classe `TodoController` tem duas dependências: um` ITodoItemService` e o `UserManager`. O `TodoItemService`, por sua vez, depende do` ApplicationDbContext`. (A ideia de que você pode desenhar uma linha de `TodoController`>` TodoItemService`> `ApplicationDbContext` é chamada de ** gráfico de dependência **).
- Quando o aplicação é executado normalmente, o contêiner de serviço ASP.NET Core e o sistema de injeção de dependência injeta cada um desses objetos no gráfico de dependência quando `TodoController` ou` TodoItemService` é criado.
- Quando você escreve um teste de unidade, por outro lado, você mesmo precisa lidar com o gráfico de dependência. É comum fornecer versões somente teste ou "simuladas" dessas dependências. Isso significa que você pode isolar apenas a lógica da classe ou método que está testando. (Isso é importante! Se você estiver testando um serviço, não quer ** também ** gravar acidentalmente em seu banco de dados.)

### Criando um projeto de teste
- É uma boa prática criar um projeto separado para seus testes, para que sejam mantidos separados do código do seu aplicação. O novo projeto de teste deve residir em um diretório próximo (não dentro) do diretório do projeto principal.
- Se você está atualmente no diretório do seu projeto, `cd` um nível acima. (Este diretório raiz também será chamado de `AspNetCoreTodo`). Em seguida, use este comando para criar um novo projeto de teste:

```
dotnet new xunit -o AspNetCoreTodo.UnitTests
```
- xUnit.NET é uma estrutura de teste popular para código .NET que pode ser usada para escrever testes de unidade e integração. Como tudo o mais, é um conjunto de pacotes NuGet que podem ser instalados em qualquer projeto. O template `dotnet new xunit` já inclui tudo que você precisa.

- A estrutura de diretório agora deve ser semelhante a esta:
```
AspNetCoreTodo/
    AspNetCoreTodo/
        AspNetCoreTodo.csproj
        Controllers/
        (etc...)

    AspNetCoreTodo.UnitTests/
        AspNetCoreTodo.UnitTests.csproj
```
- Visto que o projeto de teste usará as classes definidas em seu projeto principal, será preciso adicionar uma referência ao projeto `AspNetCoreTodo`:
```
dotnet add reference ../AspNetCoreTodo/AspNetCoreTodo.csproj
```
- Exclua o arquivo UnitTest1.cs que é criado automaticamente.

> Obs. Se estiver usando o Visual Studio Code, pode ser necessário fechar e reabrir a janela do Visual Studio Code para que o autocompletar de código funcione no novo projeto.

### Escrevendo um teste de serviço

Dê uma olhada na lógica no método `AddItemAsync ()` do `TodoItemService`:

```csharp=
public async Task<bool> AddItemAsync(
    TodoItem newItem, ApplicationUser user)
{
    newItem.Id = Guid.NewGuid();
    newItem.IsDone = false;
    newItem.DueAt = DateTimeOffset.Now.AddDays(3);
    newItem.UserId = user.Id;

    _context.Items.Add(newItem);

    var saveResult = await _context.SaveChangesAsync();
    return saveResult == 1;
}
```

- Este método toma uma série de decisões ou suposições sobre o novo item (em outras palavras, executa a lógica de negócios no novo item) antes de realmente salvá-lo no banco de dados:

* A propriedade `UserId` deve ser definida para o ID do usuário
* Novos itens devem estar sempre incompletos (`IsDone = false`)
* O título do novo item deve ser copiado de `newItem.Title`
* Novos itens sempre devem ser entregues em 3 dias a partir de agora

- Imagine se você ou outra pessoa refatorasse o método `AddItemAsync ()` e esquecesse parte desta lógica de negócios. O comportamento do seu aplicativo pode mudar sem que você perceba! Você pode evitar isso escrevendo um teste que verifica se essa lógica de negócios não mudou (mesmo se a implementação interna do método mudar).

> Pode parecer improvável agora que você introduza uma mudança na lógica de negócios sem perceber, mas fica muito mais difícil controlar as decisões e suposições em um projeto grande e complexo. Quanto maior for o seu projeto, mais importante será ter verificações automatizadas para garantir que nada mudou!

Para escrever um teste de unidade que irá verificar a lógica no `TodoItemService`, crie uma nova classe em seu projeto de teste em **AspNetCoreTodo.UnitTests/TodoItemServiceShould.cs**;
```csharp=
using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AspNetCoreTodo.UnitTests
{
    public class TodoItemServiceShould
    {
        [Fact]
        public async Task AddNewItemAsIncompleteWithDueDate()
        {
            // ...
        }
    }
}
```

> Existem muitas maneiras diferentes de nomear e organizar testes, todas com diferentes prós e contras. Eu gosto de postfixar minhas classes de teste com `Deve` para criar uma frase legível com o nome do método de teste, mas sinta-se à vontade para usar seu próprio estilo!

- O atributo `[Fact]` vem do pacote xUnit.NET e marca este método como um método de teste.
- O `TodoItemService` requer um` ApplicationDbContext`, que normalmente está conectado ao seu banco de dados. 
- isso não será usado para testes. Em vez disso, você pode usar o provedor de banco de dados na memória do Entity Framework Core em seu código de teste;
- Como todo o banco de dados existe na memória, ele é apagado toda vez que o teste é reiniciado. E, por ser um provedor Entity Framework Core adequado;
- Em `TodoItemService` use um `DbContextOptionsBuilder` para configurar o provedor de banco de dados na memória e, em seguida, faça uma chamada para` AddItemAsync() `:

 ```csharp= 
  [Fact]
        public async Task AddNewItem()
        {
           var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;

        	// Set up a context (connection to the "DB") for writing
        	using (var context = new ApplicationDbContext(options))
        	{
                var service = new TodoItemService(context);
                var fakeUser = new ApplicationUser
                {
                     Id = "fake-000",
                     UserName = "fake@example.com"
                };

    await service.AddItemAsync(new TodoItem {Title = "Testing?"}, fakeUser);
        }
}
```