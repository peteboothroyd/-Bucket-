using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoTrainingGit.Checks
{
    class PresenceCheck : CheckBase
    {
        private User mUser;
        private string mPossibleName;

        public PresenceCheck(User user, string PossibleName)
            : base("Presence Check")
        {
            mUser = user;
            mPossibleName = PossibleName;
        }

        public override bool Execute()
        {
            int count = mUser.Tasks.Count<ToDoTrainingGit.Task>(p => p.Name == mPossibleName);

            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }      
    }
}
