using WebApplication2.Domain.Entities;
using WebApplication2.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Infra
{
    public class ViagemRepository : IViagemRepository
    {
        private readonly AppDbContext _db;

        public ViagemRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Viagem> Create(
            Viagem viagem,
            CancellationToken token = default)
        {
            await _db.Viagens.AddAsync(viagem, token);
            await SaveChangesAsync(token);
            return viagem;
        }

        public async Task Delete(
            Viagem viagem,
            CancellationToken token = default)
        {
            _db.Viagens.Remove(viagem);
            await SaveChangesAsync(token);
        }

        public async Task<List<Viagem>> FindAll(CancellationToken token = default)
        {
            return await _db.Viagens
                .AsNoTracking()
                .Include(x => x.Destinos)
                .ToListAsync(token);
        }

        public async Task<Viagem?> GetById(
            Guid id,
            CancellationToken token = default
            )
        {
            return await _db.Viagens.FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<Viagem> Update(
            Viagem viagem,
            CancellationToken token = default
            )
        {
            _db.Viagens.Update(viagem);
            await SaveChangesAsync(token);
            return viagem;
        }

        public async Task SaveChangesAsync(CancellationToken token = default)
        {
            await _db.SaveChangesAsync(token);
        }
    }
}
