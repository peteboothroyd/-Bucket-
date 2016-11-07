using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class CreateTaskCommand : CommandBase
    {
        private User mUser;
        private string mName;
        private string mDescription;
        private string mTags;
        private int mToDoIdCounter;

        public CreateTaskCommand(User user, string name, string description, string tags)
            : base("Paste")
        {
            mUser = user;
            mName = name;
            mDescription = description;
            mTags = tags;
            mToDoIdCounter = user.ToDoIDCounter;
        }

        public override void Execute()
        {
            mUser.Tasks.Add(new Task(mName, mDescription, mTags, ref mToDoIdCounter));
            mUser.ToDoIDCounter = mToDoIdCounter;
        }
    }
}
