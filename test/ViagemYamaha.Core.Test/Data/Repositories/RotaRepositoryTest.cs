using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViagemYamaha.Core.Data.Repositories;
using ViagemYamaha.Core.Settings;
using ViagemYamaha.Core.Test.Build;
using Xunit;

namespace ViagemYamaha.Core.Test.Data.Repositories
{
    public class RotaRepositoryTest
    {
        private string _filePath = "C:\\temp\\RotasTest.csv";

        public RotaRepositoryTest()
        {
            new FileBuild().CreateCsvFile(_filePath);
        }

        [Fact]
        public void Arquivo_Rotas_Inexistente_Deve_Lancar_Exception()
        {
            IOptions<FileSettings> options = Options.Create<FileSettings>(new FileSettings { Path = "C:\\RotasTest.csv" });

            Assert.Throws<ApplicationException>(() => new RotaRepository(options));
        }

        [Fact]
        public async Task Rota_nao_Exitente_Success()
        {
            IOptions<FileSettings> options = Options.Create<FileSettings>(new FileSettings { Path = _filePath });
            var repository = new RotaRepository(options);

            var result = await repository.ObterMelhorRotaAsync("GUA", "CCC");

            Assert.Null(result);
        }

        [Fact]
        public async Task Obter_Melhor_Rota_Success()
        {
            IOptions<FileSettings> options = Options.Create<FileSettings>(new FileSettings { Path = _filePath });
            var repository = new RotaRepository(options);

            var result = await repository.ObterMelhorRotaAsync("GUA", "CDG");

            Assert.NotNull(result);
            Assert.Equal("GUA", result.Origem);
            Assert.Equal("CDG", result.Destino);
            Assert.Equal(65, result.Valor);
            Assert.Equal("MAR - TST", result.Escalas);
        }

        [Fact]
        public async Task Adicionar_Rotas_Success()
        {
            IOptions<FileSettings> options = Options.Create<FileSettings>(new FileSettings { Path = _filePath });
            var repository = new RotaRepository(options);
            var rotas = new List<string> { "JDI,GUA,15" };

            await repository.AdicionarAsync(rotas);

            var result = await repository.ObterMelhorRotaAsync("JDI", "GUA");

            Assert.NotNull(result);
            Assert.Equal("JDI", result.Origem);
            Assert.Equal("GUA", result.Destino);
            Assert.Equal(15, result.Valor);
            Assert.Null(result.Escalas);
        }

        [Fact]
        public async Task Validar_Rota_Cadastrada()
        {
            IOptions<FileSettings> options = Options.Create<FileSettings>(new FileSettings { Path = _filePath });
            var repository = new RotaRepository(options);

            var result = await repository.ValidarRotaCadastradaAsync("BRC,SCL,5");

            Assert.True(result);

        }

        [Fact]
        public async Task Validar_Rota_Nao_Cadastrada()
        {
            IOptions<FileSettings> options = Options.Create<FileSettings>(new FileSettings { Path = _filePath });
            var repository = new RotaRepository(options);

            var result = await repository.ValidarRotaCadastradaAsync("JDI,GUA,15");

            Assert.False(result);

        }
    }
}

//GUA,BRC,10
//GUA,VCF,DFE,4
//GUA,ZXA,ZAZ,DFE,4
//GUA,ZXA,ZAZ,DFE,FDS,FGG,4
//GUA,MAR,TST,CDG, 65,
//BRC,SCL,5
//GUA,CDG,75
//GUA,SCL,20
//GUA,ORL,56
//ORL,CDG,5
//SCL,ORL,20