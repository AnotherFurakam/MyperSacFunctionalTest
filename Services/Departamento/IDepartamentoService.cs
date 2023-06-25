using MyperSacFunctionalTest.Dto.Departamento;
using MyperSacFunctionalTest.Dto.Provincia;
using MyperSacFunctionalTest.Models;

namespace MyperSacFunctionalTest.Services.Departamento
{
    public interface IDepartamentoService
    {
        public Task<ApiResponse<List<DepartamentoDto>>> GetAll();
        public Task<ApiResponse<List<ProvinciaDto>>> GetProviciasByIdDepartamento(int id_departamento); 
    }
}
