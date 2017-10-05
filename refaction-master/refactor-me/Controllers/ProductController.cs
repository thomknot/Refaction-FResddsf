using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using refactor_me.Services;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        // GET: /products/
        [Route]
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            var allProducts = ProductServices.AllProducts();
            return allProducts;
        }



        // GET:  /products?name={ProductName}
        [Route]
        [HttpGet]
        public IEnumerable<Product> GetProductByName(string name)
        {
            var ProductByName = ProductServices.GetProductByName(name);
            return ProductByName;
        }




        // GET:  /products/{id}
        [Route("{id:Guid}")]
        [HttpGet]
        public Product GetProductByGuid(Guid id)
        {
            var ProductByGuid = ProductServices.GetProductByGuid(id);
            return ProductByGuid;
        }



        // POST:  /products
        [Route]
        [HttpPost]
        public IHttpActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var AddProductResponse = ProductServices.AddProduct(product);

                if (AddProductResponse.IsSuccess)
                {
                    return Ok(AddProductResponse.NewObject);
                }
                else
                {
                    return StatusCode(HttpStatusCode.InternalServerError);
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }




        // PUT:  /products/{id}
        [Route("{id:Guid}")]
        [HttpPut]
        public IHttpActionResult UpdateProduct(Guid id, Product updatedProduct)
        {
            if (ModelState.IsValid)
            {
                Product foundProduct = ProductServices.GetProductByGuid(id);
                if (foundProduct == null)
                {
                    return NotFound(); //StatusCode(HttpStatusCode.NotFound); //ErrorCode:404
                }
                else
                {
                    var updateProductResponse = ProductServices.UpdateProduct(id, foundProduct, updatedProduct);

                    if (updateProductResponse.IsSuccess)
                    {
                        return Ok(updateProductResponse.NewObject);
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.InternalServerError);
                    }
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        // DELETE:  /products/{id}
        [Route("{id:Guid}")]
        [HttpDelete]
        public IHttpActionResult DeleteProduct(Guid id)
        {
            var deleteProduct = ProductServices.GetProductByGuid(id);
            if (deleteProduct == null)
            {
                return NotFound(); //StatusCode(HttpStatusCode.NotFound); //ErrorCode:404
            }
            else
            {
                var deleteProductResponse = ProductServices.DeleteProduct(deleteProduct);

                if (deleteProductResponse.IsSuccess)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                {
                    return StatusCode(HttpStatusCode.InternalServerError);
                }


            }

        }




        // GET:  /products/{id}/options
        [Route("{id:Guid}/options")]
        [HttpGet]
        public IEnumerable<ProductOption> GetAllProductOptions(Guid id)
        {
            var allProductOptionsById = ProductServices.GetAllProductOptionsById(id);
            return allProductOptionsById;
        }




        // GET:  /products/{id}/options/{optionId}
        [Route("{id:Guid}/options/{oid:Guid}")]
        [HttpGet]
        public ProductOption GetProductOptionByOptionId(Guid id, Guid oid)
        {
            var productByOptionId = ProductServices.GetProductOptionByOptionId(id, oid);
            return productByOptionId;
        }



        // POST:  /products/{id}/options
        [Route("{id:Guid}/options")]
        [HttpPost]
        public IHttpActionResult AddProductOption(Guid id, ProductOption newProductOption)
        {

            if (ModelState.IsValid)
            {
                Product foundProduct = ProductServices.GetProductByGuid(id);
                if (foundProduct == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest);
                }
                else
                {
                    var AddProductOptionResponse = ProductServices.AddProductOption(foundProduct, newProductOption);
                    if (AddProductOptionResponse.IsSuccess)
                    {
                        return Ok(AddProductOptionResponse.NewObject);
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.InternalServerError);
                    }
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        // PUT:  /products/{id}/options/{optionId}
        [Route("{id:Guid}/options/{oid:Guid}")]
        [HttpPut]
        public IHttpActionResult UpdateProductOption(Guid id, Guid oid, ProductOption updatedProductOption)
        {

            if (ModelState.IsValid)
            {
                ProductOption foundProductOption = ProductServices.GetProductOptionByOptionId(id, oid);
                if (foundProductOption == null)
                {
                    return NotFound(); //StatusCode(HttpStatusCode.NotFound); //ErrorCode:404
                }
                else
                {
                    if (!foundProductOption.ProductId.Equals(id))
                    {
                        return StatusCode(HttpStatusCode.BadRequest);
                    }
                    else
                    {
                        var updateProductResponse = ProductServices.UpdateProductOption(foundProductOption, updatedProductOption);

                        if (updateProductResponse.IsSuccess)
                        {
                            return Ok(updateProductResponse.NewObject);
                        }
                        else
                        {
                            return StatusCode(HttpStatusCode.InternalServerError);
                        }
                    }
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        // DELETE:  /products/{id}/options/{optionId}
        [Route("{id:Guid}/options/{oid:Guid}")]
        public IHttpActionResult DeleteProductOption(Guid id, Guid oid)
        {
            var deleteProductOption = ProductServices.GetProductOptionByOptionId(id, oid);
            if (deleteProductOption == null)
            {
                return NotFound(); //StatusCode(HttpStatusCode.NotFound); //ErrorCode:404
            }
            else
            {
                var deleteProductResponse = ProductServices.DeleteProductOption(deleteProductOption);

                if (deleteProductResponse.IsSuccess)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                {
                    return StatusCode(HttpStatusCode.InternalServerError);
                }
            }

        }

    }
}
