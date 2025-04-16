using System;
using System.Collections.Generic;

namespace GuarderiaApp.Models
{
    public class Niño
    {
        public int Id { get; set; }
        public string NumeroMatricula { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string? Alergias { get; set; }

        public List<PersonaAutorizada> PersonasAutorizadas { get; set; } = new();

        public List<MenuConsumido> MenusConsumidos { get; set; } = new List<MenuConsumido>();
    }

}
