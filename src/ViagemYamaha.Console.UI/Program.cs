using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using ViagemYamaha.Core.Services;

namespace ViagemYamaha.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug);
                    //.AddConsole();
            });

            ILogger<RotaService> logger = loggerFactory.CreateLogger<RotaService>();

            System.Console.WriteLine("Bem vindo ao Yamaha viagens!");
            System.Console.WriteLine(new String('=', 50));

            if(args == null || args.Length <= 0)
            {
                System.Console.WriteLine("Parâmetro de arquivo de rotas não informado.");
                Environment.Exit(0);
            }

            var rotasProgram = new Rotas(args[0], logger);

            rotasProgram.PesquisarRotas().Wait();

            Environment.Exit(0);

        }
    }
}