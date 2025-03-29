using System.Data;
using AutoMapper;
using CalculateTaxes.Domain.Dtos.Product;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using CalculateTaxes.Domain.Interfaces.Services;

namespace CalculateTaxes.Services.Services
{
    public class ProductService(IProductRepository repository, IMapper mapper) : IProductService
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductResponse> CreateProduct(ProductCreate createDto)
        {
            var product = await _repository.GetByNameAsync(createDto.Name);

            if (product != null)
                throw new DuplicateNameException($"O Produto informada já está cadastrada. Id: {product.Id}");

            var entity = _mapper.Map<ProductEntity>(createDto);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<ProductResponse>(result);
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProducts()
        {
            return _mapper.Map<IEnumerable<ProductResponse>>(await _repository.GetAllAsync());
        }

        public async Task<ProductResponse?> GetByIdProduct(int id)
        {
            return _mapper.Map<ProductResponse?>(await _repository.GetByIdAsync(id));
        }

        public async Task<ProductResponse> UpdateProduct(ProductUpdate updateDto)
        {
            var entity = _mapper.Map<ProductEntity>(updateDto);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<ProductResponse>(result);
        }
    }
}