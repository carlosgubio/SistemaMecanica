using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Dtos.Login
{
    public class LoginDto
    {
        public int IdLogin { get; set; }
        public int IdCliente { get; set; }
        public int IdProfissional { get; set; }
        public int Usuario { get; set; }
        public int Senha { get; set; }
        

    }
}
