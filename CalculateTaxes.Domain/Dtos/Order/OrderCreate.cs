using Newtonsoft.Json;

namespace CalculateTaxes.Domain.Dtos.Order
{
    public record OrderCreate
    {
        [JsonProperty("pedidoId")]
        public required int OrderId { get; set; }

        [JsonProperty("clienteId")]
        public required int ClientId { get; set;}

        [JsonProperty("itens")]
        public required List<ItemOrderCreate> Items { get; set; }
    }
}