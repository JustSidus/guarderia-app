using System;
using System.Collections.Generic;

namespace GuarderiaApp.Models
{
    public class Niño
    {
        public int Id { get; set; }
        public string NumeroMatricula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Alergias { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; } = new DateTime();
        public DateTime FechaIngreso { get; set; } = new DateTime();
        public DateTime? FechaBaja { get; set; } = null;

        public List<PersonaAutorizada> PersonasAutorizadas { get; set; } = new List<PersonaAutorizada>();
        public List<MenuConsumido> MenusConsumidos { get; set; } = new List<MenuConsumido>();
    }
}
