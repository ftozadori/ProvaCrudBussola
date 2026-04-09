using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Domain.Entities;

namespace WebApplication2.Infra.EntitiesMapping
{
    public class ViagemConfiguracao : IEntityTypeConfiguration<Viagem>
    {
        public void Configure(EntityTypeBuilder<Viagem> builder)
        {
            builder.ToTable(nameof(Viagem));
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Destinos)
                .WithOne(x => x.Viagem)
                .HasForeignKey(x => x.ViagemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
