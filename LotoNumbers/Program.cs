using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using Newtonsoft.Json;

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.Value != null)
        {
            return DateTime.ParseExact(reader.Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        return DateTime.MinValue;
    }

    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString("dd/MM/yyyy"));
    }
}

public class LoteriaInfo
{
    public string Nome { get; set; }

    public string Concurso { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Data { get; set; }

    public List<int> Dezenas { get; set; }

    public List<Premiacao> Premiacoes { get; set; }
}

public class Premiacao
{
    public string Acertos { get; set; }
    public int Vencedores { get; set; }
    public string Premio { get; set; }
}

public class Program
{
    public static void Main()
    {
        var options = new Dictionary<string, string>
            {
                { "1", "Mega-Sena" },
                { "2", "Lotofácil" },
                { "3", "Quina" },
                { "4", "Lotomania" },
                { "5", "Timemania" },
                { "6", "Dupla-Sena" },
                { "7", "Loteria-Federal" },
                { "8", "Dia-de-Sorte" },
                { "9", "Super-Sete" }
            };

        string loteria = null;

        while (loteria == null)
        {
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
                        "1" => "mega-sena",
                        "2" => "lotofacil",
                        "3" => "quina",
                        "4" => "lotomania",
                        "5" => "timemania",
                        "6" => "dupla-sena",
                        "7" => "loteria-federal",
                        "8" => "dia-de-sorte",
                        "9" => "super-sete",
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

        Console.WriteLine($"Selecionado o jogo: {options.FirstOrDefault(x => x.Value.Equals(loteria, StringComparison.OrdinalIgnoreCase)).Value}");

        var httpClient = new HttpClient();
        var response = httpClient.GetAsync($"https://loteriascaixa-api.herokuapp.com/api/{loteria}/latest").Result;
        var responseContent = response.Content.ReadAsStringAsync().Result;

        var loteriaInfo = JsonConvert.DeserializeObject<LoteriaInfo>(responseContent);

        Console.WriteLine($"Loteria: {loteriaInfo.Nome}");
        Console.WriteLine($"Concurso: {loteriaInfo.Concurso}");
        Console.WriteLine($"Data: {loteriaInfo.Data.ToString("dd/MM/yyyy")}");
        Console.Write("Numeros sorteados: ");
        Console.WriteLine(string.Join(", ", loteriaInfo.Dezenas));
        Console.Write("Premiações: ");
        foreach (var premiacao in loteriaInfo.Premiacoes)
        {
            Console.WriteLine($"- Acertos: {premiacao.Acertos}");
            Console.WriteLine($"  Vencedores: {premiacao.Vencedores}");
            Console.WriteLine($"  Prêmio: {premiacao.Premio.Replace(".", "").Replace(",", ".")}");
            Console.WriteLine();
        }
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
    }
}