namespace Mascotas.Models;

public class Mascota
{
    public int MascotaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public TipoMascota TipoMascota { get; set; }
}
