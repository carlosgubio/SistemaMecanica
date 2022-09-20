using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Dtos
{
    public class LoginsDto
    {
        public int IdLogin { get; set; }
        public int IdCliente { get; set; }
        public int IdProfissional { get; set; }
        public int Usuario { get; set; }
        public int Senha { get; set; }
    }
}
