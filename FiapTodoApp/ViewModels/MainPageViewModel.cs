using FiapTodoApp.Models;
using FiapTodoApp.Pages;
using FiapTodoApp.Repositories;
using FiapTodoApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FiapTodoApp.ViewModels
{
    public class MainPageViewModel
    {
        public MockCategoryRepository CategoryRepository { get; private set; } = MockCategoryRepository.Instance;
        public MockTodoItemRepository TodoItemRepository { get; private set; } = MockTodoItemRepository.Instance;

        public ObservableCollection<Category> Categories => CategoryRepository.Items;
        public ObservableCollection<TodoItem> TodoItems => TodoItemRepository.Items;

        public async Task Initialize()
        {
            await CategoryRepository.LoadAll();
            await TodoItemRepository.LoadAll();
        }

        public void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate<EditTodoItem>(new TodoItem());
        }

        public void TodoItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;

            if (listView.SelectedItem == null)
            {
                return;
            }

            NavigationService.Navigate<EditTodoItem>(listView.SelectedItem);
        }
    }
}
