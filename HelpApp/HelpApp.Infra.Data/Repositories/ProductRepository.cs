using HelpApp.Domain.Interfaces;
using HelpApp.Domain.Entities;
using HelpApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpApp.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context; 
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }

            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id.Value);
        }

        public async Task<Product> Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Remove(Product product)
        {
            _context?.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
