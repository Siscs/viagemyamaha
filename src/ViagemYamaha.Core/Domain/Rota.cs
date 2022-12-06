using System.Linq;

namespace ViagemYamaha.Core.Domain
{
    public class Rota
    {
        public string Origem { get; set; }
        public string Escalas { get; set; }
        public string Destino { get; set; }
        public decimal Valor { get; set; }
        public bool Valid { get; set; }

        public Rota(string rota)
        {
            var splitRota = rota.Split(',');
            var len = splitRota.Length;

            if (len < 3)
            {
                Valid = false;
                return;
            }

            Origem = splitRota[0];
            Destino = splitRota[len - 2];
            Valor = decimal.Parse(splitRota[len - 1]);
            
            if(len > 3)
            {
                for (int i = 1; i < len - 2; i++)
                {
                    Escalas += (i == 1 ? "" : " - ") + splitRota[i];
                }
            }

            Valid = true;
        }
    }
}
