using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMI.Shared.Models;

namespace SMI.Shared.DTOs
{
    public class PersonaConProfesionYCorreoDTO
    {
        public Persona persona { get; set; }
        public int idProfesion { get; set; }
        public string correo { get; set; }
    }

}
