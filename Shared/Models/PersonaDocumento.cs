using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMI.Shared.Models
{
    public partial class PersonaDocumento
    {
        public int id { get; set; }
        public int id_Persona { get; set; }
        public int id_TipoDocumento { get; set; }
        public string numeroDocumento { get; set; }

        public Persona Persona { get; set; }

        public TipoDocumento TipoDocumento { get; set; }

    }

}
