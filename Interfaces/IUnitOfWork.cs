// IUnitOfWork.cs
namespace WebAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        Task<int> SaveChangesAsync();
    }
}
