using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyperSacFunctionalTest.Dto.Trabajador;
using MyperSacFunctionalTest.Enums;
using MyperSacFunctionalTest.Exceptions;
using MyperSacFunctionalTest.Models;
using System.Net;
using System.Runtime.InteropServices;

namespace MyperSacFunctionalTest.Services.Trabajador
{
    public class TrabajadorServiceImpl : ITrabajadorService
    {
        private readonly IMapper _mapper;
        private readonly TrabajadoresPruebaContext _context;

        public TrabajadorServiceImpl(IMapper mapper, TrabajadoresPruebaContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        private async Task ValidarDatosDelTrabajador(CreateTrabajadorDto trabajadorDto)
        {
            //Buscando departamento mediante su id en la base de datos
            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(d => d.Id == trabajadorDto.IdDepartamento)
                ?? throw new ApiException(HttpStatusCode.NotFound, "Departamento no enconrtado.");

            //Verificando si la privincia ingresada pertenece al departamento
            var provincia = await _context.Provincia.Where(p => p.IdDepartamento == trabajadorDto.IdDepartamento && p.Id == trabajadorDto.IdProvincia).FirstOrDefaultAsync() ?? throw new ApiException(HttpStatusCode.NotFound, "La provincia no pertenece al departamento ingresado");

            //Verificando si el distrito pertenece a la provincia
            var distrito = await _context.Distritos.Where(d => d.IdProvincia == trabajadorDto.IdProvincia && d.Id == trabajadorDto.IdDistrito).FirstOrDefaultAsync() ?? throw new ApiException(HttpStatusCode.NotFound, "El distrito no pertenece a la provincia ingresada");

            //Validando que el tipo de documento tenga el valor  DNI
            if (!TipoDocumento.DNI.ToString().Equals(trabajadorDto.TipoDocumento.ToUpper()))
                throw new ApiException(HttpStatusCode.BadRequest, "El tipo de documento es inválido solo se acepta DNI");

            //Validando que le formato de sexo sea correcto (valores permitidos M o F)
            if (!Sexo.MASCULINO.ToString().Equals(trabajadorDto.Sexo.ToUpper()) && !Sexo.FEMENINO.Equals(trabajadorDto.Sexo.ToUpper()))
                throw new ApiException(HttpStatusCode.BadRequest, "El formato de sexo, es inválido solo se acepta M o F ");

        }

        private async Task<TrabajadorDto> ObtenerTrabajadorConProcedure(int id_trabajador)
        {
            var responseDbProcedure = await _context.SpTrabajadorResponses.FromSqlInterpolated($"EXEC spObtenerTrabajador @Id = {id_trabajador}").ToListAsync();

            var trabajador = responseDbProcedure.FirstOrDefault()
                ?? throw new ApiException(HttpStatusCode.BadRequest, "Error al obtener los datos del usuario.");
            return _mapper.Map<TrabajadorDto>(trabajador);
        }

        /*Métodos del servicio*/

        public async Task<ApiResponse<GetTrabajadorDto>> GetById(int id_trabjador)
        {
            var response = new ApiResponse<GetTrabajadorDto>();
            try
            {
                var trabajador = await _context.Trabajadores.FirstOrDefaultAsync(t => t.Id == id_trabjador)
                    ?? throw new ApiException(HttpStatusCode.NotFound, "Trabajador no encontrado");

                response.Data = _mapper.Map<GetTrabajadorDto>(trabajador);
                response.Status = HttpStatusCode.OK;
            }
            catch (ApiException err)
            {
                response.Status = err.StatusCode;
                response.Success = false;
                response.Message = err.Message;
            }
            catch (Exception err)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = err.Message;
            }
            return response;
        }


        public async Task<ApiResponse<List<TrabajadorDto>>> GetAll()
        {
            var response = new ApiResponse<List<TrabajadorDto>>();
            try
            {
                var trabajadores = await _context.SpTrabajadorResponses.FromSqlInterpolated($"EXEC spListarTrabajadores").ToListAsync();
                response.Data = trabajadores.Select(t => _mapper.Map<TrabajadorDto>(t)).ToList();
                response.Status = HttpStatusCode.OK;
            }
            catch (ApiException err)
            {
                response.Status = err.StatusCode;
                response.Success = false;
                response.Message = err.Message;
            }
            catch (Exception err)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = err.Message;
            }
            return response;
        }

        public async Task<ApiResponse<TrabajadorDto>> AddTrabajador(CreateTrabajadorDto trabajadorDto)
        {
            var response = new ApiResponse<TrabajadorDto>();
            try
            {
                //Validando si el número de documento ya existe
                var dniExistente = await _context.Trabajadores.Where(t => t.NumeroDocumento == trabajadorDto.NumeroDocumento).FirstOrDefaultAsync();
                if (dniExistente != null) throw new ApiException(HttpStatusCode.BadRequest, "El número de documento ya existe");

                await ValidarDatosDelTrabajador(trabajadorDto);

                var trabajador = _mapper.Map<Trabajadore>(trabajadorDto);
                trabajador.Sexo = trabajador.Sexo.ToUpper();
                trabajador.TipoDocumento = trabajador.TipoDocumento.ToUpper();

                await _context.Trabajadores.AddAsync(trabajador);
                await _context.SaveChangesAsync();

                response.Data = await ObtenerTrabajadorConProcedure(trabajador.Id);
                response.Status = HttpStatusCode.Created;
                response.Message = "Trabajador registrado con éxito";

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


        public async Task<ApiResponse<TrabajadorDto>> UpdateTrabajador(UpdateTrabajadorDto trabajadorDto, int id_trabajador)
        {
            var response = new ApiResponse<TrabajadorDto>();
            try
            {
                var documentExist = await _context.Trabajadores.FirstOrDefaultAsync(t => t.Id != id_trabajador && t.NumeroDocumento == trabajadorDto.NumeroDocumento);
                if (documentExist != null) throw new ApiException(HttpStatusCode.BadRequest, "El número de documento ingresado ya existe.");

                await ValidarDatosDelTrabajador(trabajadorDto);

                var trabajador = await _context.Trabajadores.FirstOrDefaultAsync(t => t.Id == id_trabajador)
                    ?? throw new ApiException(HttpStatusCode.NotFound, "El trabajador no fue encontrado");

                _mapper.Map(trabajadorDto, trabajador);
                trabajador.Sexo = trabajador.Sexo.ToUpper();
                trabajador.TipoDocumento = trabajador.TipoDocumento.ToUpper();

                await _context.SaveChangesAsync();

                response.Data = await ObtenerTrabajadorConProcedure(id_trabajador);
                response.Status = HttpStatusCode.OK;
                response.Message = "Datos del trabajador actualizados con éxito";

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


        public async Task<ApiResponse<TrabajadorDto>> DeleteTrabajador(int id_trabajador)
        {
            var response = new ApiResponse<TrabajadorDto>();
            try
            {
                var trabajador = await _context.Trabajadores.FirstOrDefaultAsync(t => t.Id == id_trabajador)
                    ?? throw new ApiException(HttpStatusCode.NotFound, "El trabajador no fue encontrado");

                //Obteniendo los datos de respuesta antes de que se elimine
                response.Data = await ObtenerTrabajadorConProcedure(id_trabajador);

                _context.Trabajadores.Remove(trabajador);
                await _context.SaveChangesAsync();

                response.Status = HttpStatusCode.OK;
                response.Message = "Trabajador eliminado con éxito";

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

        public async Task<ApiResponse<List<TrabajadorDto>>> GetAllBySexo(string sexo)
        {
            var response = new ApiResponse<List<TrabajadorDto>>();
            try
            {

                //Validando que le formato de sexo sea correcto (valores permitidos M o F)
                sexo = sexo.ToUpper();
                if (!Sexo.MASCULINO.ToString().Equals(sexo) && !Sexo.FEMENINO.Equals(sexo))
                    throw new ApiException(HttpStatusCode.BadRequest, "El formato de sexo es inválido, solo se acepta M o F ");

                var trabajadores = await _context.SpTrabajadorResponses.FromSqlInterpolated($"EXEC spListarTrabajadoresPorSexo {sexo}").ToListAsync();

                response.Data = trabajadores.Select(t => _mapper.Map<TrabajadorDto>(t)).ToList();
                response.Status = HttpStatusCode.OK;
                response.Message = "Listado exitosamente";
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
