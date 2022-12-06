using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViagemYamaha.Core.Contracts.Repositories;
using ViagemYamaha.Core.Contracts.Services;
using ViagemYamaha.Core.Contracts.Ui;
using ViagemYamaha.Core.Data.Repositories;
using ViagemYamaha.Core.Domain;
using ViagemYamaha.Core.Services;
using ViagemYamaha.Core.Settings;
using Xunit;

namespace ViagemYamaha.Core.Test.Services
{
    public class RotaServiceTest
    {
        private readonly IRotaService _rotaService;
        private readonly Mock<IRotaRepository> _repositoryMock;

        public RotaServiceTest()
        {
            Mock<ILogger<RotaService>> _loggerMock = new Mock<ILogger<RotaService>>();
            _repositoryMock = new Mock<IRotaRepository>();
            _rotaService = new RotaService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task Adicionar_Rota_Deve_Gerar_Exception_Origem_Vazio()
        {
            var request = new PostRotaRequest
            {
                Origem = "",
                Destino = "",
                Escalas = null,
                Valor = 0
            };

            Func<Task> action = () =>  _rotaService.AdicionarRotaAsync(request);

            ApplicationException exception = await Assert.ThrowsAsync<ApplicationException>(action);
            Assert.Equal("Origem inválido", exception.Message);
        }

        [Fact]
        public async Task Adicionar_Rota_Deve_Gerar_Exception_Destino_Vazio()
        {
            var request = new PostRotaRequest
            {
                Origem = "SSP",
                Destino = "",
                Escalas = null,
                Valor = 0
            };

            Func<Task> action = () => _rotaService.AdicionarRotaAsync(request);

            ApplicationException exception = await Assert.ThrowsAsync<ApplicationException>(action);
            Assert.Equal("Destino inválido.", exception.Message);
        }

        [Fact]
        public async Task Adicionar_Rota_Deve_Gerar_Exception_Valor_Invalido()
        {
            var request = new PostRotaRequest
            {
                Origem = "SSP",
                Destino = "CMP",
                Escalas = null,
                Valor = 0
            };

            Func<Task> action = () => _rotaService.AdicionarRotaAsync(request);

            ApplicationException exception = await Assert.ThrowsAsync<ApplicationException>(action);
            Assert.Equal("Valor inválido.", exception.Message);
        }

        [Fact]
        public async Task Adicionar_Rota_Success()
        {
            _repositoryMock.Setup(x => x.ValidarRotaCadastradaAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _repositoryMock.Setup(x => x.AdicionarAsync(It.IsAny<List<string>>()));

            var request = new PostRotaRequest
            {
                Origem = "SSP",
                Destino = "CMP",
                Escalas = null,
                Valor = 10
            };

             await _rotaService.AdicionarRotaAsync(request);
        }

        [Fact]
        public async Task ObterMelhor_Rota_Success()
        {
            _repositoryMock.Setup(x => x.ObterMelhorRotaAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Rota("SSP,CMP,10"));

            var request = new GetRotaRequest
            {
                Destino = "SSP",
                Origem = "CMP"
            };

            var result = await _rotaService.ObterMelhorRotaAsync(request);

            Assert.NotNull(result);
            Assert.Contains("SSP", result);
            Assert.Contains("CMP", result);
            Assert.Contains("10", result);
        }
    }
}
