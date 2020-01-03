namespace EllinghamScript.Logger
{
    public class LoggerBase
    {
        public LoggerMode LoggerMode { get; set; } = LoggerMode.None;

        public virtual void Log(string message)
        {
        }
    }
}