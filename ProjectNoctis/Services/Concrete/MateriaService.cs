using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Concrete
{
    public class MateriaService : IMateriaService
    {
        private readonly IMateriaRepository materiaRepository;

        public MateriaService(IMateriaRepository materiaRepository)
        {
            this.materiaRepository = materiaRepository;
        }
        
        public List<>
    }
}
