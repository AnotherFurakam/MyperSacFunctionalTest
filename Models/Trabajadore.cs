using System;
using System.Collections.Generic;

namespace MyperSacFunctionalTest.Models;

public partial class Trabajadore
{
    public int Id { get; set; }

    public string TipoDocumento { get; set; }

    public string NumeroDocumento { get; set; }

    public string Nombres { get; set; }

    public string Sexo { get; set; }

    public int? IdDepartamento { get; set; }

    public int? IdProvincia { get; set; }

    public int? IdDistrito { get; set; }

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();

    public virtual Departamento IdDepartamentoNavigation { get; set; }

    public virtual Distrito IdDistritoNavigation { get; set; }

    public virtual Provincium IdProvinciaNavigation { get; set; }
}
