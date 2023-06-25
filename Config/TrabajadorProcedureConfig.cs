using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyperSacFunctionalTest.Models.NokeyModels;

namespace MyperSacFunctionalTest.Config
{
    public class TrabajadorProcedureConfig : IEntityTypeConfiguration<SpTrabajadorResponse>
    {
        public void Configure(EntityTypeBuilder<SpTrabajadorResponse> builder)
        {
            builder.HasNoKey();
        }
    }
}
