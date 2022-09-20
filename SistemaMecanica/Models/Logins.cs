using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Models
{
    public class Logins
    {
        public int IdLogin { get; set; }
        public int IdCliente { get; set; }
        public int IdProfissional { get; set; }
        public int Usuario { get; set; }
        public int Senha { get; set; }
    }
}
