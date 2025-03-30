using Newtonsoft.Json;

namespace CalculateTaxes.Domain.Dtos.Order
{
    public class OrderResponse
    {
        public required int Id { get; set; }

        [JsonProperty("pedidoId")]
        public required int OrderId { get; set; }

        [JsonProperty("clienteId")]
        public required int ClientId { get; set;}

        [JsonProperty("impostos")]
        public required decimal Taxes { get; set; }

        [JsonProperty("itens")]
        public required List<ItemOrderCreate> Items { get; set; }

        public required string Status { get; set; }
    }
}