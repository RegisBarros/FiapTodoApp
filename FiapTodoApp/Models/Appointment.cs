using FiapTodoApp.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapTodoApp.Models
{
    public class Appointment : NotifyableClass
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private double _reminder;

        public double Reminder
        {
            get { return _reminder; }
            set { Set(ref _reminder, value); }
        }
    }

}
