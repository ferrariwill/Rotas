namespace Data
{
    using System.Collections.Generic;
    using System.IO;
    using Domain;

    public class RotaRepository : IRotaRepository
    {
        private readonly string _arquivo;

        public RotaRepository(string arquivo)
        {
            _arquivo = arquivo;
        }

        public List<Rota> ObterRotas()
        {
            var rotas = new List<Rota>();
            foreach (var linha in File.ReadAllLines(_arquivo))
            {
                var partes = linha.Split(',');
                rotas.Add(new Rota
                {
                    Origem = partes[0],
                    Destino = partes[1],
                    Valor = decimal.Parse(partes[2])
                });
            }
            return rotas;
        }

        public bool AdicionarRota(Rota rota)
        {
            try
            {
                var novaLinha = $"{rota.Origem.ToUpper()},{rota.Destino.ToUpper()},{rota.Valor:F2}";
                File.AppendAllText(_arquivo, novaLinha + "\n");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
