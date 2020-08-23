namespace Domain.SharedKernel
{
    public interface IRepository<out TAggregate, out TKey>
        where TAggregate : Aggregate<TKey>
        where TKey : struct
    {
    }
}
