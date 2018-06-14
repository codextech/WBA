using BAW.Models;
using Sokuuhotu.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

        

         public async Task AddProductAsync(Product model ,string userId,int closeDate)
        {

            var product = new Product
            {
                CategoryId = model.CategoryId,
                ProductName = model.ProductName,
                ProductDescription =model.ProductDescription,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(closeDate),

                UserId = userId
            };

         

            _db.Products.Add(product);

            await _db.SaveChangesAsync();


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




        public async Task<IEnumerable<Product>> GetPostedProductsAsync(string userId)
        {
            return await _db.Products.Where(p => p.UserId == userId).ToArrayAsync();

        }




        // Edit function

        public async Task ProductEditAsync(Product model, int closeinDays)
        {

            model.EndDate = model.EndDate.AddDays(closeinDays);

            _db.Entry(model).State = EntityState.Modified;
            await _db.SaveChangesAsync();


        }




        public async Task<IEnumerable<Product>> SearchAsync(string search)
        {
            return await _db.Products.Include("Category").Where(p => p.ProductName.Contains(search.ToLower()) )
                .ToArrayAsync();
        }



        //************Without async*************
        //public IEnumerable<SelectListItem> CategoryList()
        //{

        //    var list = new SelectList(_db.Categories, "CategoryId", "CategoryName");
        //    return list;
        //}

        //Find Product by ID /For Edit Action
        public Product FindProduct(int? productId)
        {
            return _db.Products.Find(productId);
        }


    }
}