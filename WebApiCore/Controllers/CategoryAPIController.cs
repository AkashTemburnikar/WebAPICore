using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiCore.Models;
using WebApiCore.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        private readonly IService<Category, int> service;

        public CategoryAPIController(IService<Category,int> service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var cats = await service.GetAsync();
            return Ok(cats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var cat = await service.GetAsync(id);
            return Ok(cat);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Category category)
        {
           
            if (ModelState.IsValid)
            {
                if(category.BasePrice < 0)
                {
                    throw new Exception("Base Price cannot be negative");
                }
                category = await service.CreateAsync(category);
                return Ok(category);
            }
            return BadRequest(ModelState);
          
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                category = await service.UpdateAsync(id, category);
                return Ok(category);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var res = await service.DeleteAync(id);
            if(res)
            {
                return Ok($"Record Deleted Succesfully {res}");
            }
            return NotFound($"Record Not Found");
        }
    }
}
