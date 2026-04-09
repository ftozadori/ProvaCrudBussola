using WebApplication2.Domain.Entities;

namespace WebApplication2.Infra.Interfaces
{
    public interface IViagemRepository
    {
        public Task<Viagem> Create(Viagem viagem, CancellationToken token = default);
        public Task<Viagem> GetById(Guid id, CancellationToken token = default);
        public Task<Viagem> Update(Viagem viagem, CancellationToken token = default);
        public Task<List<Viagem>> FindAll(CancellationToken token = default);
        public Task Delete(Viagem viagem, CancellationToken token = default);
        public Task SaveChangesAsync(CancellationToken token = default);
    }
}
