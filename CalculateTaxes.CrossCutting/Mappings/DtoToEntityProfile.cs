using AutoMapper;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Dtos.FeatureFlag;
using CalculateTaxes.Domain.Dtos.Product;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Models;

namespace CalculateTaxes.CrossCutting.Mappings
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            #region Product
            CreateMap<ProductCreate, ProductEntity>()
                .BeforeMap((src, dest) => {
                    _ =new Name(src.Name);
                    _ = new Price(src.Price);
                });

            CreateMap<ProductUpdate, ProductEntity>()
                .BeforeMap((src, dest) => {
                    _ =new Name(src.Name);
                    _ = new Price(src.Price);
                });
            #endregion

            #region Client
            CreateMap<ClientCreate, ClientEntity>()
                .BeforeMap((src, dest) => {
                    _ =new Name(src.Name);
                    _ = new Birthday(src.Birthday);
                    _ = new CPF(src.CPF);
                });

            CreateMap<ClientUpdate, ClientEntity>()
                .BeforeMap((src, dest) => {
                    _ =new Name(src.Name);
                });
            #endregion
        
            #region FeatureFlag
            CreateMap<FeatureFlagCreate, FeatureFlagEntity>()
                .BeforeMap((src, dest) => {
                    _ =new Name(src.Name);
                });

            CreateMap<FeatureFlagUpdate, FeatureFlagEntity>();                        
            #endregion
        }
    }
}