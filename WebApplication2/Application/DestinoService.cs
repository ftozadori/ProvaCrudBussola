using WebApplication2.Domain.Entities;
using WebApplication2.Infra.Interfaces;

namespace WebApplication2.Application
{
    public class DestinoService
    {
        private readonly IDestinoRepository _repository;

        public DestinoService(IDestinoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Destino> Create(Destino destino, CancellationToken token = default)
        {
            await _repository.Create(destino, token);
            await _repository.SaveChangesAsync(token);
            return destino;
        }

        public async Task<Destino?> GetById(Guid id, CancellationToken token = default)
        {
            return await _repository.GetById(id, token);
        }

        public async Task<List<Destino>> FindAll(CancellationToken token = default)
        {
            return await _repository.FindAll(token);
        }

        public async Task<Destino> Update(Guid id, Destino periferico, CancellationToken token = default)
        {
            var existente = await _repository.GetById(id, token);

            if (existente == null)
                throw new Exception("Destino não encontrado");

            existente.Nome = periferico.Nome;
            existente.ViagemId = periferico.ViagemId;

            await _repository.Update(existente, token);
            await _repository.SaveChangesAsync(token);

            return existente;
        }

        public async Task Delete(Guid id, CancellationToken token = default)
        {
            var existente = await _repository.GetById(id, token);

            if (existente == null)
                throw new Exception("Destino não encontrado");

            await _repository.Delete(existente, token);
            await _repository.SaveChangesAsync(token);
        }

    }
}
