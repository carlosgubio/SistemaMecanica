using SistemaMecanica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.ViewModels
{
    public class SalvarClienteViewModel
    {
        public Clientes Clientes  { get; set; }
        public Profissionais Profissionais { get; set; }
        public Servicos Servicos { get; set; }
        public Produtos Produtos { get; set; }
    }
}
