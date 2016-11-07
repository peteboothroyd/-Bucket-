using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    public abstract class CommandBase
    {
        private readonly string mName;

        public CommandBase(string name)
        {
            mName = name;
        }

        public string Name
        {
            get { return mName; }
        }

        public abstract void Execute();
    }
}
