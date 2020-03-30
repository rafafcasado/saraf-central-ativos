using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class OrdemServicoMotivoCampoRepository : IOrdemServicoMotivoCampo
    {
        public OrdemServicoMotivoCampo GetByMotivoCampo(int motivoID, int campoID)
        {
            OrdemServicoMotivoCampo item = null;

            using (var ctx = new Context.Context())
            {
                item = ctx.OrdemServicoMotivoCampos.Where(x => x.OrdemServicoMotivoID == motivoID && x.OrdemServicoCampoID == campoID).SingleOrDefault();
            }

            return item;
        }

        public List<OrdemServicoMotivoCampo> GetByMotivoID(int motivoID)
        {
            List<OrdemServicoMotivoCampo> campos = null;

            using (var ctx = new Context.Context())
            {
                campos = ctx.OrdemServicoMotivoCampos.Include("OrdemServicoCampo").Where(x => x.OrdemServicoMotivoID == motivoID).ToList();
            }

            return campos;
        }
    }
}
