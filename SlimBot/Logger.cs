using System;

namespace SlimBot
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            if(message is null) throw new ArgumentException("Message cannot be null.");
            Console.WriteLine(message);
        }
    }
}