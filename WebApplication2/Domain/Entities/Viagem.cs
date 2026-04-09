namespace WebApplication2.Domain.Entities
{
    public class Viagem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Nome { get; set; }
        public DateTime DataChegada { get; set; }
        public DateTime DataSaida { get; set; }
        public double Valor { get; set; }
        public ICollection<Destino>? Destinos { get; set; }
    }
}
