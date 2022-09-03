using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Dtos.Profissionais
{
    public class ProfissionaisDto
    {
        public int IdProfissional { get; set; }
        public string NomeProfissional { get; set; }
        public string CargoProfissional { get; set; }
        public List<ProfissionaisDto> execucao { get; set; }
    }
}
