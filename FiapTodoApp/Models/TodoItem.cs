using FiapTodoApp.Abstracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapTodoApp.Models
{
    public class TodoItem : NotifyableClass
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private string _details;

        public string Details
        {
            get { return _details; }
            set { Set(ref _details, value); }
        }

        private DateTime? _startDate;

        public DateTime? StartDate
        {
            get { return _startDate; }
            set { Set(ref _startDate, value); }
        }

        private Appointment _appointment;

        public Appointment Appointment
        {
            get { return _appointment; }
            set { Set(ref _appointment, value); }
        }

        private string _location;

        public string Location
        {
            get { return _location; }
            set { Set(ref _location, value); }
        }

        private bool? _isComplete;

        public bool? IsComplete
        {
            get { return _isComplete ?? false; }
            set { Set(ref _isComplete, value); }
        }

        private string _categoryId;
        public string CategoryId
        {
            get
            {
                if (Category != null)
                    return Category.Id;

                return _categoryId;
            }
            set { Set(ref _categoryId, value); }
        }

        private Category _category;

        [JsonIgnore]
        public Category Category
        {
            get { return _category; }
            set { Set(ref _category, value); }
        }

        private byte[] _picture;

        public byte[] Picture
        {
            get { return _picture; }
            set { Set(ref _picture, value); }
        }

        private bool _isInkPicture;
        public bool IsInkPicture
        {
            get { return _isInkPicture; }
            set { _isInkPicture = value; }
        }
    }
}
