using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoClassLib;

namespace TodoList.Pages
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Todo TodoItem { get; set; }
        private ITodoRepository repo;


        public DeleteModel(ITodoRepository repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public void OnGet(int? id)
        {
            if (id.HasValue)
            {
                TodoItem = repo.Read(id.Value);
            }

            TodoItem = TodoItem == null ? new Todo() : TodoItem;
        }
        public IActionResult OnPost()
        {
            if (TodoItem != null)
                repo.Delete(TodoItem.Id);

            return Redirect("/");
        }
    }
}