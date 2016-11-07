using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ToDoTrainingGit.Commands
{
    class SaveFileCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private string mPath;

        public SaveFileCommand(MainWindow MainWindow, string Path)
            : base("Export")
        {
            mMainWindow = MainWindow;
            mPath = Path;
        }

        public override void Execute()
        {
            var todos = mMainWindow.Tasks;

            XDocument mExportedDoc = new XDocument();
            XElement xmlTree = new XElement("root");

            foreach (Task todo in todos)
            {
                if (String.IsNullOrWhiteSpace(todo.Name))
                    todo.Name = "";
                if (String.IsNullOrWhiteSpace(todo.Description))
                    todo.Description = "";
                if (String.IsNullOrWhiteSpace(todo.Tags))
                    todo.Tags = "";
                if (String.IsNullOrWhiteSpace(todo.Marked.ToString()))
                    todo.Marked = false;

                XElement task = new XElement("Task",
                new XAttribute("name", todo.Name),
                new XAttribute("description", todo.Description),
                new XAttribute("marked", todo.Marked.ToString()),
                new XAttribute("tags", todo.Tags)
                );
                xmlTree.Add(task);
            }

            mExportedDoc.Add(xmlTree);
            //root.ReplaceWith(xmlTree);
            mExportedDoc.Save(mPath);
        }
    }
}
