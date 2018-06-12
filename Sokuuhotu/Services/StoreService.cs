using BAW.Models;
using Sokuuhotu.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BAW.Services
{
    public class StoreService
    {


        private readonly ApplicationDbContext _db;

        public StoreService(ApplicationDbContext context)
        {
            _db = context;
        }

        public StoreService() : this(new ApplicationDbContext())
        {

        }




        public async Task<IEnumerable<Category>> CategoriesAsync()
        {
            return await _db.Categories.ToArrayAsync();
        }


        public async Task<IEnumerable<Product>> ProductsAsync()
        {
            return await _db.Products.ToArrayAsync();
        }


        public async Task<Product> ProductByIdAsync(int proId)
        {
            return await _db.Products.Include("Category").SingleOrDefaultAsync(p => p.ProductId == proId);
        }


        public async Task<IEnumerable<Product>> ProductsByCategoryIdAsync(int categoryId)
        {
            return await _db.Products.Include("Category").Where(p => p.CategoryId == categoryId /*&& p.EndDate< DateTime.Now*/)
                .ToArrayAsync();
        }



    }
}