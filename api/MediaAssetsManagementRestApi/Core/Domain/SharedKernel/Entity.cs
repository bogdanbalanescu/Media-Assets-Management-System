using Domain.SharedKernel.Exceptions;
using EnsureThat;
using System;

namespace Domain.SharedKernel
{
    /// <summary>
    /// Entity with identity, but which should only be manipulated through the repository of its owner/aggregate.
    /// </summary>
    /// <typeparam name="TKey">The type of the entity's identity.</typeparam>
    public abstract class Entity<TKey> where TKey: struct
    {
        public TKey Id { get; set; }
        public DateTime CreationDate { get; set; }

        protected Entity()
        {
            CreationDate = DateTime.UtcNow;
        }

        protected Entity(TKey id, DateTime creationDate)
        {
            EnsureArg.IsNotDefault(id, nameof(id), 
                o => o.WithException(new RequiredArgumentException(nameof(id))));
            Id = id;

            EnsureArg.IsTrue(creationDate.Kind == DateTimeKind.Utc, nameof(creationDate),
                o => o.WithException(new ArgumentException(nameof(creationDate), "Date must be of type UTC")));
            EnsureArg.IsLte(creationDate, DateTime.UtcNow, nameof(creationDate),
                o => o.WithException(new ArgumentException(nameof(creationDate), "Date must not be in the future")));
            CreationDate = creationDate;
        }
    }
}
