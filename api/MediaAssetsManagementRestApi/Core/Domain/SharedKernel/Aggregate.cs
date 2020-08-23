using System;

namespace Domain.SharedKernel
{
    /// <summary>
    /// Aggregate, which defines a transaction boundary. Can be manipulated directly through a repository.
    /// </summary>
    /// <typeparam name="TKey">The type of the aggregate's identity.</typeparam>
    public abstract class Aggregate<TKey> : Entity<TKey> where TKey : struct
    {
        protected Aggregate() : base() { }

        protected Aggregate(TKey id, DateTime creationDate) : base(id, creationDate) { }
    }
}
