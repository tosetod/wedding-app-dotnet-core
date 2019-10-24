using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Exceptions
{
    [Serializable]
    public class ResourceNotFoundException<T> : Exception
    {
        public ResourceNotFoundException()
        {
            
        }

        public ResourceNotFoundException(string message)
            : base(message)
        {
            
        }

        public ResourceNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
            
        }

        public ResourceNotFoundException(Type resource, long id)
            : base($"Resource {resource.Name} with id {id} was not found")
        {
            
        }

        public ResourceNotFoundException(long id)
            : base($"Resource {typeof(T).Name} with id {id} was not found")
        {
            
        }
    }
}
