namespace OrderService.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}