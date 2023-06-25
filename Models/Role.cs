using System;
using System.Collections.Generic;

namespace MyperSacFunctionalTest.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
}
