using System;
using System.Runtime.Serialization;

namespace Behaviours.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        protected static string GetMessage(Type entity, string key) => $"{entity.Name} with key \"{key}\" can not found";

        public EntityNotFoundException() { }

        public EntityNotFoundException(Type entity, string key) : base(GetMessage(entity, key)) { }

        public EntityNotFoundException(Type entity, string key, Exception inner) : base(GetMessage(entity, key), inner) { }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class EntityNotFoundException<TEntity> : EntityNotFoundException
    {
        public EntityNotFoundException(string key) : base(typeof(TEntity), key) { }

        public EntityNotFoundException(string key, Exception inner) : base(typeof(TEntity), key, inner) { }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
