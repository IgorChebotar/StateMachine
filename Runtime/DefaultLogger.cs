using SimpleMan.Utilities;

namespace SimpleMan.StateMachine
{
    public class DefaultLogger : ILogger
    {
        public void PrintInfo(string message)
        {
            PrintToConsole.Info(message);
        }

        public void PrintWarning(string message)
        {
            PrintToConsole.Warning(message);
        }
    }
}