using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ToDoTrainingGit
{
    class Check
    { 
        public static bool EmptyStringCheck(string tempName)
        {
            if (tempName.Equals(ToDoApplication.EXIT_STRING))
                return true;

            if (String.IsNullOrWhiteSpace(tempName))
            {
                return false;
            }
            return true;
        }

        public static string AskQuestion(string question, Func<string, bool> predicate = null)
        {
            if (predicate != null)
            {
                while (true)
                {
                    Console.WriteLine(question);
                    var result = Console.ReadLine().Trim();

                    if (string.IsNullOrWhiteSpace(result))
                    {
                        continue;
                    }

                    if (predicate != null && !predicate(result))
                    {
                        continue;
                    }

                    return result;
                }
            }
            return "";
        }

        public static string AskQuestionServer (ToDoApplication application, string question, string serverfunction)
        {
            while(true)
            {
                Console.WriteLine(question);
                string answer = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(answer))
                {
                    continue;
                }

                if(ExitCheck(answer))
                {
                    return "exit";
                }

                try
                {
                    var mConnection = application.HubConnection;
                    var mHub = application.Hub;
                    bool serverboolresult = false;

                    while (mConnection.State != ConnectionState.Connected)
                    {
                        mConnection.Start().Wait();
                    }
                    serverboolresult = mHub.Invoke<bool>(serverfunction, answer).Result;
                    
                    if (!serverboolresult)
                    {
                        continue;
                    }

                    return answer;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }                  
            }          
        }

        public static bool EitherOrQuestionCheck(string userInput, string option1, string option2)
        {
            if (userInput.Equals(ToDoApplication.EXIT_STRING))
                return true;

            if (String.IsNullOrWhiteSpace(userInput))
            {
                return false;
            }

            if (userInput.ToLower().Trim() != option1 && userInput.ToLower().Trim() !=
                option2)
            {
                Console.WriteLine("Please enter either " + option1 + " or " + option2 + ":");
                return false;
            }

            return true;
        }

        public static bool ExitCheck(string userInput)
        {
            if (userInput.Trim().ToLower() == ToDoApplication.EXIT_STRING)
            {
                return true;
            }

            return false;
        }

        public static bool XMLFileCheck(string fileName)
        {
            if (fileName.Equals(ToDoApplication.EXIT_STRING))
                return true;

            string fileNamePath =
                ToDoApplication.FOLDER_LOCATION + fileName + ToDoApplication.FILE_EXTENSION;
            if(File.Exists(fileNamePath))
            {
                return true;
            }

            Console.WriteLine("File does not exist!");
            return false;
        }

        public static bool XMLNodeValidCheck(XElement node)
        {
            if (node.Attribute(ToDoApplication.NAME_ATTRIBUTION) != null && node.Attribute(ToDoApplication.DESCRIPTION_ATTRIBUTION) 
                != null && node.Attribute(ToDoApplication.MARKED_ATTRIBUTE) != null && node.Attribute(ToDoApplication.TAGS_ATTRIBUTE) != null
                && node.Attribute(ToDoApplication.SORTORDER_ATTRIBUTE) != null && node.Attribute(ToDoApplication.ISVIEWABLE_ATTRIBUTE) != null)
            {
                return true;
            }
            return false;
        }

        public static bool BoolCheck(string tempName)
        {
            if (String.IsNullOrWhiteSpace(tempName))
            {
                return false;
            }

            if (tempName.ToLower().Trim() != "true" && tempName.ToLower().Trim() != "false")
            {
                Console.WriteLine("Marked is not true or false.");
                return false;
            }

            return true;
        }

        public static bool ReorderInstructionCheck(string userInput)
        {
            userInput=userInput.ToLower().Trim();

            if (String.IsNullOrWhiteSpace(userInput))
                return false;

            if(userInput == "move" || userInput == "view all" ||
                userInput == "view unfinished" || userInput == "view completed")
            {
                return true;
            }

            return false;
        }

        public static bool ListCountGreaterZeroCheck(ToDoApplication Application)
        {
             try
            {
                bool ListCountGreaterZeroResult = false;
                var mConnection = Application.HubConnection;
                var mHub = Application.Hub;

                while(mConnection.State != ConnectionState.Connected)
                {
                    mConnection.Start().Wait();
                }

                ListCountGreaterZeroResult = mHub.Invoke<bool>("ClientListCountGreaterThanZeroCheckRequest").Result;
            
                return ListCountGreaterZeroResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }            
        }
    }
}
