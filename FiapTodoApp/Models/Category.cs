using FiapTodoApp.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapTodoApp.Models
{
    public class Category : NotifyableClass
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        private string _color;

        public string Color
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_color))
                {
                    return "Transparent";
                }

                return _color;
            }
            set { Set(ref _color, value); }
        }
    }
}
