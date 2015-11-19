using System;

namespace IntuiLab.Leap.Exceptions
{
    class LeapException : Exception
    {
        public LeapException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public LeapException(string message)
            : base(message)
        {
        }
    }
}
