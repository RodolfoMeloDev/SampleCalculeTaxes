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
            var result = await _repository.InsertAsync( 
                new ProductEntity
                {
                    Name = new Name(createDto.Name).Value,
                    Price = new Price(createDto.Price).Value
                });

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
            var result = await _repository.UpdateAsync( 
                new ProductEntity
                {
                    Id = updateDto.id,
                    Name = new Name(updateDto.Name).Value,
                    Price = new Price(updateDto.Price).Value,
                    Active = updateDto.Active
                }
            );

            return _mapper.Map<ProductUpdateResponse>(result);
        }
    }
}