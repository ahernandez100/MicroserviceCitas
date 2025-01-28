using RecetasServices.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasServices.Application.Services
{
    public interface ICitaService
    {
        CitaDto GetById(int codigo, string token);

    }
}
