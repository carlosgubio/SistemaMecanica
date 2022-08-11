using SistemaMecanica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.ViewModels
{
    public class SalvarClienteViewModel
    {
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string EnderecoCliente { get; set; }
        public string VeiculoCliente { get; set; }
        public string PlacaVeiculoCliente { get; set; }
        public string CorVeiculoCliente { get; set; }
    }
}
