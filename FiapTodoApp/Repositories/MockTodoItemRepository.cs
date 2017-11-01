using FiapTodoApp.Models;
using FiapTodoApp.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapTodoApp.Repositories
{
    public class MockTodoItemRepository : Repository<TodoItem>
    {
        private static readonly Lazy<MockTodoItemRepository> _instance =
                new Lazy<MockTodoItemRepository>(() => new MockTodoItemRepository());

        public static MockTodoItemRepository Instance { get { return _instance.Value; } }

        private MockCategoryRepository CategoryRepository => MockCategoryRepository.Instance;

        public override async Task LoadAll()
        {
            if (Items.Count == 0)
            {
                var todoItems = new List<TodoItem>()
                {
                    new TodoItem {Id = Guid.NewGuid().ToString(), Category = CategoryRepository.Items[1],
                        Details = "", Title = "Ir a padaria" },
                    new TodoItem {Id = Guid.NewGuid().ToString(), Category = CategoryRepository.Items[2],
                        Details = "Andar 10 - Sala 5", Title = "Reunião com a equipe" },
                    new TodoItem {Id = Guid.NewGuid().ToString(), Category = CategoryRepository.Items[2],
                        Details = "Pendências do projeto", Title = "Ligar para o João" },
                };

                todoItems.ForEach(t => Items.Add(t));
            }
        }

        public override async Task Create(TodoItem entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            Items.Add(entity);
        }

        public override async Task Update(TodoItem entity)
        {
            var todoItem = Items.SingleOrDefault(t => t.Id == entity.Id);

            if (todoItem != null)
            {
                todoItem.Title = entity.Title;
                todoItem.Details = entity.Details;
                todoItem.IsComplete = entity.IsComplete;
                todoItem.StartDate = entity.StartDate;
                todoItem.Appointment = entity.Appointment;
            }
        }

        public override async Task Delete(TodoItem entity)
        {
            var todoItem = Items.SingleOrDefault(c => c.Id == entity.Id);

            if (todoItem != null)
            {
                Items.Remove(todoItem);
            }
        }
    }

}
