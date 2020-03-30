using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class OrdemServicoMotivoCampoValorRepository : IOrdemServicoMotivoCampoValor
    {
        public OrdemServicoMotivoCampoValor Get(int ordemServicoID, int ordemServicoMotivoCampoID)
        {
            OrdemServicoMotivoCampoValor ordemServicoMotivoCampoValor = null;

            using (var ctx = new Context.Context())
            {
                ordemServicoMotivoCampoValor = ctx.OrdemServicoMotivoCampoValores.Where(x => x.OrdemServicoID == ordemServicoID && x.OrdemServicoMotivoCampoID == ordemServicoMotivoCampoID).SingleOrDefault();
            }

            return ordemServicoMotivoCampoValor;
        }

        public void Insert(OrdemServicoMotivoCampoValor valor)
        {
            using (var ctx = new Context.Context())
            {
                ctx.OrdemServicoMotivoCampoValores.Add(valor);
                ctx.SaveChanges();
            }
        }
    }
}
