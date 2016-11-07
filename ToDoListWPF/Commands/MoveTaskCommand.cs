using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace ToDoTrainingGit.Commands
{
    class MoveTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private bool mDirection;

        public MoveTaskCommand(MainWindow mainwindow, bool direction)
            : base("Move")
        {
            mMainWindow = mainwindow;
            mDirection = direction;
        }

        public override void Execute()
        {
            try
            {
                Task markedTask = (Task)mMainWindow.TaskListBox.SelectedItem;

                mMainWindow.Hub.Invoke("ClientMoveTaskRequest", markedTask.ID, mDirection).Wait();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
