using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class BackCommand : CommandBase
    {
        private ToDoApplication mApplication;

        public BackCommand(ToDoApplication todoapplication) : base("Back")
        {
            mApplication = todoapplication;
        }

        public override void Execute()
        {
            mApplication.CommandStack.Pop();
        }
    }
}
