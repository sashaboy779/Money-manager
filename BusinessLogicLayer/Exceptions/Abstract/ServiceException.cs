using System;

namespace BusinessLogicLayer.Exceptions.Abstract
{
    public abstract class ServiceException : Exception
    {
        protected ServiceException(string message) : base(message)
        {
        }
    }
}
