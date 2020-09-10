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
}