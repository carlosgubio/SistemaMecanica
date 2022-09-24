using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.ViewModels
{
    public class CadastrarOrdemServicoViewModel
    {
        public List<int> IdItens { get; set; }
        public List<int> IdExecucoes { get; set; }
        public List<int> IdServicosExecutados { get; set; }
        public int IdOrdemServico { get; set; }
        public int IdProfissional { get; set; }
        public int IdCliente { get; set; }
        public int IdServico { get; set; }
        public int IdProduto { get; set; }
        public int IdVeiculo { get; set; }
        public float TotalGeral { get; set; }
    }
}
