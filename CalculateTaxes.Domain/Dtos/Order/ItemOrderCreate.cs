using Newtonsoft.Json;

namespace CalculateTaxes.Domain.Dtos.Order
{
    public record ItemOrderCreate
    {
        [JsonProperty("produtoId")]
        public required int ProductId { get; set; }

        [JsonProperty("quantidade")]
        public required int Amount { get; set; }

        [JsonProperty("valor")]
        public required decimal Price { get; set; }
    }
}