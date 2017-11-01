using FiapTodoApp.Abstracts;
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
using Windows.UI.Xaml.Input;

namespace FiapTodoApp.ViewModels
{
    public class MainPageViewModel : NotifyableClass
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

        private bool _isSplitViewOpen;

        public bool IsSplitViewOpen
        {
            get { return _isSplitViewOpen; }
            set { Set(ref _isSplitViewOpen, value); }
        }

        public void HamburguerButton_Click()
        {
            IsSplitViewOpen = !IsSplitViewOpen;
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

        public async void RemoveTodoItem_Click()
        {
            if (_selectedDeleteTodoItem != null)
            {
                await TodoItemRepository.Delete(_selectedDeleteTodoItem);

                _selectedDeleteTodoItem = null;
            }
        }

        private TodoItem _selectedDeleteTodoItem;
        public void TodoItems_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            _selectedDeleteTodoItem = ((FrameworkElement)e.OriginalSource).DataContext as TodoItem;
        }
    }
}
