using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ToDoTrainingGit.Commands
{
    class SaveFileCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private string mPath;

        public SaveFileCommand(MainWindow mainwindow, string path)
            : base("Save")
        {
            mMainWindow = mainwindow;
            mPath = path;
        }

        public override void Execute()
        {
            var todos = mMainWindow.Tasks;
            var orderedTodos = todos.OrderBy(p => p.SortOrder);

            XDocument mExportedDoc = new XDocument();

            XElement xmlTree = new XElement("root");

            Parallel.ForEach(orderedTodos, todo =>
            {
                if (String.IsNullOrWhiteSpace(todo.Name))
                    todo.Name = "";
                if (String.IsNullOrWhiteSpace(todo.Description))
                    todo.Description = "";
                if (String.IsNullOrWhiteSpace(todo.Tags))
                    todo.Tags = "";
                if (String.IsNullOrWhiteSpace(todo.Marked.ToString()))
                    todo.Marked = false;
                if (String.IsNullOrWhiteSpace(todo.SortOrder.ToString()))
                    todo.SortOrder = 0;
                if (String.IsNullOrWhiteSpace(todo.IsViewable.ToString()))
                    todo.IsViewable = false;

                XElement task = new XElement("Task",
                new XAttribute("name", todo.Name),
                new XAttribute("description", todo.Description),
                new XAttribute("marked", todo.Marked.ToString()),
                new XAttribute("tags", todo.Tags),
                new XAttribute("sortorder", todo.SortOrder),
                new XAttribute("isviewable", todo.IsViewable)
                );
                xmlTree.Add(task);
            });

            mExportedDoc.Add(xmlTree);
            mExportedDoc.Save(mPath);
        }
    }
}
