using System;
using System.IO;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Xunit;

namespace RotaDeViagem.Tests
{
    public class RotaServiceTests
    {
        [Fact]
        public void TestarMainComEntradaValida()
        {
            // Criando mocks para as dependências
            var mockRotaService = new Mock<RotaService>();
            var mockRepository = new Mock<RotaRepository>();

            // Configurando o mock para o método ObterMelhorRota
            mockRotaService
                .Setup(s => s.ObterMelhorRota("GRU", "CDG"))
                .Returns("GRU - BRC - SCL - ORL - CDG ao custo de $40");

            // Simulando a criação de rotas
            mockRepository.Setup(r => r.CriarRotasEmArquivo(It.IsAny<string>())).Verifiable();

            using (var consoleOutput = new StringWriter())
            {
                Console.SetOut(consoleOutput);

                // Simulando a entrada do usuário
                var input = new StringReader("GRU-CDG\n"); // Simula o input "GRU-CDG"
                Console.SetIn(input);

                Program.Main(Array.Empty<string>());

                // Verificando se a saída contém a rota esperada
                var result = consoleOutput.ToString();
                Assert.Contains("Melhor Rota: GRU - BRC - SCL - ORL - CDG ao custo de $40", result);
            }

            // Verificando se o método CriarRotasEmArquivo foi chamado
            mockRepository.Verify(r => r.CriarRotasEmArquivo(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void TestarMainComEntradaInvalida()
        {
            // Criando mocks para as dependências
            var mockRotaService = new Mock<RotaService>();
            var mockRepository = new Mock<RotaRepository>();

            // Simulando a criação de rotas
            mockRepository.Setup(r => r.CriarRotasEmArquivo(It.IsAny<string>())).Verifiable();

            using (var consoleOutput = new StringWriter())
            {
                Console.SetOut(consoleOutput);

                // Simulando a entrada do usuário com valor inválido
                var input = new StringReader("GRU-\n"); // Simula o input inválido "GRU-"
                Console.SetIn(input);

                Program.Main(Array.Empty<string>());

                // Verificando se o programa não retornou uma saída inesperada
                var result = consoleOutput.ToString();
                Assert.Contains("Digite a rota (origem-destino):", result);
            }

            // Verificando se o método CriarRotasEmArquivo foi chamado
            mockRepository.Verify(r => r.CriarRotasEmArquivo(It.IsAny<string>()), Times.Once);
        }
    }
}
