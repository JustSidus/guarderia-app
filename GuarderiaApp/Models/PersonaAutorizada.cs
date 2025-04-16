using GuarderiaApp.Models;

public class PersonaAutorizada
{
    public int Id { get; set; }
    public string Cedula { get; set; }
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Relacion { get; set; }
    public string CuentaBancaria { get; set; }
    public bool EsResponsablePago { get; set; }

    public int NiñoId { get; set; }
    public Niño Niño { get; set; }  // Importante: no eliminar esto
}
