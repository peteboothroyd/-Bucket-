using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class PasteTaskCommand : CommandBase
    {
        private User mUser;
        private string mName;
        private string mDescription;
        private bool mMarked;
        private int mSortOrder;
        private string mTags;
        private bool mIsViewable;

        public PasteTaskCommand(User user, string name, string description, bool marked, int sortorder, string tags, bool isviewable)
            : base("Paste")
        {
            mUser = user;
            mName = name;
            mDescription = description;
            mMarked = marked;
            mSortOrder = sortorder;
            mTags = tags;
            mIsViewable = isviewable;
        }

        public override void Execute()
        {
            int mToDoCount = mUser.ToDoIDCounter;
            mUser.Tasks.Add(new Task(mName, mDescription, mMarked, mSortOrder, mTags, mIsViewable, ref mToDoCount));
            mUser.ToDoIDCounter = mToDoCount;
        }
    }
}
