using MyperSacFunctionalTest.Dto.Departamento;
using MyperSacFunctionalTest.Dto.Distrito;
using MyperSacFunctionalTest.Models;

namespace MyperSacFunctionalTest.Services.Provincia
{
    public interface IProvinciaService
    {
        public Task<ApiResponse<List<DistritoDto>>> GetDistritosByProvinciaId(int id_provincia);
    }
}
