using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FiapTodoApp.Abstracts
{
    public abstract class NotifyableClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Set<T>(ref T data, T value, [CallerMemberName]string propertyName = null)
        {
            if (!object.Equals(data, value))
            {
                data = value;
                this.OnPropertyChanged(propertyName);
            }
        }
    }
}
