using System.Text.Json.Serialization;

namespace WebApplication2.Domain.Entities
{
    public class Destino
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Nome { get; set; }
        public required Guid ViagemId { get; set; }

        [JsonIgnore]
        public Viagem? Viagem { get; set; }
    }
}
