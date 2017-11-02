using FiapTodoApp.Models;
using FiapTodoApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FiapTodoApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditTodoItem : Page
    {
        public EditTodoItemViewModel ViewModel { get; } = new EditTodoItemViewModel();

        public EditTodoItem()
        {
            this.InitializeComponent();
            this.Loaded += EditTodoItem_Loaded;
        }

        private async void EditTodoItem_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.Initialize();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var todoItem = JsonConvert.DeserializeObject<TodoItem>(e.Parameter.ToString());

            if (todoItem != null)
            {
                ViewModel.TodoItem = todoItem;
            }
        }
    }
}
