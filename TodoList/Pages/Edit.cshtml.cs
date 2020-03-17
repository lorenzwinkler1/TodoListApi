using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoClassLib;

namespace TodoList.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Todo Item { get; set; }
        private ITodoRepository repo;

        public EditModel(ITodoRepository repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public void OnGet(int? id)
        {
            if (id.HasValue)
            {
                Item = this.repo.Read(id.Value);
            }

            Item = Item == null ? new Todo() : Item;

        }
        public IActionResult OnPost()
        {
            if (Item.Id == 0)
            {
                repo.Create(Item);
            }
            else
            {
                repo.Update(Item);
            }
            return Redirect("/");
        }
    }
}