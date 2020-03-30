using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class EntidadeCampoRepository : IEntidadeCampo
    {
        public List<EntidadeCampo> GetByEntidadeID(int id)
        {
            List<EntidadeCampo> itens = null;

            using (var ctx = new Context.Context())
            {
                itens = ctx.EntidadeCampos.Where(x => x.DataExclusao == null && (int)x.Entidade == id).ToList();
            }

            return itens;
        }
    }
}
