## Teste de integração

- Comparados aos testes de unidade, os testes de integração são muito maiores em escopo. exercitar toda a pilha de aplicativos. Em vez de isolar uma classe ou método, os testes de integração garantem que todos os componentes de seu aplicativo estejam funcionando juntos de maneira adequada: roteamento, controladores, serviços, código de banco de dados e assim por diante.
- Os testes de integração são mais lentos e mais complexos do que os testes de unidade, portanto, é comum que um projeto tenha muitos testes de unidade pequenos, mas apenas alguns testes de integração.
- Para testar a pilha inteira (incluindo o roteamento do controlador), os testes de integração normalmente fazem chamadas HTTP para seu aplicativo, como um navegador da web faria.
- Para realizar um teste de integração, você pode iniciar seu aplicativo e fazer solicitações manualmente para http: // localhost: 5000. No entanto, o ASP.NET Core oferece uma alternativa melhor: a classe `TestServer`. Essa classe pode hospedar seu aplicativo durante o teste e, em seguida, interrompê-lo automaticamente quando o teste for concluído.

### Crie um projeto de teste

- Se você está atualmente no diretório do seu projeto, `cd` um nível acima para o diretório raiz` AspNetCoreTodo`. Use este comando para criar um novo projeto de teste:`dotnet new xunit -o AspNetCoreTodo.IntegrationTests`
- Sua estrutura de diretório agora deve ser semelhante a esta:

```
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
