using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase.Models;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.Services.Models;
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
        private readonly IStatusRepository statusRepository;

        public MateriaService(IMateriaRepository materiaRepository, IStatusRepository statusRepository)
        {
            this.materiaRepository = materiaRepository;
            this.statusRepository = statusRepository;
        }
        
        public List<RecordMateria> BuildRecordMateriaInfoByName(string name)
        {
            var recordMaterias = materiaRepository.GetRecordMateriasByCharName(name);
            var newRecordMaterias = new List<RecordMateria>();

            foreach(var materia in recordMaterias)
            {
                var newMateria = new RecordMateria();

                newMateria.Info = materia;
                newMateria.Statuses = statusRepository.GetStatusesByEffectText(materia.Name, materia.Effect);
                newMateria.Others = new Dictionary<string, List<SheetOthers>>();

                statusRepository.GetOthersByNamesAndSource(materia.Name, newMateria.Others);

                newRecordMaterias.Add(newMateria);
            }

            return newRecordMaterias;
        }

        public List<LegendMateria> BuildLegendMateriaInfoByName(string name)
        {
            var legendMaterias = materiaRepository.GetLegendMateriasByCharName(name);
            var newLegendMaterias = new List<LegendMateria>();

            foreach (var materia in legendMaterias)
            {
                var newMateria = new LegendMateria();

                newMateria.Info = materia;
                newMateria.Statuses = statusRepository.GetStatusesByEffectText(materia.Name, materia.Effect);
                newMateria.Others = new Dictionary<string, List<SheetOthers>>();

                statusRepository.GetOthersByNamesAndSource(materia.Name, newMateria.Others);

                newLegendMaterias.Add(newMateria);
            }

            return newLegendMaterias;
        }
    }
}
