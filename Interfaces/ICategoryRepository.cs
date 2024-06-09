// ICategoryRepository.cs
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<bool> DeleteCategory(int id);
    }
}
