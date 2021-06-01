using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Services;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly IService<Product, int> service;

        public ProductAPIController(IService<Product, int> service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var prods = await service.GetAsync();
            return Ok(prods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var prod = await service.GetAsync(id);
            return Ok(prod);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Product product)
        {
            if (ModelState.IsValid)
            {
                product = await service.CreateAsync(product);
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                product = await service.UpdateAsync(id, product);
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var res = await service.DeleteAync(id);
            if (res)
            {
                return Ok($"Record Deleted Succesfully {res}");
            }
            return NotFound($"Record Not Found");
        }
    }
}
