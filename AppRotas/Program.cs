using Service;
using Domain;
using Data;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Erro: Caminho do arquivo CSV não fornecido.");
            Console.WriteLine("Uso: executavel <caminho do arquivo CSV>");
            return;
        }
        var caminhoArquivoCsv = args[0];

        if (!File.Exists(caminhoArquivoCsv))
        {
            Console.WriteLine($"Erro: O arquivo '{caminhoArquivoCsv}' não foi encontrado.");
            return;
        }

        if (Path.GetExtension(caminhoArquivoCsv).ToLower() != ".csv")
        {
            Console.WriteLine("Erro: O arquivo fornecido não é um arquivo CSV.");
            return;
        }

        var rotaRepository = new RotaRepository(caminhoArquivoCsv);
        var rotaService = new RotaService(rotaRepository);

        bool continuar = true;
        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("### Menu ###");
            Console.WriteLine("1 - Consultar melhor rota");
            Console.WriteLine("2 - Adicionar uma nova rota");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ConsultarMelhorRota(rotaService);
                    break;

                case "2":
                    AdicionarNovaRota(rotaService);
                    break;

                case "0":
                    continuar = false;
                    Console.WriteLine("Saindo...");
                    break;

                default:
                    Console.WriteLine("Opção inválida! Pressione qualquer tecla para tentar novamente...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void ConsultarMelhorRota(RotaService rotaService)
    {
        Console.Clear();
        Console.WriteLine("Digite a rota no formato 'Origem-Destino' (exemplo: GRU-CDG):");
        var entrada = Console.ReadLine();

        if (string.IsNullOrEmpty(entrada) || !entrada.Contains('-'))
        {
            Console.WriteLine("Formato inválido. O formato correto é 'Origem-Destino'.");
            Console.ReadKey();
            return;
        }

        var partes = entrada.Split('-');
        var origem = partes[0].Trim();
        var destino = partes[1].Trim();

        var melhorRota = rotaService.ObterMelhorRota(origem.ToUpper(), destino.ToUpper());

        if (melhorRota != null)
        {
            Console.WriteLine($"Melhor Rota: {melhorRota.DescCaminho} ao custo de ${melhorRota.Valor}");
        }
        else
        {
            Console.WriteLine("Não foi possível encontrar uma rota entre os pontos informados.");
        }

        Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }

    
    static void AdicionarNovaRota(RotaService rotaService)
    {
        Console.Clear();
        Console.WriteLine("Digite a origem da nova rota:");
        var origem = Console.ReadLine();
        Console.WriteLine("Digite o destino da nova rota:");
        var destino = Console.ReadLine();
        Console.WriteLine("Digite o valor da rota:");
        var valor = decimal.Parse(Console.ReadLine());

        var novaRota = new Rota();

        novaRota.Origem = origem;
        novaRota.Destino = destino;
        novaRota.Valor = valor;

        rotaService.RegistrarRota(novaRota);

        Console.WriteLine("Rota registrada com sucesso!");

        Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }
}
