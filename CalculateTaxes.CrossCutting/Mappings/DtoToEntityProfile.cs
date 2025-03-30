using AutoMapper;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Dtos.FeatureFlag;
using CalculateTaxes.Domain.Dtos.Order;
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
            CreateMap<ProductCreate, ProductEntity>();

            CreateMap<ProductUpdate, ProductEntity>();
            #endregion

            #region Client
            CreateMap<ClientCreate, ClientEntity>();

            CreateMap<ClientUpdate, ClientEntity>();
            #endregion
        
            #region FeatureFlag
            CreateMap<FeatureFlagCreate, FeatureFlagEntity>();

            CreateMap<FeatureFlagUpdate, FeatureFlagEntity>();
            #endregion
        
            #region Orders
            CreateMap<OrderCreate, OrderEntity>();
            CreateMap<ItemOrderCreate, ItemsOrderEntity>();
            #endregion
        }
    }
}