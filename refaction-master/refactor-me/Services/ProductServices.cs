using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;


namespace refactor_me.Services
{
    public static class ProductServices
    {
        static string _error = "Something went wrong please try again later!";
        public static IEnumerable<Product> GetProductByName(string name)
        {
            using (var dbcontext = new ApplicationDbContext())
            {
                var productList = dbcontext.Products.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
                return productList;
            }
        }

        public static IEnumerable<Product> AllProducts()
        {
            using (var dbcontext = new ApplicationDbContext())
            {
                var allProducts = dbcontext.Products.ToList();
                return allProducts;
            }
        }

        public static Product GetProductByGuid(Guid id)
        {
            using (var dbcontext = new ApplicationDbContext())
            {
                var productByGuid = dbcontext.Products.FirstOrDefault(p => p.ProductId == id);
                return productByGuid;
            }
        }

        public static IEnumerable<ProductOption> GetAllProductOptionsById(Guid id)
        {
            using (var dbcontext = new ApplicationDbContext())
            {
                var allProductOptions = dbcontext.ProductOptions.Where(x => x.ProductId == id).ToList();
                return allProductOptions;
            }
        }

        public static ProductOption GetProductOptionByOptionId(Guid id, Guid oid)
        {
            using (var dbcontext = new ApplicationDbContext())
            {
                var productByOptionId = dbcontext.ProductOptions.FirstOrDefault(po => po.ProductId == id && po.ProductOptionId == oid);
                return productByOptionId;
            }
        }

        public static ServiceResponseResult AddProduct(Product product)
        {
            using (var dbcontext = new ApplicationDbContext())
            {
                product.ProductId = Guid.NewGuid();
                dbcontext.Products.Add(product);
                try
                {
                    dbcontext.SaveChanges();
                    return new ServiceResponseResult { IsSuccess = true, NewObject = product };
                }
                catch (DbUpdateException)
                {
                    return new ServiceResponseResult { IsSuccess = false, ErrorMessage = _error };
                }
            }
        }

        public static ServiceResponseResult UpdateProduct(Guid id, Product foundProduct, Product updatedProduct)
        {
            using (var dbcontext = new ApplicationDbContext())
            {

                if (!updatedProduct.ProductId.Equals(id))
                {
                    updatedProduct.ProductId = foundProduct.ProductId;
                }
                dbcontext.Products.Attach(updatedProduct);
                var entry = dbcontext.Entry(updatedProduct);
                entry.State = EntityState.Modified;
                try
                {
                    dbcontext.SaveChanges();
                    return new ServiceResponseResult { IsSuccess = true, NewObject = updatedProduct };
                }
                catch (DbUpdateException)
                {
                    return new ServiceResponseResult { IsSuccess = false, ErrorMessage = _error };
                }
            }
        }

        public static ServiceResponseResult DeleteProduct(Product deleteProduct)
        {
            using (var dbcontext = new ApplicationDbContext())
            {
                dbcontext.Products.Remove(deleteProduct);

                try
                {
                    dbcontext.SaveChanges();
                    return new ServiceResponseResult { IsSuccess = true };
                }
                catch (DbUpdateException)
                {
                    return new ServiceResponseResult { IsSuccess = false, ErrorMessage = _error };
                }
            }
        }

        public static ServiceResponseResult DeleteProductOption(ProductOption deleteProductOption)
        {
            using (var dbcontext = new ApplicationDbContext())
            {
                dbcontext.ProductOptions.Remove(deleteProductOption);

                try
                {
                    dbcontext.SaveChanges();
                    return new ServiceResponseResult { IsSuccess = true };
                }
                catch (DbUpdateException)
                {
                    return new ServiceResponseResult { IsSuccess = false, ErrorMessage = _error };
                }
            }
        }

        public static ServiceResponseResult AddProductOption(Product foundProduct, ProductOption newProductOption)
        {
            using (var dbcontext = new ApplicationDbContext())
            {
                newProductOption.ProductOptionId = Guid.NewGuid();
                newProductOption.ProductId = foundProduct.ProductId;
                newProductOption.product = foundProduct;
                dbcontext.ProductOptions.Add(newProductOption);
                try
                {
                    dbcontext.SaveChanges();
                    return new ServiceResponseResult { IsSuccess = true, NewObject = newProductOption };
                }
                catch (DbUpdateException)
                {
                    return new ServiceResponseResult { IsSuccess = false, ErrorMessage = _error };
                }
            }
        }

        public static ServiceResponseResult UpdateProductOption(ProductOption foundProductOption, ProductOption updatedProductOption)
        {
            using (var dbcontext = new ApplicationDbContext())
            {
                foundProductOption.Name = updatedProductOption.Name;
                foundProductOption.Description = updatedProductOption.Description;
                dbcontext.ProductOptions.Attach(foundProductOption);
                var entry = dbcontext.Entry(foundProductOption);
                entry.State = EntityState.Modified;
                try
                {
                    dbcontext.SaveChanges();
                    return new ServiceResponseResult { IsSuccess = true, NewObject = updatedProductOption };
                }
                catch (DbUpdateException)
                {
                    return new ServiceResponseResult { IsSuccess = false, ErrorMessage = _error };
                }
            }
        }
    }
}