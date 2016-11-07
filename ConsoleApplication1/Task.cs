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

        private void RaisePropertyChanged(PropertyChangedEventArgs e)
        {
            var evt = PropertyChanged;
            if (evt != null)
                evt(this, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
