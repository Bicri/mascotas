using Mascotas.Models;
using Mascotas.Models.DTO;
using Mascotas.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace Mascotas.Controllers
{
    public class MascotasController : Controller
    {
        private readonly ILogger<MascotasController> _logger;
        private readonly IMascotaRepository mascotaRepository;

        public MascotasController(ILogger<MascotasController> logger, IMascotaRepository mascotaRepository)
        {
            _logger = logger;
            this.mascotaRepository = mascotaRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<Mascota> mascotas = await mascotaRepository.ObtenerPrimerasMascotas();
            return View(mascotas);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            CrearMascotaDTO mascota = new();
            mascota.ListadoMascotas = mascotaRepository.CrearMascota();
            return View(mascota);
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteMascota(string nombre)
        {
            bool existe = await mascotaRepository.ValidarNombreMascota(nombre);

            if(existe)
            {
                return Json($"El nombre de mascota {nombre} ya existe");
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteMascotaEdicion(string nombre, int mascotaId)
        {
            bool existe = await mascotaRepository.ValidarNombreMascotaEdicion(nombre, mascotaId);

            if (existe)
            {
                return Json($"El nombre de mascota {nombre} ya existe");
            }

            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearMascotaDTO nuevaMascota)
        {
            if( ! ModelState.IsValid) {
                return View(nuevaMascota);
            }

            await mascotaRepository.CrearMascota(nuevaMascota);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> BuscarCoincidencias(string nombre)
        {
            List<MascotaDTO> mascotas = await mascotaRepository.BusquedaCoincidencias(nombre);
            return Json(mascotas);
        }

        public async Task<IActionResult> Editar(int id)
        {
            Mascota mascota = await mascotaRepository.ObtenerPorId(id);

            ActualizarMascotaDTO mascotaActualizar = new();
            mascotaActualizar.ListadoMascotas = mascotaRepository.CrearMascota();

            mascotaActualizar.MascotaId = mascota.MascotaId;
            mascotaActualizar.Nombre = mascota.Nombre;
            mascotaActualizar.Edad = mascota.Edad;
            mascotaActualizar.TipoMascotaEscogido = mascota.TipoMascota;

            return View(mascotaActualizar);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ActualizarMascotaDTO mascotaActualizar)
        {
            await mascotaRepository.Actualizar(mascotaActualizar);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Borrar(int id)
        {
            Mascota mascota = await mascotaRepository.ObtenerPorId(id);
            return View(mascota);
        }

        public async Task<IActionResult> BorrarMascota(int mascotaId)
        {
            await mascotaRepository.Borrar(mascotaId);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
