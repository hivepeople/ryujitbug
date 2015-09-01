using System;
using System.Threading.Tasks;

namespace RyuJIT.Bug.Helper
{
    /// <summary>
    /// This test class exhibits a bug in RyuJIT where it compiles the state machine for the async method to invalid
    /// code. This in turn triggers a runtime InvalidProgramException as the method is invoked.
    /// <remarks>It may look like a very artificial example, but it is distilled from a real-world class.</remarks>
    /// </summary>
    public sealed class RyuJitBugHelper
    {
        private readonly object mutex = new object();

        private Task myTask;

        public RyuJitBugHelper(Task someTask)
        {
            this.myTask = someTask;
        }

        /// <summary>
        /// This has been reduced to be as minimal as possible and still trigger the bug. Removing the lock or the
        /// try-catch resultet in valid code being generated.
        /// </summary>
        /// <returns></returns>
        public async Task AwaitTaskCompletion()
        {
            Task localTask;

            lock (mutex)
            {
                if (myTask == null)
                    return;

                // Grab a reference to myTask inside lock
                localTask = myTask;
            }

            // We must await completion _outside_ the lock
            try
            {
                await localTask.ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (OperationCanceledException)
            {
                // Expected cancellation behaviour - ignore
            }
        }
    }
}
