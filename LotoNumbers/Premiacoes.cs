using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoNumbers
{
    public class Premiacoes
    {
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
            public string Loteria { get; set; }

            public string Concurso { get; set; }

            [JsonConverter(typeof(DateTimeConverter))]
            public DateTime Data { get; set; }

            public List<int> Dezenas { get; set; }

            public List<Premiacao> Premiacoes { get; set; }
        }

        public class Premiacao
        {

            public string Descricao { get; set; }
            public int Faixa { get; set; }
            public int Ganhadores { get; set; }
            public decimal ValorPremio { get; set; }
        }

        public class Loterias
        {
            public string loterias { get; set; }
        }

    }
}
