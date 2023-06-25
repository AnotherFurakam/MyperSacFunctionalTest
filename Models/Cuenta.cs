using System;
using System.Collections.Generic;

namespace MyperSacFunctionalTest.Models;

public partial class Cuenta
{
    public int Id { get; set; }

    public int IdTrabajador { get; set; }

    public int IdRol { get; set; }

    public string HashPassword { get; set; }

    public string SaltPassword { get; set; }

    public virtual Role IdRolNavigation { get; set; }

    public virtual Trabajadore IdTrabajadorNavigation { get; set; }
}
