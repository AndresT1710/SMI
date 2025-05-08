using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMI.Shared.DTOs
{
    public class UsuarioCreadoDTO
    {
        public int Id { get; set; }
        public int Id_Persona { get; set; }
        public string Correo { get; set; }
        public bool Activo { get; set; }
    }
}
