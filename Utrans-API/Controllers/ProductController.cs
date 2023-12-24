﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Utrans_API.DBContexts;
using Utrans_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Utrans_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {   
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.id == id);
        }

        // GET: api/<ProductsController>
        // api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return _context.Products.Where(b => b.Deleted_at == null).ToList();
        }

        // GET api/<ProductsController>/5
        // api/Product/[ID]
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var Product = await _context.Products.Where(b => b.Deleted_at == null).FirstOrDefaultAsync(b => b.id == id); ;

            if (Product == null)
            {
                return NotFound();
            }

            return Product;
            //return "value";
        }

        // POST api/<ProductsController>
        // POST api/Product
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts(Products Product)
        {

            var product  = new Products
            {
                Brand_id = Product.Brand_id,
                Code = Product.Code,
                Name = Product.Name,
                Description = Product.Description,
                stock = Product.stock,
                sales_price = Product.sales_price,
                standard_price = Product.standard_price,
                Created_at = Product.Created_at,
                Updated_at = Product.Updated_at,
                Deleted_at = Product.Deleted_at
            };

            await _context.Products.AddAsync(product);

            try
            {                 
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                {   
                    _context.Entry(product).State = EntityState.Modified;
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetProducts), new { id = Product.id }, Product);
        }

        // PUT api/<ProductsController>/5
        // api/Product/[ID]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Products Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found");
            }

            existingProduct.Brand_id = Product.Brand_id;
            existingProduct.Code = Product.Code;
            existingProduct.Name = Product.Name;
            existingProduct.Description = Product.Description;
            existingProduct.stock = Product.stock;
            existingProduct.sales_price = Product.sales_price;
            existingProduct.standard_price = Product.standard_price;
            existingProduct.Updated_at = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    return NotFound($"Brand with ID {id} not found");
                }
                else
                {   
                    _context.Entry(Product).State = EntityState.Modified;
                    throw;
                }
            }

            return Ok(new { message = "Product Updated" });
        }

        // DELETE api/<ProductsController>/5
        // api/Product/[ID]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProduct(int id)
        {   
            var existingProduct = await _context.Products.FindAsync(id);
            
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Deleted_at = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!ProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Product deleted" });
        }
    }
}
