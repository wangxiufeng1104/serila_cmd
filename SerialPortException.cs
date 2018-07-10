using System;

namespace YS.IO.Ports
{
    public class SerialPortException:Exception
    {
        public SerialPortException(string message)
            : base(message)
        { 
        }
    }
}
