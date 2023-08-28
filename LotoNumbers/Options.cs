using static LotoNumbers.Premiacoes;

namespace LotoNumbers;

public class Options
{
    public string LotoOptions()
    {
        var nome = new Loterias();
        var loteria = nome.loterias;

        var options = new Dictionary<string, string>
        {
            { "0", "Voltar" },
            { "1", "Mega-Sena" },
            { "2", "Lotofácil" },
            { "3", "Quina" },
            { "4", "Lotomania" },
            { "5", "Timemania" },
            { "6", "Dupla-Sena" },
            { "7", "Loteria-Federal" },
            { "8", "Dia-de-Sorte" },
            { "9", "Super-Sete" },
            { "10", "Mais Milionária" }
        };

        while (loteria == null)
        {
            Console.WriteLine();
            Console.WriteLine("Escolha o jogo desejado:");
            foreach (var option in options)
            {
                Console.WriteLine($"{option.Key}. {option.Value}");
            }

            var selectedOption = Console.ReadLine();

            if (options.ContainsKey(selectedOption!))
            {
                if (selectedOption == "0")
                {
                    Console.WriteLine("Voltando para a seleção de jogos...");
                }
                else
                {
                    loteria = selectedOption switch
                    {
                        "1" => "megasena",
                        "2" => "lotofacil",
                        "3" => "quina",
                        "4" => "lotomania",
                        "5" => "timemania",
                        "6" => "duplasena",
                        "7" => "loteriafederal",
                        "8" => "diadesorte",
                        "9" => "supersete",
                        "10" => "maismilionaria",
                        _ => null
                    };

                    if (loteria == null)
                    {
                        Console.WriteLine("Opção inválida. Escolha novamente.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Escolha novamente.");
            }
        }
        return loteria;
    } 
}
