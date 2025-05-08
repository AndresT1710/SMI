using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMI.Shared.Models
{
    public class PersonaProfesion
    {
        public int id_Persona { get; set; }
        public int id_Profesion { get; set; }
        // Propiedades de navegación
        public virtual Persona Persona { get; set; }
        public virtual Profesion Profesion { get; set; }
    }
}
