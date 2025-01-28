using CitasMicroService.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitasMicroService.Application.Servicio
{
    public interface IPersonaService
    {
        PersonaDto GetById(int codigo, string token);
    }
}
