using System.Collections.Generic;
using System.Threading.Tasks;
using ViagemYamaha.Core.Domain;

namespace ViagemYamaha.Core.Contracts.Repositories
{
    public interface IRotaRepository
    {
        Task<Rota> ObterMelhorRotaAsync(string origem, string destino);
        Task AdicionarAsync(List<string> rotas);
        Task<bool> ValidarRotaCadastradaAsync(string rota);
    }
}
