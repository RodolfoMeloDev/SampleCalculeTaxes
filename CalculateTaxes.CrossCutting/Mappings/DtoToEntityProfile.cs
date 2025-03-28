using AutoMapper;
using CalculateTaxes.Domain.Dtos.Product;
using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.CrossCutting.Mappings
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<ProductCreateResponse, ProductEntity>()
                .ReverseMap()
                .ConstructUsing(dto => new ProductCreateResponse(dto.Id, dto.Name, dto.Price, dto.CreatedAt));

            CreateMap<ProductUpdateResponse, ProductEntity>()
                .ReverseMap()
                .ConstructUsing(dto => new ProductUpdateResponse(dto.Id, dto.Name, dto.Price, dto.Active, dto.CreatedAt, (DateTime)dto.UpdatedAt! ));

            CreateMap<ProductDto, ProductEntity>()
                .ReverseMap()
                .ConstructUsing(dto => new ProductDto(dto.Id, dto.Name, dto.Price, dto.Active, dto.CreatedAt, dto.UpdatedAt));
        }
    }
}