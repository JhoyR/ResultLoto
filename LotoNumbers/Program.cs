using LotoNumbers;
using static LotoNumbers.Premiacoes;

public class Program
{

    public static void Main()
    {
        LoteriaProgram();
    }
    public static void LoteriaProgram()
    {
        Options options = new Options();
        string loteria = options.LotoOptions();

        Console.WriteLine($"Selecionado o jogo: {loteria}");

        LoteriaAPI loteriaAPI = new LoteriaAPI();
        LoteriaInfo loteriaInfo = loteriaAPI.GetLatestLoteriaInfo(loteria);

        InformacoesJogos informacoes = new InformacoesJogos();
        informacoes.InformacoesJogo(loteriaInfo);

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1. Consultar outros jogos");
            Console.WriteLine("2. Verificar seus jogos");
            Console.WriteLine("3. Sair");
            Console.WriteLine();
            Console.WriteLine();

            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    loteria = options.LotoOptions();
                    Console.WriteLine($"Selecionado o jogo: {loteria}");

                    loteriaInfo = loteriaAPI.GetLatestLoteriaInfo(loteria);

                    informacoes.InformacoesJogo(loteriaInfo);
                    break;

                case "2":
                    while (true)
                    {
                        Console.WriteLine("Digite uma aposta (separando os números por vírgula) ou 'exit' para sair:");
                        var apostaInput = Console.ReadLine();

                        if (apostaInput == "exit")
                        {
                            break;
                        }

                        var aposta = new List<int>();
                        var apostaNumeros = apostaInput.Split(',');

                        foreach (var numero in apostaNumeros)
                        {
                            aposta.Add(int.Parse(numero.Trim()));
                        }

                        var acertos = aposta.FindAll(numero => loteriaInfo.Dezenas.Contains(numero)).Count;

                        Console.WriteLine($"Acertos: {acertos}");
                    }
                    break;

                case "3":
                    Console.WriteLine("Saindo...");
                    return;

                default:
                    Console.WriteLine("Opção inválida. Escolha novamente.");
                    break;
            }
        }
    }
}