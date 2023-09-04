namespace SimpleMan.StateMachine
{
    public interface ILogger
    {
        void PrintInfo(string message);
        void PrintWarning(string message);
    }
}