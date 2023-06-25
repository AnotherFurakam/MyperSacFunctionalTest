using AutoMapper;
using MyperSacFunctionalTest.Dto.Departamento;
using MyperSacFunctionalTest.Dto.Distrito;
using MyperSacFunctionalTest.Dto.Provincia;
using MyperSacFunctionalTest.Dto.Trabajador;
using MyperSacFunctionalTest.Models;
using MyperSacFunctionalTest.Models.NokeyModels;

namespace MyperSacFunctionalTest.Utils
{
    public class AutoMapperProfile: Profile
    {

        public AutoMapperProfile()
        {
            //Trabajadores
            CreateMap<CreateTrabajadorDto, Trabajadore>();
            CreateMap<Trabajadore, TrabajadorDto>();
            CreateMap<SpTrabajadorResponse, TrabajadorDto>();

            //Provincia
            CreateMap<Provincium, ProvinciaDto>();

            //Departamento
            CreateMap<Departamento, DepartamentoDto>();

            //Distrito
            CreateMap<Distrito, DistritoDto>();


        }
    }
}
