using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViagemYamaha.Core.Contracts.Repositories;
using ViagemYamaha.Core.Domain;
using ViagemYamaha.Core.Extensions;
using ViagemYamaha.Core.Settings;

namespace ViagemYamaha.Core.Data.Repositories
{
    public class RotaRepository : IRotaRepository
    {
        private readonly FileSettings _fileSettings;

        public RotaRepository(IOptions<FileSettings> options)
        {
            _fileSettings = options.Value;

            if (!_fileSettings.Path.FileExists())
                throw new ApplicationException("Arquivo de rotas não existe");
        }

        public async Task<Rota> ObterMelhorRotaAsync(string origem, string destino)
        {
            var rotas = CarregarRotas();

            var melhorRota = rotas
                .Where(r => r.Origem == origem && r.Destino == destino && r.Valid)
                .OrderBy(x => x.Valor).FirstOrDefault();

            return await Task.FromResult(melhorRota);
        }

        public async Task AdicionarAsync(List<string> rotas)
        {
            await File.AppendAllLinesAsync(_fileSettings.Path, rotas);
        }

        public async Task<bool> ValidarRotaCadastradaAsync(string rota)
        {
            var rotas = LerArquivo();
            return await Task.FromResult(rotas.Any(x => x == rota));
        }

        private List<Rota> CarregarRotas()
        {
            var fileLines = LerArquivo();

            if (fileLines != null && fileLines.Length > 0)
            {
                return fileLines
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => new Rota(x)).ToList();
            }

            return new List<Rota>();
        }

        private string[] LerArquivo()
        {
            return File.ReadAllLines(_fileSettings.Path);
        }

    }
}
