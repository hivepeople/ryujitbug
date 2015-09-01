using System;
using System.Threading.Tasks;
using NUnit.Framework;
using RyuJIT.Bug.Helper;

namespace RyuJIT.Bug.Test
{
    [TestFixture]
    public class RyuJITTest
    {

        [Test]
        public void JittingFailsWithInvalidProgramException()
        {
            var completedTask = Task.FromResult(1);

            var helper = new RyuJitBugHelper(completedTask);

            helper.AwaitTaskCompletion().Wait();
        }
    }
}
