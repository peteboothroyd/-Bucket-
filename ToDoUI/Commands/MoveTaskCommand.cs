using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class MoveTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private bool mDirection;

        public MoveTaskCommand(MainWindow MainWindow, bool Direction)
            : base("Move")
        {
            mMainWindow = MainWindow;
            mDirection = Direction;
        }

        public override void Execute()
        {
            var todos = mMainWindow.Tasks;
            var orderedTodos = todos.OrderBy(p => p.SortOrder);
            int count = 0;

            foreach (Task todo in orderedTodos)
            {
                todo.SortOrder = count;
                count++;
            }

            Task movingTask = (Task)mMainWindow.TaskListBox.SelectedItem;

            int oldPosition = movingTask.SortOrder;
            int newPosition = 0;
            int directionInt = 0;

            if (mDirection)
            {
                newPosition = oldPosition - 1;
                directionInt = 1;
                movingTask.SortOrder = newPosition;
            }

            else
            {
                newPosition = oldPosition + 1;
                directionInt = -1;
                movingTask.SortOrder = newPosition;
            }

            foreach (Task todo in todos)
            {
                if (todo.SortOrder == newPosition && todo.Name != movingTask.Name)
                {
                    todo.SortOrder = todo.SortOrder + directionInt;
                }
            }

            var orderedList = todos.OrderBy(p => p.SortOrder);
            var observableCollectionOrderedList = new ObservableCollection<Task>(orderedList);
            todos = observableCollectionOrderedList;
        }
    }
}
