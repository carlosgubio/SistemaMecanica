using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Models
{
    public class OrdensServico
    {
        public List<int> IdItens { get; set; }
        public List<int> IdProfissionais { get; set; }
        public List<int> IdServicosExecutados { get; set; }
        public int IdOrdemServico { get; set; }        
        public int IdCliente { get; set; }
        public int IdVeiculo { get; set; }
        public float TotalGeral { get; set; }
    }
}
