using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyperSacFunctionalTest.Dto.Departamento;
using MyperSacFunctionalTest.Dto.Provincia;
using MyperSacFunctionalTest.Exceptions;
using MyperSacFunctionalTest.Models;
using System.Net;

namespace MyperSacFunctionalTest.Services.Departamento
{
    public class DepartamentoServiceImpl : IDepartamentoService
    {
        private readonly IMapper _mapper;
        private readonly TrabajadoresPruebaContext _context;

        public DepartamentoServiceImpl(IMapper mapper, TrabajadoresPruebaContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ApiResponse<List<DepartamentoDto>>> GetAll()
        {
            var response = new ApiResponse<List<DepartamentoDto>>();
            try
            {
                var departamentos = await _context.Departamentos.ToListAsync();

                response.Data = departamentos.Select(d => _mapper.Map<DepartamentoDto>(d)).ToList();
                response.Status = HttpStatusCode.OK;

            }
            catch (ApiException err)
            {
                response.Success = false;
                response.Status = err.StatusCode;
                response.Message = err.Message;
            }
            catch (Exception err)
            {
                response.Success = false;
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = err.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<ProvinciaDto>>> GetProviciasByIdDepartamento(int id_departamento)
        {
            var response = new ApiResponse<List<ProvinciaDto>>();
            try
            {
                var departamento = await _context.Departamentos.Include(d => d.Provincia).FirstOrDefaultAsync(d => d.Id == id_departamento)
                    ?? throw new ApiException(HttpStatusCode.NotFound, "Departamento no encontrado");

                response.Data = departamento.Provincia.Select(p => _mapper.Map<ProvinciaDto>(p)).ToList();
                response.Status = HttpStatusCode.OK;
            }
            catch (ApiException err)
            {
                response.Success = false;
                response.Status = err.StatusCode;
                response.Message = err.Message;
            }
            catch (Exception err)
            {
                response.Success = false;
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = err.Message;
            }
            return response;
        }
    }
}
