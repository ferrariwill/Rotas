namespace Data
{
    using System.Collections.Generic;
    using Domain;

    public interface IRotaRepository
    {
        List<Rota> ObterRotas();
        bool AdicionarRota(Rota rota);
    }
}
