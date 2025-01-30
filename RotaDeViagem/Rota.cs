
namespace RotaDeViagem
{
    public class Rota
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
        public int Valor { get; set; }

        public Rota(string origem, string destino, int valor)
        {
            Origem = origem;
            Destino = destino;
            Valor = valor;
        }
    }
}