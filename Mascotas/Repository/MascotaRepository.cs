using Mascotas.Context;
using Mascotas.Models;
using Mascotas.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Mascotas.Repository;

public class MascotaRepository : IMascotaRepository
{
    private readonly ApplicationDbContext db;

    public MascotaRepository(ApplicationDbContext db)
    {
        this.db = db;
    }

    /// <summary>
    /// Obtiene las primeras 3 mascotas
    /// </summary>
    /// <returns></returns>
    public async Task<List<Mascota>> ObtenerPrimerasMascotas()
    {
        return await db.Mascotas.AsNoTracking().OrderByDescending(m => m.MascotaId).Take(3).ToListAsync();
    }
    /// <summary>
    /// Obtiene la mascota por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Mascota> ObtenerPorId(int id)
    {
        return await db.Mascotas.Where(m => m.MascotaId == id).FirstAsync();
    }
    /// <summary>
    /// Método que busca por coincidencias en los nombres de las mascotas
    /// </summary>
    /// <param name="nombre"></param>
    /// <returns></returns>
    public async Task<List<MascotaDTO>> BusquedaCoincidencias(string nombre)
    {
        //Si está vacío devuelve igual que el método index
        if(string.IsNullOrEmpty(nombre))
        {
            List<Mascota> mascotas = await ObtenerPrimerasMascotas();
            return mascotas.Select(m => new MascotaDTO
            {
                MascotaId = m.MascotaId,
                Nombre = m.Nombre,
                Edad = m.Edad,
                TipoMascota = m.TipoMascota,
                TipoMascotaStr = m.TipoMascota.ToString()

            }).ToList();
        }
        //Caso contrario, devuelve las coincidencias
        return await db.Mascotas.Where(m => m.Nombre.Contains(nombre)).Select(m => new MascotaDTO
        {
            MascotaId = m.MascotaId,
            Nombre = m.Nombre,
            Edad = m.Edad,
            TipoMascota = m.TipoMascota,
            TipoMascotaStr = m.TipoMascota.ToString()

        }).ToListAsync();
    }
    /// <summary>
    /// Método que crea una nueva mascota
    /// </summary>
    /// <param name="nuevaMascota"></param>
    /// <returns></returns>
    public async Task CrearMascota(CrearMascotaDTO nuevaMascota)
    {
        Mascota mascota = new Mascota()
        {
            Nombre = nuevaMascota.Nombre,
            Edad = nuevaMascota.Edad,
            TipoMascota = nuevaMascota.TipoMascotaEscogido
        };

        await db.AddAsync(mascota);
        await db.SaveChangesAsync();
    }

    /// <summary>
    /// Valida si ya existe un nombre de mascota registrado
    /// </summary>
    /// <param name="nombre"></param>
    /// <returns></returns>
    public async Task<bool> ValidarNombreMascota(string nombre)
    {
        return await db.Mascotas.AnyAsync(m => m.Nombre == nombre);
    }

    /// <summary>
    /// Valida que no exista la mascota al actualizar
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> ValidarNombreMascotaEdicion(string nombre, int id)
    {
        return await db.Mascotas.AnyAsync(m => m.Nombre == nombre && m.MascotaId != id);
    }

    /// <summary>
    /// Método para actualizar los datos de la mascota
    /// </summary>
    /// <param name="actualizarMascota"></param>
    /// <returns></returns>
    public async Task Actualizar(ActualizarMascotaDTO actualizarMascota)
    {
        Mascota mascota = await ObtenerPorId(actualizarMascota.MascotaId);

        mascota.Nombre = actualizarMascota.Nombre;
        mascota.Edad = actualizarMascota.Edad;
        mascota.TipoMascota = actualizarMascota.TipoMascotaEscogido;

        await db.SaveChangesAsync();
    }
    /// <summary>
    /// Elimina una mascota por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task Borrar(int id)
    {
        Mascota mascota = await ObtenerPorId(id);

        db.Mascotas.Remove(mascota);
        await db.SaveChangesAsync();
    }
}
