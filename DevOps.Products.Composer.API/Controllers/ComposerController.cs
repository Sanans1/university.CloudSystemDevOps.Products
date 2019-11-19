using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;
using DevOps.Products.Composer.API.Services.Interfaces;
using DevOps.Products.Composer.API.Services.Interfaces.Facades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevOps.Products.Composer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComposerController : ControllerBase
    {
        private readonly IProductFacadeService _productFacadeService;


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(string searchString, int? categoryID, int? brandID)
        //{
        //    return Ok(await _productFacadeService.(product => (string.IsNullOrWhiteSpace(searchString) ||
        //                                                       product.Name.Contains(searchString) ||
        //                                                       product.Description.Contains(searchString)) &&
        //                                                      (!categoryID.HasValue || product.CategoryID == categoryID) &&
        //                                                      (!brandID.HasValue || product.BrandID == brandID)));
        //}
    }
}