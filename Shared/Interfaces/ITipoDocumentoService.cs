using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMI.Shared.DTOs;
using SMI.Shared.Models;

namespace SMI.Shared.Interfaces
{
    public interface ITipoDocumentoService
    {
        Task<List<TipoDocumento>> GetTiposDocumentoAsync();
    }

}