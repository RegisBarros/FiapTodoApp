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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            set
            {
                Set(ref _todoItem, value);

                StartDate = TodoItem.StartDate;

                if (TodoItem.StartDate.HasValue)
                {
                    StartTime = TodoItem.StartDate.Value.TimeOfDay;
                }
            }
        }

        private DateTime? _startDate;

        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                Set(ref _startDate, value);

                TodoItem.StartDate = _startDate;
            }
        }

        private TimeSpan _startTime;
        public TimeSpan StartTime
        {
            get { return _startTime; }
            set
            {
                Set(ref _startTime, value);

                if (TodoItem.StartDate.HasValue)
                {
                    TodoItem.StartDate = StartDate.Value.Date + _startTime;
                }
            }
        }

        private int _minimumSliderValue;

        public int MinimumSliderValue
        {
            get { return _minimumSliderValue; }
            set { Set(ref _minimumSliderValue, value); }
        }

        public void StartDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (!args.NewDate.HasValue)
            {
                TodoItem.Appointment.Reminder = MinimumSliderValue = -1;
            }
        }

        public void ToggleReminder_Toggled(object sender, RoutedEventArgs e)
        {
            var toggle = (ToggleSwitch)sender;

            if (!toggle.IsOn)
            {
                StartTime = new TimeSpan();
                TodoItem.Appointment.Reminder = MinimumSliderValue = -1;
            }
            else
            {
                TodoItem.Appointment = TodoItem.Appointment ?? new Appointment();
                MinimumSliderValue = 0;
            }
        }



        public async void SaveTodoItemButton_Click()
        {
            if (TodoItems.Any(t => t.Id == TodoItem.Id))
            {
                await ManageAppointment();

                await TodoItemRepository.Update(TodoItem);
            }
            else
            {
                await ManageAppointment();

                await TodoItemRepository.Create(TodoItem);
            }

            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private async Task ManageAppointment()
        {
            if (TodoItem.Appointment != null)
            {
                if (TodoItem.Appointment.Reminder >= 0)
                {
                    if (TodoItem.Appointment.Id > 0)
                    {
                        //TODO:Update Appointment
                    }
                    else
                    {
                        //TODO:Create Appointment
                        TodoItem.Appointment.Id = new Random().Next();
                    }
                }
                else
                {
                    if (TodoItem.Appointment != null && TodoItem.Appointment.Id > 0)
                    {
                        //TODO:Delete Appointment
                    }
                    TodoItem.Appointment = null;
                }
            }
        }

        public async void DeleteTodoItemButton_Click()
        {
            var dialog = new ContentDialog
            {
                Title = "Confirmação",
                Content = "Você confirma a exclusão desse registro? Essa operação não poderá ser desfeita.",
                PrimaryButtonText = "Sim",
                SecondaryButtonText = "Não"
            };

            dialog.PrimaryButtonClick += async (s, e) =>
            {
                if (TodoItem.Appointment != null && TodoItem.Appointment.Id > 0)
                {
                    //TODO:Delete Appointment
                }

                await TodoItemRepository.Delete(TodoItem);

                NavigationService.GoBack();
            };

            await dialog.ShowAsync();
        }
    }
}
