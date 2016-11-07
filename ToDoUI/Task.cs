using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoTrainingGit
{
    public class Task : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Marked { get; set; }
        public int SortOrder { get; set; }
        public string Tags { get; set; }
        public bool IsViewable { get; set; }


        public Task()
        {
            this.IsViewable = true;
            this.SortOrder = 0;
            this.Tags = "";
        }

        public Task(int sortOrder)
        {
            this.IsViewable = true;
            this.SortOrder = sortOrder;
            this.Tags = "";
        }

        public Task(string name, string description)
        {
            this.Name = name;
            this.Description = description;
            this.Marked = false;
            this.IsViewable = true;
            this.SortOrder = 0;
            this.Tags = "";
        }

        public Task(string name, string description, bool marked)
        {
            this.Name = name;
            this.Description = description;
            this.Marked = marked;
            this.IsViewable = true;
            this.SortOrder = 0;
            this.Tags = "";
        }

        public Task(string name, string description, string tags, bool marked)
        {
            this.Name = name;
            this.Description = description;
            this.Marked = marked;
            this.IsViewable = true;
            this.SortOrder = 0;
            this.Tags = tags;
        }

        public Task(string name, string description, string tags)
        {
            this.Name = name;
            this.Description = description;
            this.IsViewable = true;
            this.SortOrder = 0;
            this.Tags = tags;
        }

        private void RaisePropertyChanged(PropertyChangedEventArgs e)
        {
            var evt = PropertyChanged;
            if (evt != null)
                evt(this, e);
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
