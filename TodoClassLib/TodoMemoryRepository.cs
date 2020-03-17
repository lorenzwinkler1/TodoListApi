using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TodoClassLib
{
    public class TodoMemoryRepository : ITodoRepository
    {
        static private List<Todo> storage = new List<Todo>();

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

            todoItem.Id = storage.Count != 0 ? storage.Select((item) => item.Id).Max() + 1 : 1;

            storage.Add(todoItem);
            return true;
        }

        public void Delete(int id, bool save = true)
        {
            if (storage.Any(item => item.Id == id))
            {
                storage = storage.Where(item => item.Id != id).ToList();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public Todo Read(int id)
        {
            return storage.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Todo> ReadAll()
        {
            return storage;
        }

        public bool Update(Todo itemToUpdate)
        {
            if (String.IsNullOrWhiteSpace(itemToUpdate.Title) || !storage.Any(item => item.Id == itemToUpdate.Id))
                return false;
            var index = storage.FindIndex(item => item.Id == itemToUpdate.Id);
            storage[index] = itemToUpdate;
            return true;
        }

        public void Clear()
        {
            storage.Clear();
        }
    }
}
