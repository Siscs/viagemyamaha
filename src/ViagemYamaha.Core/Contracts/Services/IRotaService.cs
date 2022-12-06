using System.Threading.Tasks;
using ViagemYamaha.Core.Contracts.Ui;
using ViagemYamaha.Core.Domain;

namespace ViagemYamaha.Core.Contracts.Services
{
    public interface IRotaService
    {
        Task<string> ObterMelhorRotaAsync(GetRotaRequest request);
        Task AdicionarRotaAsync(PostRotaRequest request);
    }
}
