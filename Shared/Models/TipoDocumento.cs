using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMI.Shared.Models
{
    public partial class TipoDocumento
    {
        public int id { get; set; }
        public string nombre { get; set; }

        public ICollection<PersonaDocumento> PersonaDocumentos { get; set; } = new List<PersonaDocumento>();
    }

}
