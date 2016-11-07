using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ToDoTrainingGit.Commands
{
    class NewTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;

        public NewTaskCommand(MainWindow mainwindow)
            : base("New")
        {
            mMainWindow = mainwindow;
        }

        public override void Execute()
        {
            try
            {
                mMainWindow.Hub.Invoke("ClientNewTaskRequest").Wait();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
