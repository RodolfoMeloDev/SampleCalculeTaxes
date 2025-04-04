using AutoMapper;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Dtos.FeatureFlag;
using CalculateTaxes.Domain.Dtos.Order;
using CalculateTaxes.Domain.Dtos.Product;
using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            #region Product
            CreateMap<ProductEntity, ProductResponse>()
                .ConstructUsing(dto => new ProductResponse(dto.Id, dto.Name, dto.Active, dto.CreatedAt, dto.UpdatedAt));
            #endregion

            #region Client
            CreateMap<ClientEntity, ClientResponse>()
                .ConstructUsing(dto => new ClientResponse(dto.Id, dto.Name, dto.Birthday, dto.CPF, dto.Active, dto.CreatedAt, dto.UpdatedAt));
            #endregion

            #region FeatureFlag
            CreateMap<FeatureFlagEntity, FeatureFlagResponse>()
                .ConstructUsing(dto => new FeatureFlagResponse(dto.Id, dto.Name, dto.Active, dto.CreatedAt, dto.UpdatedAt));
            #endregion

            #region Orders
            CreateMap<OrderEntity, OrderResponse>();
            CreateMap<OrderEntity, OrderCreateResponse>();
            CreateMap<ItemsOrderEntity, ItemOrderCreate>();
            #endregion
        }
    }
}