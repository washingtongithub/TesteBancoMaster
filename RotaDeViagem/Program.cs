using System;
namespace RotaDeViagem
{
    class Program
    {
        static void Main()
        {
            var repository = new RotaRepository();
            var service = new RotaService(repository);
            repository.CriarRotasEmArquivo("rotas.txt");

            while (true)
            {
                Console.WriteLine("Digite a rota (origem-destino):");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) break;

                var partes = input.Split('-');
                if (partes.Length == 2)
                {
                    var origem = partes[0];
                    var destino = partes[1];

                    var resultado = service.ObterMelhorRota(origem, destino);
                    Console.WriteLine($"Melhor Rota: {resultado}");
                }
            }
        }
    }
}