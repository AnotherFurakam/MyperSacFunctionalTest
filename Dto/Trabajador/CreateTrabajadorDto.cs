﻿namespace MyperSacFunctionalTest.Dto.Trabajador
{
    public class CreateTrabajadorDto
    {
        public string TipoDocumento { get; set; }

        public string NumeroDocumento { get; set; }

        public string Nombres { get; set; }

        public string Sexo { get; set; }

        public int? IdDepartamento { get; set; }

        public int? IdProvincia { get; set; }

        public int? IdDistrito { get; set; }

    }
}
