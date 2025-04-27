using HelpApp.Domain.Interfaces;
using HelpApp.Domain.Entities;
using HelpApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpApp.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        public async Task<Category> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
            return await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> Create(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Update(Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(category.Id);
            if (existingCategory == null)
            {
                return null;
            }

            _context.Entry(existingCategory).CurrentValues.SetValues(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Remove(Category category)
        {
            var existingCategory = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id ==  category.Id);
            if (existingCategory.Products?.Any() == true)
            {
                return null;
            }

            if (existingCategory.Products?.Any() == true)
            {
                _context.Products.RemoveRange(existingCategory.Products);
            }

            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
