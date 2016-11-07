using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoTrainingGit.Checks
{
    class ListCountGreaterThanZeroCheck : CheckBase
    {
        private User mUser;

        public ListCountGreaterThanZeroCheck(User user)
            : base("Presence Check")
        {
            mUser = user;
        }

        public override bool Execute()
        {
            if (mUser.Tasks.Count > 0)
            {
                return true;
            }
            else return false;
        }  
    }
}
