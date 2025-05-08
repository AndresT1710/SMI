using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMI.Shared.DTOs
{
    public class AuthResponseDTO
    {
        public UsuarioDTO Usuario { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
