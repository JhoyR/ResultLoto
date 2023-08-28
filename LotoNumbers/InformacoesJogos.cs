using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LotoNumbers.Premiacoes;

namespace LotoNumbers;

public class InformacoesJogos
{
    public void InformacoesJogo(LoteriaInfo loteriaInfo)
    {
        Console.WriteLine();
        Console.WriteLine($"Loteria: {loteriaInfo.Loteria}");
        Console.WriteLine($"Concurso: {loteriaInfo.Concurso}");
        Console.WriteLine($"Data: {loteriaInfo.Data.ToString("dd/MM/yyyy")}");
        Console.Write("Numeros sorteados: ");
        Console.WriteLine(string.Join(", ", loteriaInfo.Dezenas));
        Console.Write("Premiações: ");
        foreach (var premiacao in loteriaInfo.Premiacoes)
        {
            Console.WriteLine($"- Acertos: {premiacao.Descricao}");
            Console.WriteLine($"  Faixa: {premiacao.Faixa}");
            Console.WriteLine($"  Vencedores: {premiacao.Ganhadores}");
            Console.WriteLine($"  Prêmio: R${premiacao.ValorPremio}");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
