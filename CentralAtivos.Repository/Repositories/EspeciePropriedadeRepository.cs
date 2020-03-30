using System.Collections.Generic;
using System.Linq;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;

namespace CentralAtivos.Repository.Repositories
{
    public class EspeciePropriedadeRepository : IEspeciePropriedade
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var especiePropriedade = ctx.EspeciePropriedades.Find(id);

                if (especiePropriedade != null)
                {
                    ctx.EspeciePropriedades.Remove(especiePropriedade);
                    ctx.SaveChanges();
                }
            }
        }

        public List<EspeciePropriedade> GetByEmpresaID(int empresaID)
        {
            List<EspeciePropriedade> especiePropriedades = null;

            using (var ctx = new Context.Context())
            {
                especiePropriedades = ctx.EspeciePropriedades.Include("Especie.Grupo").Where(x => x.DataExclusao == null && x.Especie.Grupo.EmpresaID == empresaID).ToList();
            }

            return especiePropriedades;
        }

        public List<EspeciePropriedade> GetByEspecieID(int especieID)
        {
            List<EspeciePropriedade> especiePropriedades = null;

            using (var ctx = new Context.Context())
            {
                especiePropriedades = ctx.EspeciePropriedades.Include("Propriedade.Valores").Where(x => x.DataExclusao == null && x.EspecieID == especieID && x.Propriedade.DataExclusao == null).ToList();
            }

            return especiePropriedades;
        }

        public EspeciePropriedade GetByID(int id)
        {
            EspeciePropriedade especiePropriedade = null;

            using (var ctx = new Context.Context())
            {
                especiePropriedade = ctx.EspeciePropriedades.Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            return especiePropriedade;
        }

        public void Insert(EspeciePropriedade especiePropriedade)
        {
            using (var ctx = new Context.Context())
            {
                ctx.EspeciePropriedades.Add(especiePropriedade);
                ctx.SaveChanges();
            }
        }
    }
}
