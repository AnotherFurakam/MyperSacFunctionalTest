﻿using MyperSacFunctionalTest.Dto.Trabajador;
using MyperSacFunctionalTest.Models;

namespace MyperSacFunctionalTest.Services.Trabajador
{
    public interface ITrabajadorService
    {
        public Task<ApiResponse<GetTrabajadorDto>> GetById(int id_trabjador);
        public Task<ApiResponse<List<TrabajadorDto>>> GetAll();
        public Task<ApiResponse<List<TrabajadorDto>>> GetAllBySexo(string sexo);
        public Task<ApiResponse<TrabajadorDto>> AddTrabajador(CreateTrabajadorDto trabajadorDto);
        public Task<ApiResponse<TrabajadorDto>> UpdateTrabajador(UpdateTrabajadorDto trabajadorDto, int id_trabajador);
        public Task<ApiResponse<TrabajadorDto>> DeleteTrabajador(int id_trabajador);
    }
}
