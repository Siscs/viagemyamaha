using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViagemYamaha.Core.Contracts.Repositories;
using ViagemYamaha.Core.Contracts.Services;
using ViagemYamaha.Core.Contracts.Ui;
using ViagemYamaha.Core.Domain;

namespace ViagemYamaha.Core.Services
{
    public class RotaService : IRotaService
    {
        private readonly ILogger<RotaService> _logger;
        private readonly IRotaRepository _rotaRepository;

        public RotaService(ILogger<RotaService> logger,
                           IRotaRepository rotaRepository)
        {
            _logger = logger;
            _rotaRepository = rotaRepository;
        }

        public async Task<string> ObterMelhorRotaAsync(GetRotaRequest request)
        {
            GenerateLog($"Obter melhor rota: {request.Origem.ToUpper()} - {request.Destino.ToUpper()}");
            var rota = await _rotaRepository.ObterMelhorRotaAsync(request.Origem.ToUpper(), request.Destino.ToUpper());
            return FormatarRota(rota);
        }

        public async Task AdicionarRotaAsync(PostRotaRequest request)
        {
            // Poderia aqui usar o fluentValidator
            // não usei somente por recomendação de não usar bibliotecas

            if (string.IsNullOrEmpty(request.Origem))
                GenerateAppException("Origem inválido");

            if (string.IsNullOrEmpty(request.Destino))
                GenerateAppException("Destino inválido.");

            if (request.Valor <= 0)
                GenerateAppException("Valor inválido.");

            var rotaCsv = ConverterRotaFormatoCsv(request);

            GenerateLog($"Adicionando rota: {rotaCsv}");

            var existeRota = await _rotaRepository.ValidarRotaCadastradaAsync(rotaCsv);

            if (existeRota)
                GenerateAppException("Rota já existe.");

            var rotas = new List<string>();
            rotas.Add(rotaCsv);

            await _rotaRepository.AdicionarAsync(rotas);
        }

        private string ConverterRotaFormatoCsv(PostRotaRequest request)
        {
            var escalas = string.Empty;

            if (request.Escalas != null && request.Escalas.Any())
                escalas = string.Join(',', request.Escalas) + ",";

            return $"{request.Origem.ToUpper()},{escalas.ToUpper()}{request.Destino.ToUpper()},{request.Valor}";
        }

        private string FormatarRota(Rota rota)
        {
            if (rota is null)
                return null;

            return $"{rota.Origem} - { (rota.Escalas == null ? string.Empty : rota.Escalas + " - ")}{rota.Destino} ao custo de {rota.Valor.ToString("C0")}";
        }

        private void GenerateAppException(string message)
        {
            _logger.LogInformation($"{nameof(RotaService)} - {message}");
            throw new ApplicationException(message);
        }

        private void GenerateLog(string message)
        {
            _logger.LogInformation($"{nameof(RotaService)} - {message}");
        }

    }
}
