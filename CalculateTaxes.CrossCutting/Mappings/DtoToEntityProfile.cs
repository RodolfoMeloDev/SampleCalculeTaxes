using AutoMapper;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Dtos.Product;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Models;

namespace CalculateTaxes.CrossCutting.Mappings
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<ProductCreate, ProductEntity>()
                .BeforeMap((src, dest) => {
                    _ =new Name(src.Name);
                    _ = new Price(src.Price);
                })
                .ReverseMap() 
                .ConstructUsing(dto => new ProductCreate(dto.Name, dto.Price));

            CreateMap<ProductCreateResponse, ProductEntity>()
                .ReverseMap()
                .ConstructUsing(dto => new ProductCreateResponse(dto.Id, new Name(dto.Name).Value, new Price(dto.Price).Value, dto.CreatedAt));

            CreateMap<ProductUpdate, ProductEntity>()
                .BeforeMap((src, dest) => {
                    _ =new Name(src.Name);
                    _ = new Price(src.Price);
                })
                .ReverseMap() 
                .ConstructUsing(dto => new ProductUpdate(dto.Id, dto.Name, dto.Price, dto.Active));

            CreateMap<ProductUpdateResponse, ProductEntity>()
                .ReverseMap()
                .ConstructUsing(dto => new ProductUpdateResponse(dto.Id, dto.Name, dto.Price, dto.Active, dto.CreatedAt, (DateTime)dto.UpdatedAt! ));

            CreateMap<ProductDto, ProductEntity>()
                .ReverseMap()
                .ConstructUsing(dto => new ProductDto(dto.Id, dto.Name, dto.Price, dto.Active, dto.CreatedAt, dto.UpdatedAt));

            CreateMap<ClientCreate, ClientEntity>()
                .BeforeMap((src, dest) => {
                    _ =new Name(src.Name);
                    _ = new Birthday(src.Birthday);
                })
                .ReverseMap() 
                .ConstructUsing(dto => new ClientCreate(dto.Name, dto.Birthday));

            CreateMap<ClientUpdate, ClientEntity>()
                .BeforeMap((src, dest) => {
                    _ =new Name(src.Name);
                })
                .ReverseMap()
                .ConstructUsing(dto => new ClientUpdate(dto.Id, dto.Name, dto.Active));

            CreateMap<ClientCreateResponse, ClientEntity>()
                .ReverseMap()
                .ConstructUsing(dto => new ClientCreateResponse(dto.Id, dto.Name, dto.Birthday, dto.CreatedAt));

            CreateMap<ClientUpdateResponse, ClientEntity>()
                .ReverseMap()
                .ConstructUsing(dto => new ClientUpdateResponse(dto.Id, dto.Name, dto.Birthday, dto.Active, dto.CreatedAt, (DateTime)dto.UpdatedAt! ));

            CreateMap<ClientResponse, ClientEntity>()
                .ReverseMap()
                .ConstructUsing(dto => new ClientResponse(dto.Id, dto.Name, dto.Birthday, dto.Active, dto.CreatedAt, dto.UpdatedAt));
        }
    }
}