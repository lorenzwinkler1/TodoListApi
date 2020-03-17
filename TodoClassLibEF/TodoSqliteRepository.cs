using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TodoClassLib;
using Microsoft.EntityFrameworkCore;

namespace TodoClassLibEF
{
    public class TodoSqliteRepository : ITodoRepository
    {
        private DataContext context;

        public TodoSqliteRepository(DataContext context)
        {
            this.context = context;
        }

        public bool Create(Todo todoItem, bool save = true)
        {
            if (todoItem == null)
                return false;
            if (todoItem.Id != 0)
                throw new InvalidOperationException("Id must not be set when creating a new item");
            if (String.IsNullOrWhiteSpace(todoItem.Title))
                return false;
            if (DateTime.Compare(todoItem.Created, todoItem.Due) > 0)
                return false;


            context.Todos.Add(todoItem);
            if (save)
                context.SaveChanges();
            return true;
        }

        public void Delete(int id, bool save = true)
        {
            var todoitem = context.Todos.FirstOrDefault(item => item.Id == id);
            if (todoitem != null)
            {
                context.Todos.Remove(todoitem);

                if (save)
                    context.SaveChanges();
            }
        }

        public Todo Read(int id)
        {
            var item = context.Todos.AsNoTracking().FirstOrDefault(item => item.Id == id);

            return item;
        }

        public IEnumerable<Todo> ReadAll()
        {
            return context.Todos.ToList();
        }

        public bool Update(Todo itemToUpdate)
        {
            if (String.IsNullOrWhiteSpace(itemToUpdate.Title) || !context.Todos.Any(item => item.Id == itemToUpdate.Id))
                return false;

            context.Todos.Update(itemToUpdate);
            context.SaveChanges();

            return true;
        }
    }
}
