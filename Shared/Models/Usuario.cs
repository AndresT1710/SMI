using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMI.Shared.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public int Id_Persona { get; set; } // clave foránea
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }

        // Propiedad de navegación hacia la tabla Persona
        public Persona Persona { get; set; } = null!;

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}

