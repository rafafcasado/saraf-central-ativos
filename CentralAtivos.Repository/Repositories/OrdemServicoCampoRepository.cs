using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class OrdemServicoCampoRepository : IOrdemServicoCampo
    {
        public OrdemServicoCampo GetByName(string name)
        {
            OrdemServicoCampo item = null;

            using (var ctx = new Context.Context())
            {
                item = ctx.OrdemServicoCampos.Where(x => x.NomeCampo.ToLower() == name.ToLower()).SingleOrDefault();
            }

            return item;
        }
    }
}
