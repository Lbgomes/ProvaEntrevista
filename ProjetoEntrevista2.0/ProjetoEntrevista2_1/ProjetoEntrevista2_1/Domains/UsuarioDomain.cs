using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEntrevista2_1.Domains
{
    public class UsuarioDomain
    {
        public int IdUsuario{ get; set; }
        public int IdTipoUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public decimal NumeroDocumento { get; set; }
        public int Telefone { get; set; }
    }
}
