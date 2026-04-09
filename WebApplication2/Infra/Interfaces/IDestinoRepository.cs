using WebApplication2.Domain.Entities;

namespace WebApplication2.Infra.Interfaces
{
    public interface IDestinoRepository
    {
        public Task<Destino> Create(Destino destino, CancellationToken token = default);
        public Task<Destino> GetById(Guid id, CancellationToken token = default);
        public Task<Destino> Update(Destino destino, CancellationToken token = default);
        public Task<List<Destino>> FindAll(CancellationToken token = default);
        public Task Delete(Destino destino, CancellationToken token = default);
        public Task SaveChangesAsync(CancellationToken token = default);
    }
}
