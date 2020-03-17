using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TodoClassLib;
using TodoListMVC.Models;

namespace TodoListMVC.Controllers
{
    public class TodoController : Controller
    {
        readonly ITodoRepository repository;
        readonly TodoOptions options;

        public TodoController(ITodoRepository repository, IOptions<TodoOptions> options)
        {
            this.options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            this.repository = repository;
        }

        // GET: Todo
        public ActionResult Index()
        {
            ViewData["options"] = this.options;
            return View(this.repository.ReadAll());
        }

        // GET: Todo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Todo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                this.repository.Create(new Todo()
                {
                    Title = collection["Title"],
                    Due = DateTime.Parse(collection["Due"]),
                    Created = DateTime.Now,
                    IsDone = bool.Parse(collection["IsDone"].First())

                }); ;

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Todo/Edit/5
        public ActionResult Edit(int id)
        {
            return View(repository.Read(id));
        }

        // POST: Todo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                repository.Update(new Todo()
                {
                    Id=id,
                    Title = collection["Title"],
                    Due = DateTime.Parse(collection["Due"]),
                    Created = DateTime.Parse(collection["Created"]),
                    IsDone = bool.Parse(collection["IsDone"].First())
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(repository.Read(id));
            }
        }

        // GET: Todo/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repository.Read(id));
        }

        // POST: Todo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                repository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}