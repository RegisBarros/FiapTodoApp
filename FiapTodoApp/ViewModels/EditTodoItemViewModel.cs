using FiapTodoApp.Abstracts;
using FiapTodoApp.Models;
using FiapTodoApp.Repositories;
using FiapTodoApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapTodoApp.ViewModels
{
    public class EditTodoItemViewModel : NotifyableClass
    {
        public MockTodoItemRepository TodoItemRepository { get; private set; } = MockTodoItemRepository.Instance;
        public ObservableCollection<TodoItem> TodoItems => TodoItemRepository.Items;

        private TodoItem _todoItem;

        public TodoItem TodoItem
        {
            get { return _todoItem; }
            set { Set(ref _todoItem, value); }
        }

        public async void SaveTodoItemButton_Click()
        {
            if (TodoItems.Any(t => t.Id == TodoItem.Id))
            {
                await TodoItemRepository.Update(TodoItem);
            }
            else
            {
                await TodoItemRepository.Create(TodoItem);
            }

            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        public async void DeleteTodoItemButton_Click()
        {
            await TodoItemRepository.Delete(TodoItem);

            NavigationService.GoBack();
        }
    }
}
