using System;
using Xunit;
using TodoClassLib;
using System.Linq;

namespace TodoListTest
{
    public class TedoMemoryRepositoryTest
    {
        ITodoRepository GetRepo()
        {
            var repo= new TodoMemoryRepository();
            repo.Clear();
            return repo;
        }

        [Fact]
        public void CreateTest()
        {
            var repo = GetRepo();

            Assert.False(repo.Create(null));//null is not allowed and can not be created

            //Id is Auto-Increment
            Assert.Throws<InvalidOperationException>(() =>
            repo.Create(new Todo()
            {
                Id = 1234
            }));

            //Valid TodoItem
            Assert.True(repo.Create(new Todo()
            {
                Title = "myTitle",
            }));

            //Todo Items need to have a title
            Assert.False(repo.Create(new Todo()));
            Assert.False(repo.Create(new Todo()
            {
                Title = ""
            }));
            Assert.False(repo.Create(new Todo()
            {
                Title = " "
            }));

            //Due Date must be after created date
            Assert.False(repo.Create(new Todo()
            {
                Created = DateTime.UtcNow.AddDays(1),
                Due = DateTime.UtcNow,
                Title = "asdf"
            }));

            //After create must have ID
            var todoitem = new Todo()
            {
                Title = "myTitle",
            };
            repo.Create(todoitem);
            Assert.True(todoitem.Id!=0);

        }
        [Fact]
        public void ReadTest()
        {
            var repo = GetRepo();
            Assert.Null(repo.Read(-10));
            Assert.Null(repo.Read(10000));
            var item = new Todo()
            {
                Title = "asdf"
            };
            repo.Create(item);
            var readItem = repo.Read(item.Id);
            Assert.NotNull(readItem);
            Assert.Equal(item, readItem);
            Assert.Equal(item.Created, readItem.Created);
            Assert.Equal(item.Due, readItem.Due);
            Assert.Equal(item.Id, readItem.Id);
            Assert.Equal(item.IsDone, readItem.IsDone);
            Assert.Equal(item.Title, readItem.Title);



        }

        [Fact]
        public void DeleteTest()
        {
            var repo = GetRepo();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                repo.Delete(0);
            });
            var item = new Todo()
            {
                Title = "hi"
            };
            Assert.True(repo.Create(item));
            repo.Delete(item.Id);
            Assert.Null(repo.Read(item.Id));
        }

        [Fact]
        public void UpdateTest()
        {
            var repo = GetRepo();
            var item = new Todo()
            {
                Title = "asdf",
            };
            repo.Create(item);

            var item1 = new Todo()
            {
                Id = item.Id,
                Title = "asdf1",
            };
            Assert.True(repo.Update(item1));

            Assert.Equal("asdf1", repo.Read(item.Id).Title);
            Assert.False(repo.Update(new Todo()
            {
                Title = "",
                Id = item.Id,
            }));

            Assert.False(repo.Update(new Todo()
            {
                Title = "asdf",
                Id = 123456,
            }));
        }

        [Fact]
        public void GetAllTests()
        {            
            var repo = GetRepo();
            var repoCount = repo.ReadAll().Count();

            var item0 = new Todo()
            {
                Title = "asdf",
            };
            var item1 = new Todo()
            {
                Title = "asdf1",
            };
            var item2 = new Todo();

            repo.Create(item0);
            repo.Create(item1);
            repo.Create(item2);

            Assert.Equal(repoCount+2, repo.ReadAll().Count());
        }
    }
}
