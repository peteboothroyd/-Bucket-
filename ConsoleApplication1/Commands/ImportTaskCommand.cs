using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ToDoTrainingGit.Commands
{
    class ImportTaskCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public ImportTaskCommand(ToDoApplication toDoApplication)
            : base("Import")
        {
            mApplication = toDoApplication;
        }

        public override void Execute()
        {
            if (Check.ListCountGreaterZeroCheck(mApplication))
            {
                Console.WriteLine("Cannot import when todo list is not empty.");
                return;
            }

            var taskName = Check.AskQuestion("Type in name of file to import from:", Check.XMLFileCheck);
            if (Check.ExitCheck(taskName))
                return;

            try
            {
                XDocument mImportedDoc = XDocument.Load(taskName + ToDoApplication.FILE_EXTENSION);

                Parallel.ForEach(mImportedDoc.Root.Elements(), node =>
               {
                   if (Check.XMLNodeValidCheck(node))
                   {
                       string[] mAttributeArray = new string[7];
                       mAttributeArray[0] = node.Attribute(ToDoApplication.NAME_ATTRIBUTION).Value.Trim();
                       mAttributeArray[1] = node.Attribute(ToDoApplication.DESCRIPTION_ATTRIBUTION).Value.Trim();
                       mAttributeArray[2] = node.Attribute(ToDoApplication.MARKED_ATTRIBUTE).Value.Trim();
                       mAttributeArray[3] = node.Attribute(ToDoApplication.SORTORDER_ATTRIBUTE).Value.Trim();
                       mAttributeArray[4] = node.Attribute(ToDoApplication.TAGS_ATTRIBUTE).Value.Trim();
                       mAttributeArray[5] = node.Attribute(ToDoApplication.ISVIEWABLE_ATTRIBUTE).Value.Trim();

                       if (!Check.BoolCheck(mAttributeArray[2]))
                           return;

                       for (int i = 0, n = mAttributeArray.Length; i < n; i++)
                       {
                           if (String.IsNullOrEmpty(mAttributeArray[i]))
                               mAttributeArray[i] = "";
                       }

                       mApplication.Hub.Invoke("ClientImportTaskRequest", mAttributeArray[0], mAttributeArray[1], bool.Parse(mAttributeArray[2]), int.Parse(mAttributeArray[3]), mAttributeArray[4], bool.Parse(mAttributeArray[5])).Wait();
                   }
               });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
