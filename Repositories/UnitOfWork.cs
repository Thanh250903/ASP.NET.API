using WebAPI.Data;
using WebAPI.Interfaces;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Products = new ProductRepository(_context);
        Categories = new CategoryRepository(_context);
    }

    public IProductRepository Products { get; private set; }
    public ICategoryRepository Categories { get; private set; }


    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}