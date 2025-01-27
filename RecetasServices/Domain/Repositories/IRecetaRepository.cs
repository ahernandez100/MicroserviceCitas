using RecetasServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasServices.Domain.Repositories
{
    public interface IRecetaRepository
    {
        int Add(Receta citas);
        Receta GetById(int codigo);
        void Update(Receta cita);
        int Delete(int codigo);
    }
}
