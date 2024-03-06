using Mascotas.Models;
using Mascotas.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mascotas.Repository;

public interface IMascotaRepository
{
    /// <summary>
    /// Obtiene las primeras 3 mascotas
    /// </summary>
    /// <returns></returns>
    public Task<List<Mascota>> ObtenerPrimerasMascotas();
    /// <summary>
    /// Obtiene la mascota por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<Mascota> ObtenerPorId(int id);
    /// <summary>
    /// Método que busca por coincidencias en los nombres de las mascotas
    /// </summary>
    /// <param name="nombre"></param>
    /// <returns></returns>
    public Task<List<MascotaDTO>> BusquedaCoincidencias(string nombre);
    /// <summary>
    /// Método que inicializa el formulario de creación de mascotas
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SelectListItem> CrearMascota();
    /// <summary>
    /// Método que crea una nueva mascota
    /// </summary>
    /// <param name="nuevaMascota"></param>
    /// <returns></returns>
    public Task CrearMascota(CrearMascotaDTO nuevaMascota);
    /// <summary>
    /// Valida si ya existe un nombre de mascota registrado
    /// </summary>
    /// <param name="nombre"></param>
    /// <returns></returns>
    public Task<bool> ValidarNombreMascota(string nombre);
    /// <summary>
    /// Valida que no exista la mascota al actualizar
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> ValidarNombreMascotaEdicion(string nombre, int id);
    /// <summary>
    /// Método para actualizar los datos de la mascota
    /// </summary>
    /// <param name="actualizarMascota"></param>
    /// <returns></returns>
    public Task Actualizar(ActualizarMascotaDTO actualizarMascota);
    /// <summary>
    /// Elimina una mascota por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task Borrar(int id);
}
