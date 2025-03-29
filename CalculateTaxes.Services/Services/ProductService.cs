using AutoMapper;
using CalculateTaxes.Domain.Dtos.Product;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using CalculateTaxes.Domain.Interfaces.Services;
using CalculateTaxes.Domain.Models;

namespace CalculateTaxes.Services.Services
{
    public class ProductService(IProductRepository repository, IMapper mapper) : IProductService
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductCreateResponse> CreateProduct(ProductCreate createDto)
        {
            var entity = _mapper.Map<ProductEntity>(createDto);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<ProductCreateResponse>(result);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _repository.GetAllAsync());
        }

        public async Task<ProductDto?> GetByIdProduct(int id)
        {
            return _mapper.Map<ProductDto?>(await _repository.GetByIdAsync(id));
        }

        public async Task<ProductUpdateResponse> UpdateProduct(ProductUpdate updateDto)
        {
            var entity = _mapper.Map<ProductEntity>(updateDto);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<ProductUpdateResponse>(result);
        }
    }
}