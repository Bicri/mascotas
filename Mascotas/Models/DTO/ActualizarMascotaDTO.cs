using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mascotas.Models.DTO;

public class ActualizarMascotaDTO : CrearMascotaDTO
{
    [Remote(action: "VerificarExisteMascotaEdicion", controller: "Mascotas", AdditionalFields = "MascotaId")]
    public override string Nombre { get; set; } = string.Empty;
    public int MascotaId { get; set; }
}
