using System;
using System.Collections.Generic;
namespace GuarderiaApp.Models
{
    public class NiñoConPersonasViewModel
    {
        public Niño Niño { get; set; } = new Niño();
        public List<PersonaAutorizada> PersonasAutorizadas { get; set; } = new List<PersonaAutorizada>();
    }
}
