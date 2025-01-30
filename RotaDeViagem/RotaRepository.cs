using System;
using System.Collections.Generic;
using System.IO;

namespace RotaDeViagem
{
    public class RotaRepository
    {
        private readonly List<Rota> _rotas;

        public RotaRepository()
        {
            _rotas = new List<Rota>();
        }

        public void AdicionarRota(Rota rota)
        {
            _rotas.Add(rota);
        }

        public IEnumerable<Rota> ObterTodasAsRotas()
        {
            return _rotas;
        }

        public void CriarRotasEmArquivo(string caminho)
        {
            // Criando a lista para armazenar as rotas
            var sistema = new List<Tuple<string, string, int>>
            {

                // Adicionando as rotas à lista
                new Tuple<string, string, int>("GRU", "BRC", 10),
                new Tuple<string, string, int>("BRC", "SCL", 5),
                new Tuple<string, string, int>("GRU", "CDG", 75),
                new Tuple<string, string, int>("GRU", "SCL", 20),
                new Tuple<string, string, int>("GRU", "ORL", 56),
                new Tuple<string, string, int>("ORL", "CDG", 5),
                new Tuple<string, string, int>("SCL", "ORL", 20)
            };

            // Gravar as rotas no arquivo .txt
            using (StreamWriter writer = new(caminho))
            {
                foreach (var rota in sistema)
                {
                    writer.WriteLine($"{rota.Item1},{rota.Item2},{rota.Item3}");
                }
            }

            CarregarRotasDeArquivo(caminho);
        }

        public void CarregarRotasDeArquivo(string caminho)
        {
            foreach (var linha in File.ReadLines(caminho))
            {
                var partes = linha.Split(',');
                var rota = new Rota(partes[0], partes[1], int.Parse(partes[2]));
                AdicionarRota(rota);
            }
        }

    }
}