namespace Tests
{
    using NUnit.Framework;
    using Service;
    using Domain;
    using System.Collections.Generic;
    using Data;

    public class Tests
    {
        private RotaService _rotaService;
        private RotaRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new RotaRepository(Path.Combine(Directory.GetCurrentDirectory(), "rotas.csv"));
        }

        [Test]
        public void TesteMelhorRota()
        {
            var melhorRota = _rotaService.ObterMelhorRota("GRU", "CDG");

            Assert.AreEqual("GRU - BRC - SCL - ORL - CDG",melhorRota.DescCaminho);
            Assert.AreEqual(40, melhorRota.Valor);
        }

        [Test]
        public void TesteRotaExiste()
        {
            var retorno = _rotaService.RotaExiste("TESTE", "TESTE");

            Assert.IsFalse(retorno);
        }

        [Test]
        public void TesteIncluiRota()
        {
            Rota novaRota = new Rota();
            novaRota.Destino = "TESTE1";
            novaRota.Origem = "TESTE2";
            novaRota.Valor = 10;
            var retorno = _rotaService.RegistrarRota(novaRota);

            Assert.IsTrue(retorno);
        }

    }
}