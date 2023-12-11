using Microsoft.EntityFrameworkCore;
using NSTask.Contracts;
using NSTask.Models;

namespace NSTask.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private NSTaskDataBase _context;
        public ProductRepository(NSTaskDataBase context)
        {
            _context = context;
        }
        public async Task<Product> Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;


        }

        public async Task<Product> Find(int id)
        {

            return await _context.Products.SingleOrDefaultAsync(c => c.ProductId == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.Products.AnyAsync(c => c.ProductId == id);
        }

        public async Task<Product> Remove(int id)
        {
            var product = await _context.Products.SingleAsync(c => c.ProductId == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task<Product> Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}

