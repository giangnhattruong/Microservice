namespace ProductService.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}