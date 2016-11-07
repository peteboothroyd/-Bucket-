using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ToDoTrainingGit.Commands
{
    class ClearTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;

        public ClearTaskCommand(MainWindow MainWindow)
            : base("Clear")
        {
            mMainWindow = MainWindow;
        }

        public override void Execute()
        {
            var todos = mMainWindow.Tasks;
            string messageBoxText = "Are you sure you want to clear all tasks?";
            string caption = "Clear Tasks Warning";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;


            MessageBoxResult rsltMessageBox = MessageBox.Show(messageBoxText, caption, btnMessageBox, icnMessageBox, MessageBoxResult.No);
            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    break;
                case MessageBoxResult.No:
                    return;
            }
            todos.Clear();
        }
    }
}
