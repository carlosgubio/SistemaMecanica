using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.ViewModels
{
    public class SalvarOrdemServicoViewModel
    {
        public int IdOrdemServico { get; set; }
        public int IdProfissional { get; set; }
        public int IdServico { get; set; }
        public int IdPeca { get; set; }
        public float TotalGeral { get; set; }
    }
}
