namespace Service
{
    using Data;
    using Domain;
    using System.Collections.Generic;
    using System.Linq;

    public class RotaService : IRotaService
    {
        private readonly IRotaRepository _rotaRepository;

        public RotaService(IRotaRepository rotaRepository)
        {
            _rotaRepository = rotaRepository;
        }

        public MelhorRota ObterMelhorRota(string origem, string destino)
        {
            var rotas = _rotaRepository.ObterRotas();
            var grafo = new Dictionary<string, List<(string destino, decimal custo)>>();

            foreach (var rota in rotas)
            {
                if (!grafo.ContainsKey(rota.Origem))
                {
                    grafo[rota.Origem] = new List<(string destino, decimal custo)>();
                }

                grafo[rota.Origem].Add((rota.Destino, rota.Valor));

                if (!grafo.ContainsKey(rota.Destino))
                {
                    grafo[rota.Destino] = new List<(string destino, decimal custo)>();
                }
            }

            var menoresCustos = new Dictionary<string, decimal>();
            var caminhos = new Dictionary<string, string>();
            var visitados = new HashSet<string>();

            foreach (var node in grafo.Keys)
            {
                menoresCustos[node] = decimal.MaxValue;
                caminhos[node] = null;
            }

            menoresCustos[origem] = 0;

            while (visitados.Count < grafo.Keys.Count)
            {
                var nodoAtual = menoresCustos
                    .Where(x => !visitados.Contains(x.Key))
                    .OrderBy(x => x.Value)
                    .FirstOrDefault().Key;

                if (nodoAtual == null || menoresCustos[nodoAtual] == decimal.MaxValue)
                    break;

                visitados.Add(nodoAtual);

                foreach (var (dest, custo) in grafo[nodoAtual])
                {
                    var novoCusto = menoresCustos[nodoAtual] + custo;
                    if (novoCusto < menoresCustos[dest])
                    {
                        menoresCustos[dest] = novoCusto;
                        caminhos[dest] = nodoAtual;
                    }
                }
            }

            if (!menoresCustos.ContainsKey(destino) || menoresCustos[destino] == decimal.MaxValue)
                return null;

            var caminho = new Stack<string>();
            var atual = destino;
            while (atual != null)
            {
                caminho.Push(atual);
                caminhos.TryGetValue(atual, out atual);
            }

            return new MelhorRota
            {
                DescCaminho = string.Join(" - ", caminho),
                Valor = menoresCustos[destino]
            };
        }

        public bool RotaExiste(string origem, string destino)
        {
            return _rotaRepository.ObterRotas()
                                    .Any(rota => rota.Destino.ToUpper() == destino.ToUpper() && 
                                                    rota.Origem.ToUpper() == origem.ToUpper());
        }

        public bool RegistrarRota(Rota rota)
        {
            
            return _rotaRepository.AdicionarRota(rota);
        }
    }
}
