 using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;
 using AspNetCoreTodo.Services;
 using Microsoft.AspNetCore.Mvc;
 using AspNetCoreTodo.Models;

  namespace AspNetCoreTodo.Controllers
  {
    public class TodoController : Controller    
    {
      private readonly ITodoItemService _todoItemService;
      public TodoController(ITodoItemService todoItemService)
      {
                _todoItemService = todoItemService;    
      }
        public async Task<IActionResult> Index()
        {
          var items = await _todoItemService.GetIncompleteItemsAsync();

          var model = new TodoViewModel()    
          {       
             Items = items    
          };
          return View(model);
        }
    }
  }