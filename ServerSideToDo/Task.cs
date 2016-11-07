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
        public int ID { get; private set; }
        public List<string> TagsList { get; set; }

        public Task(ref int toDoIdCounter)
        {
            this.Name = "";
            this.Description = "";
            this.Marked = false;
            this.IsViewable = true;
            this.SortOrder = 0;
            this.Tags = "";
            this.ID = toDoIdCounter;
            this.TagsList = new List<string>();
            toDoIdCounter++;
        }

        public Task(int sortOrder, ref int toDoIdCounter)
        {
            this.Name = "";
            this.Description = "";
            this.Marked = false;
            this.IsViewable = true;
            this.SortOrder = sortOrder;
            this.Tags = "";
            this.ID = toDoIdCounter;
            this.TagsList = new List<string>();
            toDoIdCounter++;
        }

        public Task(string name, string description, ref int toDoIdCounter)
        {
            this.Name = name;
            this.Description = description;
            this.Marked = false;
            this.IsViewable = true;
            this.SortOrder = 0;
            this.Tags = "";
            this.ID = toDoIdCounter;
            this.TagsList = new List<string>();
            toDoIdCounter++;
        }

        public Task(string name, string description, bool marked, ref int toDoIdCounter)
        {
            this.Name = name;
            this.Description = description;
            this.Marked = marked;
            this.IsViewable = true;
            this.SortOrder = 0;
            this.Tags = "";
            this.ID = toDoIdCounter;
            this.TagsList = new List<string>();
            toDoIdCounter++;
        }

        public Task(string name, string description, string tags, bool marked, ref int toDoIdCounter)
        {
            this.Name = name;
            this.Description = description;
            this.Marked = marked;
            this.IsViewable = true;
            this.SortOrder = 0;
            this.Tags = tags;
            this.ID = toDoIdCounter;

            this.TagsList = new List<string>();
            string[] mTags = tags.Split(ToDoHub.sDelemiters);

            foreach(string tag in mTags)
            {
                TagsList.Add(tag);
            }
            toDoIdCounter++;
        }

        public Task(string name, string description, string tags, ref int toDoIdCounter)
        {
            this.Name = name;
            this.Description = description;
            this.IsViewable = true;
            this.SortOrder = 0;
            this.Tags = tags;
            this.Marked = false;
            this.ID = toDoIdCounter;

            this.TagsList = new List<string>();
            string[] mTags = tags.Split(ToDoHub.sDelemiters);

            foreach (string tag in mTags)
            {
                TagsList.Add(tag);
            }
            toDoIdCounter++;
        }

        public Task(string name, string description, bool marked, int sortorder, string tags, bool isviewable, ref int toDoIdCounter)
        {
            this.Name = name;
            this.Description = description;
            this.IsViewable = isviewable;
            this.SortOrder = sortorder;
            this.Tags = tags;
            this.Marked = marked;
            this.ID = toDoIdCounter;

            this.TagsList = new List<string>();
            string[] mTags = tags.Split(ToDoHub.sDelemiters);

            foreach (string tag in mTags)
            {
                TagsList.Add(tag.Trim());
            }
            toDoIdCounter++;
        }

        private void RaisePropertyChanged(PropertyChangedEventArgs e)
        {
            var evt = PropertyChanged;
            if (evt != null)
            {
                evt(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
