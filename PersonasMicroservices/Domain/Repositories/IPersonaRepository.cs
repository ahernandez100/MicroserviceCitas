using PersonasMicroservices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonasMicroservices.Domain.Repositories
{
    public interface IPersonaRepository
    {
        int Add(Persona persona);
        Persona GetById(int codigo);
        void Update(Persona persona);

    }
}
