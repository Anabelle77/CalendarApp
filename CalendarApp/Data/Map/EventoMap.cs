using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CalendarApp.Models;

namespace CalendarApp.Data.Map
{
    public class EventoMap : IEntityTypeConfiguration<EventoModel>
    {
        public void Configure(EntityTypeBuilder<EventoModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(255);
                
            builder.Property(x => x.Descricao)
                .HasMaxLength(1000);

            builder.HasOne(x => x.Usuario);

            builder.HasMany(x => x.Participantes);

        }
    }
}
