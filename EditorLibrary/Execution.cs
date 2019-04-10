using System;
using System.IO;
using Newtonsoft.Json;
using EditorLibrary.Dict;
using EditorLibrary.Cmd;
using EditorLibrary.Editor;
using EditorLibrary.Except;

namespace EditorLibrary
{
    public static class Execution
    {
        static void StartUp()
        {
            var json = File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/EditorLibrary/Dict/JsonStartUp/Cmd.json");
            CmdDict.SetUp(JsonConvert.DeserializeObject<CmdList>(json));

            TextView.Execute.Init();
        }

        public static void Run()
        {
            StartUp();

            Console.WriteLine(ConsoleDict.Wellcome);

            EditorHandler.Init();

            while (true)
            {
                Console.Write(ConsoleDict.InputCmd);
                try
                {
                    if (!CmdHandler.Execute(Console.ReadLine()))
                        break;
                }
                catch (BaseException ex)
                {
                    Console.WriteLine($"{ConsoleDict.ExceptionOccurred.PadLeft(15, ' ')} {ex.Message}");
                }
            }
        }
    }
}
