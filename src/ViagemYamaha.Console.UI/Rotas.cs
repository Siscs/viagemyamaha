using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using ViagemYamaha.Core.Contracts.Ui;
using ViagemYamaha.Core.Data.Repositories;
using ViagemYamaha.Core.Services;
using ViagemYamaha.Core.Settings;

namespace ViagemYamaha.Console
{
    public class Rotas
    {
        private readonly string _fileRoutesPath;
        private readonly RotaService _rotaService;
        public Rotas(string fileRoutesPath, ILogger<RotaService> logger)
        {
            _fileRoutesPath = fileRoutesPath;
            IOptions<FileSettings> options = Options.Create(new FileSettings { Path = _fileRoutesPath });
            RotaRepository rotaRepository = new RotaRepository(options);
            _rotaService = new RotaService(logger, rotaRepository);
        }

        public async Task PesquisarRotas()
        {
            var stop = false;
            var rotaInput = string.Empty;
            while (!stop)
            {
                System.Console.Write("Digite a rota: ");
                rotaInput = System.Console.ReadLine();

                if (rotaInput.ToUpper() == "EXIT")
                {
                    stop = true;
                    continue;
                }

                var rotas = rotaInput.Split("-");

                if (rotas.Length < 2)
                { 
                    System.Console.WriteLine($"Rota inválida. informe ORIGEM-DESTINO");
                    continue;
                }

                try
                {
                    var request = new GetRotaRequest
                    {
                        Origem = rotas[0],
                        Destino = rotas[1]
                    };

                    var melhorRota = await _rotaService.ObterMelhorRotaAsync(request);

                    if(string.IsNullOrEmpty(melhorRota))
                    {
                        System.Console.WriteLine($"Não existe rota com a origem e destino informado.");
                        continue;
                    }

                    System.Console.WriteLine($"Melhor rota: {melhorRota}");

                }
                catch (ApplicationException ex)
                {
                    System.Console.WriteLine("Ocorreu o erro: ");
                    System.Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Ocorreu um erro desconhecido: ");
                    System.Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
