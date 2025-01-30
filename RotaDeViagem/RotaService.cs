using System;
using System.Collections.Generic;
using System.Linq;

namespace RotaDeViagem
{
    public class RotaService
    {
        private readonly RotaRepository _repository;

        public RotaService(RotaRepository repository)
        {
            _repository = repository;
        }

        public string ObterMelhorRota(string origem, string destino)
        {
            var rotas = _repository.ObterTodasAsRotas();
            var rotasPossiveis = ObterRotasPossiveis(origem, destino, rotas);

            var melhorRota = rotasPossiveis
                .OrderBy(r => r.Item2) // Ordena pelo custo
                .FirstOrDefault();

            if (melhorRota != null)
            {
                return $"{string.Join(" - ", melhorRota.Item1)} ao custo de ${melhorRota.Item2}";
            }

            return "Rota não encontrada";
        }

        private List<Tuple<List<string>, int>> ObterRotasPossiveis(string origem, string destino, IEnumerable<Rota> rotas)
        {
            var rotasPossiveis = new List<Tuple<List<string>, int>>();

            foreach (var rota in rotas.Where(r => r.Origem == origem))
            {
                var caminho = new List<string> { origem, rota.Destino };
                var custo = rota.Valor;
                BuscarRotasRecursivas(rota.Destino, destino, caminho, custo, rotas, rotasPossiveis);
            }

            return rotasPossiveis;
        }

        private void BuscarRotasRecursivas(string origemAtual, string destino, List<string> caminho, int custo, IEnumerable<Rota> rotas, List<Tuple<List<string>, int>> rotasPossiveis)
        {
            if (origemAtual == destino)
            {
                rotasPossiveis.Add(new Tuple<List<string>, int>(new List<string>(caminho), custo));
                return;
            }

            foreach (var rota in rotas.Where(r => r.Origem == origemAtual))
            {
                if (!caminho.Contains(rota.Destino)) // Previne ciclos
                {
                    caminho.Add(rota.Destino);
                    BuscarRotasRecursivas(rota.Destino, destino, caminho, custo + rota.Valor, rotas, rotasPossiveis);
                    caminho.RemoveAt(caminho.Count - 1);
                }
            }
        }
    }
}