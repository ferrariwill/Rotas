using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Rota
    {
        public string Origem { get; set; } = String.Empty;
        public string Destino { get; set; }= String.Empty;
        public decimal Valor { get; set; }
    }
}
