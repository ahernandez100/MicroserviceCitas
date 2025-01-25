using CitasMicroService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitasMicroService.Domain.Repositories
{
    public interface ICitaRepository
    {
        int Add(Cita citas);
        Cita GetById(int codigo);
        List<Cita> GetAll();

        void Update(Cita cita);
        int Delete(int codigo);



    }
}
