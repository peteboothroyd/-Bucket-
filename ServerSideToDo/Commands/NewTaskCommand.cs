using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class NewTaskCommand : CommandBase
    {
        private User mUser;

        public NewTaskCommand(User user)
            : base("New")
        {
            mUser = user;
        }

        public override void Execute()
        {
            int mToDoCount  = mUser.ToDoIDCounter;
            mUser.Tasks.Add(new Task(mUser.Tasks.Count,ref mToDoCount));
            mUser.ToDoIDCounter = mToDoCount;
        }

        public int GetId()
        {
            return mUser.ToDoIDCounter - 1;
        }

        public int GetSortOrder()
        {
            return mUser.Tasks.Count;
        }
    }
}
