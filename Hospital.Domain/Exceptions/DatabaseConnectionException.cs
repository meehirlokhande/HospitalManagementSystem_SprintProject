using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.Exceptions
{
    public class DatabaseConnectionException:Exception
    {
        public DatabaseConnectionException(string message): base(message) { }
        public DatabaseConnectionException(string message,Exception inner):base(message,inner) { }

    }
}
