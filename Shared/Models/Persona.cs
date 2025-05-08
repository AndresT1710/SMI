#nullable disable
using System;
using System.Collections.Generic;

namespace SMI.Shared.Models;

public partial class Persona
{
    public int id { get; set; }

    public int? id_Genero { get; set; }

    public string nombre { get; set; }

    public string apellido { get; set; }
    public DateTime FechaNacimiento { get; set; }

    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public ICollection<PersonaDocumento> PersonaDocumentos { get; set; } = new List<PersonaDocumento>();

}