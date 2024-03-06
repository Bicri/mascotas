using Mascotas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mascotas.Context.Configuration;

public class MascotaEntityTypeConfiguration : IEntityTypeConfiguration<Mascota>
{
    public void Configure(EntityTypeBuilder<Mascota> mascota)
    {
        //Definicion de tabla
        mascota.ToTable("Mascota");
        //Definicion de llaves llaves
        mascota.HasKey(m => m.MascotaId);
        //Definicion de indices
        mascota.HasIndex(m => m.Nombre);

        //Definicion de propiedades
        mascota.Property(m => m.Nombre).IsRequired().HasMaxLength(100);
        mascota.Property(m => m.Edad).IsRequired();
        mascota.Property(m => m.TipoMascota).IsRequired();
    }
}
