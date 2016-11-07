using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class MoveTaskCommand : CommandBase
    {
        private User mUser;
        private bool mDirection;
        private int mRandomID;

        public MoveTaskCommand(User user, int RandomID, bool Direction)
            : base("Move")
        {
            mUser = user;
            mDirection = Direction;
            mRandomID = RandomID;
        }

        public override void Execute()
        {

            ToDoTrainingGit.Task taskItem = mUser.Tasks.FirstOrDefault<ToDoTrainingGit.Task>(task => task.ID == mRandomID);

            var orderedTodos = mUser.Tasks.OrderBy(p => p.SortOrder);
            int count = 0;

            foreach (Task todo in orderedTodos)
            {
                todo.SortOrder = count;
                count++;
            }

            int oldPosition = taskItem.SortOrder;
            int newPosition = 0;
            int directionInt = 0;

            if (mDirection)
            {
                newPosition = oldPosition - 1;
                directionInt = 1;
                taskItem.SortOrder = newPosition;
            }

            else
            {
                newPosition = oldPosition + 1;
                directionInt = -1;
                taskItem.SortOrder = newPosition;
            }

            foreach (Task todo in mUser.Tasks)
            {
                if (todo.SortOrder == newPosition && todo.Name != taskItem.Name)
                {
                    todo.SortOrder = todo.SortOrder + directionInt;
                }
            }
        }
    }
}
