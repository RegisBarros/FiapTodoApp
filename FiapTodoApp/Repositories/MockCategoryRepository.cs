using FiapTodoApp.Models;
using FiapTodoApp.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapTodoApp.Repositories
{
    public class MockCategoryRepository : Repository<Category>
    {
        private static readonly Lazy<MockCategoryRepository> _instance =
                new Lazy<MockCategoryRepository>(() => new MockCategoryRepository());

        public static MockCategoryRepository Instance { get { return _instance.Value; } }

        public override async Task LoadAll()
        {
            if (Items.Count == 0)
            {
                var categories = new List<Category>()
                {
                new Category {Color = "Black", Description = "Pessoal", Id = Guid.NewGuid().ToString() },
                new Category {Color = "Red", Description = "Família", Id = Guid.NewGuid().ToString() },
                new Category {Color = "Lime", Description = "Trabalho", Id = Guid.NewGuid().ToString() },
                };

                categories.ForEach(c => Items.Add(c));
            }
        }

        public override async Task Create(Category entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            Items.Add(entity);
        }

        public override async Task Update(Category entity)
        {
            var category = Items.SingleOrDefault(c => c.Id == entity.Id);

            if (category != null)
            {
                category.Color = entity.Color;
                category.Description = entity.Description;
            }
        }

        public override async Task Delete(Category entity)
        {
            var category = Items.SingleOrDefault(c => c.Id == entity.Id);

            if (category != null)
            {
                Items.Remove(category);
            }
        }

    }
}
