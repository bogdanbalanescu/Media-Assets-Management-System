using System;

namespace Api.Controllers.ErrorHandling
{
    public class EnumerationClass<TKey, TValue> : IComparable<TKey> where TKey : IComparable
    {
        public TKey Id { get; set; }
        public TValue Value { get; set; }

        public EnumerationClass(TKey id, TValue value)
        {
            Id = id;
            Value = value;
        }

        public int CompareTo(TKey other)
        {
            return Id.CompareTo(other);
        }
    }
}