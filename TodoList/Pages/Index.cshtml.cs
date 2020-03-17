using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TodoClassLib;

namespace TodoList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        readonly ITodoRepository repo;
        public IEnumerable<Todo> Todos { get; set; }

        public IndexModel(ITodoRepository repo, ILogger<IndexModel> logger)
        {
            _logger = logger;
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public void OnGet()
        {
            Todos = repo.ReadAll();
        }
    }
}
