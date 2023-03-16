using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entites;
using Talabat.Domine.IRepository;
using Talabat.Domine.Specifications;

namespace Talabat.APIs.Controllers
{
    public class ProudectsController : GenericController
    {
        private readonly IGenericRepository<Product> _genericRepository;
        private readonly IGenericRepository<ProductBrand> _productBrand;
        private readonly IGenericRepository<ProductType> _productType;
        private readonly IMapper _mapper;

        public ProudectsController(IGenericRepository<Product> genericRepository,
            IGenericRepository<ProductBrand> productBrand,
            IGenericRepository<ProductType> productType,
            IMapper mapper)
        {
            _genericRepository = genericRepository;
            _productBrand = productBrand;
            _productType = productType;
            _mapper = mapper;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProduct([FromQuery]ProductSpecificationParameters parameters)
        {
            var spec = new ProductTypeAndBarndSpec(parameters);
            var product = await _genericRepository.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(product);
            var filterCount = new ProductWithCountOfFilteration(parameters);
            var count = await _genericRepository.GetCountAsync(filterCount);
            return Ok(new Pagination<ProductToReturnDto>(parameters.pageIndex, parameters.PageSize, count, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductTypeAndBarndSpec(id);
            var product = await _genericRepository.GetByIdWithSpecAsync(spec);
            if (product == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrand()
        {
            var brand = await _productBrand.GetAllAsync();
            return Ok(brand);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductType()
        {
            var brand = await _productType.GetAllAsync();
            return Ok(brand);
        }
        

    }
}
