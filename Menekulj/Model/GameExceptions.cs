using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menekulj.Model
{
    public class TooManyMinesException : Exception
    {
        public TooManyMinesException(string msg) : base(msg)
        {
        }

        public TooManyMinesException() { }
    }

    public class AlreadyRunningException : Exception
    {

    }

    public class NoGameCreatedException : Exception
    {

    }

    public class UnitIsDeadException : Exception
    {


    }
}
