using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoTrainingGit.Checks
{
    public abstract class CheckBase
    {
        private readonly string mName;

        public CheckBase(string name)
        {
            mName = name;
        }

        public string Name
        {
            get { return mName; }
        }

        public abstract bool Execute();
    }
}
