using RecetasServices.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasServices.Application.Services
{
    public interface IPersonaService
    {
        PersonaDto GetById(int codigo, string token);
    }
}
