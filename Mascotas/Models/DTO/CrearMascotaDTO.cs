using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Mascotas.Models.DTO;

public class CrearMascotaDTO
{
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [StringLength(maximumLength:100, MinimumLength = 3, ErrorMessage = "La longitud del campo {0} debe estar entre {2} y {1}")]
    [Display(Name = "Nombre de la mascota")]
    [Remote(action: "VerificarExisteMascota", controller:"Mascotas")]
    public virtual string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Range(minimum: 1, maximum: 50, ErrorMessage = "El valor debe estar entre {1} y {2} años")]
    [Display(Name = "Edad de la mascota en años")]
    public int Edad { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido")]
    public TipoMascota TipoMascotaEscogido { get; set; }
    [Display(Name = "Tipo de mascota")]
    public IEnumerable<SelectListItem>? ListadoMascotas { get; set; }
}
