using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMI.Shared.Models;

namespace SMI.Shared.DTOs
{
    public class PersonaConDocumentosDTO
    {
        public Persona persona { get; set; }
        public List<DocumentoDTO> documentos { get; set; }
    }

}
