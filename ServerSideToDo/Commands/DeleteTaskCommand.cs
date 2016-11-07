using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class DeleteTaskCommand : CommandBase
    {
        private User mUser;
        private int mRandomID;

        public DeleteTaskCommand(User user, int RandomID)
            : base("Delete")
        {
            mUser = user;
            mRandomID = RandomID;
        }

        public override void Execute()
        {
            int mIndex = mUser.Tasks.FindIndex(task => task.ID == mRandomID);
            mUser.Tasks.RemoveAt(mIndex);
        }
    }
}
