namespace Service
{
    using Domain;

    public interface IRotaService
    {
        MelhorRota ObterMelhorRota(string origem, string destino);
        bool RegistrarRota(Rota rota);

        bool RotaExiste(string origem,string destino);
    }
}
