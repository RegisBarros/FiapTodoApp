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

        public IEnumerable<string> CategoryColors => App.AvailableColors;

        public async Task Initialize()
        {
            await CategoryRepository.LoadAll();
            await TodoItemRepository.LoadAll();

            TodoItems.CollectionChanged -= TodoItems_CollectionChanged;
            TodoItems.CollectionChanged += TodoItems_CollectionChanged;
            FilteredTodoItems = TodoItems;
        }
        
        private bool _isSplitViewOpen;

        public bool IsSplitViewOpen
        {
            get { return _isSplitViewOpen; }
            set { Set(ref _isSplitViewOpen, value); }
        }

        private IEnumerable<TodoItem> _filteredTodoItems;
        public IEnumerable<TodoItem> FilteredTodoItems
        {
            get { return _filteredTodoItems; }
            set
            {
                var newValue = value;

                if (SelectedCategory != null)
                {
                    newValue = newValue.Where(t => t.CategoryId == SelectedCategory.Id);
                }

                Set(ref _filteredTodoItems, newValue);
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (Equals(_selectedCategory, value))
                {
                    return;
                }

                Set(ref _selectedCategory, value);

                FilteredTodoItems = TodoItems;
            }
        }

        private Category _selectedEditCategory;
        public Category SelectedEditCategory
        {
            get { return _selectedEditCategory; }
            set { Set(ref _selectedEditCategory, value); }
        }

        public void Category_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            SelectedEditCategory = ((FrameworkElement)e.OriginalSource).DataContext as Category;
        }

        public async void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var category = new Category
            {
                Description = $"Categoria {Categories.Count + 1}",
                Color = App.AvailableColors.Except(Categories.Select(c => c.Color)).First()
            };

            await CategoryRepository.Create(category);
        }


        public void Categories_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var listView = (ListView)sender;

            var category = ((FrameworkElement)e.OriginalSource).DataContext as Category;

            if (SelectedCategory != null && Equals(category, SelectedCategory))
            {
                SelectedCategory = null;
            }
            else
            {
                SelectedCategory = category;
            }
        }

        private void TodoItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FilteredTodoItems = TodoItems;
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
