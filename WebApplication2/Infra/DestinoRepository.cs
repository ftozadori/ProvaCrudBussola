using WebApplication2.Domain.Entities;
using WebApplication2.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Infra
{
    public class DestinoRepository : IDestinoRepository
    {
        private readonly AppDbContext _context;

        public DestinoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Destino> Create(Destino destino, CancellationToken token = default)
        {
            await _context.Destinos.AddAsync(destino, token);
            return destino;
        }

        public async Task<Destino?> GetById(Guid id, CancellationToken token = default)
        {
            return await _context.Destinos
                .Include(x => x.Viagem)
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<List<Destino>> FindAll(CancellationToken token = default)
        {
            return await _context.Destinos
                .Include(p => p.Viagem)
                .ToListAsync(token);
        }

        public Task<Destino> Update(Destino destino, CancellationToken token = default)
        {
            _context.Destinos.Update(destino);
            return Task.FromResult(destino);
        }

        public Task Delete(Destino destino, CancellationToken token = default)
        {
            _context.Destinos.Remove(destino);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken token = default)
        {
            await _context.SaveChangesAsync(token);
        }
    }
}
