using System.Collections.Generic;

namespace ViagemYamaha.Core.Contracts.Ui
{
    public class PostRotaRequest
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
        public List<string> Escalas { get; set; }
        public decimal Valor  { get; set; }
    }

}
