using System;
using System.Runtime.CompilerServices;
using View.AutoTask.Options;

namespace View.AutoTask.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            TaskInitializer.Start(new TaskOptions()
            {
                Cache = new Caches.DefaultFileMetadataCache(),
                CacheType = CacheType.Text,
                TaskScope = TaskScope.Assembly
            });

            Console.ReadKey();
        }

        [TaskMethod(10, Units.Second, OnStartup = true)]
        public static void TaskTest0()
        {
            Console.WriteLine($"TaskTest0 Runing. {DateTime.Now:yy-MM-dd HH:mm:ss}");
        }
    }

    [TaskClass]
    public class MyClass
    {

        [TaskMethod(10, Units.Second, OnStartup = true)]
        public static void TaskTest1()
        {
            Console.WriteLine($"TaskTest1 Runing. {DateTime.Now:yy-MM-dd HH:mm:ss}");
        }

        [TaskMethod(10, Units.Second, OnStartup = false)]
        public static void TaskTest2()
        {
            Console.WriteLine($"TaskTest2 Runing. {DateTime.Now:yy-MM-dd HH:mm:ss}");
        }

        [TaskMethod(10, Units.Second, RunSpan = "11:36:00-11:36:59")]
        public static void TaskTest3()
        {
            Console.WriteLine($"TaskTest3 Runing. {DateTime.Now:yy-MM-dd HH:mm:ss}");
        }

        [TaskMethod(1, Units.Minute)]
        public static void TaskTest4()
        {
            Console.WriteLine($"TaskTest4 Runing. {DateTime.Now:yy-MM-dd HH:mm:ss}");
        }

        [TaskMethod(1, Units.Hour)]
        public static void TaskTest5()
        {
            Console.WriteLine($"TaskTest5 Runing. {DateTime.Now:yy-MM-dd HH:mm:ss}");
        }
    }
}
