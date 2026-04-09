using WebApplication2.Domain.Entities;
using WebApplication2.Infra.Interfaces;

namespace WebApplication2.Application
{
    public class ViagemService
    {
        private readonly IViagemRepository _repo;

        public ViagemService(IViagemRepository repo)
        {
            _repo = repo;
        }

        public async Task<Viagem> Create(
            Viagem viagem,
            CancellationToken token = default
            )
        {
            return await _repo.Create(viagem, token);
        }

        public async Task<Viagem?> GetById(
            Guid id,
            CancellationToken token = default
            )
        {
            var viagem = await _repo.GetById(id, token)
                ?? throw new Exception("Viagem não encontrada.");

            return viagem;
        }

        public async Task<Viagem> Update(
            Guid id,
            Viagem viagem,
            CancellationToken token = default
            )
        {
            var viagemEncontrada = await this.GetById(id, token)
                ?? throw new Exception("Viagem não encontrada.");

            viagemEncontrada.Nome = viagem.Nome;
            await _repo.Update(viagemEncontrada, token);
            return viagemEncontrada;
        }

        public async Task<List<Viagem>> FindAll(CancellationToken token = default)
        {
            return await _repo.FindAll(token);
        }
        public async Task Delete(
            Guid id,
            CancellationToken token = default
            )
        {
            var viagem = await _repo.GetById(id, token)
                ?? throw new Exception("Viagem não encontrada.");

            await _repo.Delete(viagem, token);
        }
    }
}
