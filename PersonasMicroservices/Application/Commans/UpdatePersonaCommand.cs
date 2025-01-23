using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonasMicroservices.Application.Commans
{
    public class UpdatePersonaCommand
    {
        public int codigo { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string documento { get; set; }
    }
}