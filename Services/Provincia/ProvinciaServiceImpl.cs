using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyperSacFunctionalTest.Dto.Distrito;
using MyperSacFunctionalTest.Exceptions;
using MyperSacFunctionalTest.Models;
using System.Net;

namespace MyperSacFunctionalTest.Services.Provincia
{
    public class ProvinciaServiceImpl : IProvinciaService
    {
        private readonly IMapper _mapper;
        private readonly TrabajadoresPruebaContext _context;

        public ProvinciaServiceImpl(TrabajadoresPruebaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<DistritoDto>>> GetDistritosByProvinciaId(int id_provincia)
        {
            var response = new ApiResponse<List<DistritoDto>>();
            try
            {
                var provincia = await _context.Provincia.Include(p => p.Distritos).FirstOrDefaultAsync(p => p.Id == id_provincia)
                    ?? throw new ApiException(HttpStatusCode.NotFound, "Provincia no encontrada");

                response.Data = provincia.Distritos.Select(p => _mapper.Map<DistritoDto>(p)).ToList();
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
