using System;
using System.Threading.Tasks;
using RyuJIT.Bug.Helper;

namespace RyuJIT.Bug.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Awaiting task...");

            var completedTask = Task.FromResult(1);
            var helper = new RyuJitBugHelper(completedTask);
            helper.AwaitTaskCompletion().Wait();

            Console.WriteLine("done");
        }
    }
}
