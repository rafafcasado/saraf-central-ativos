using System.Collections.Generic;
using System.Linq;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;

namespace CentralAtivos.Repository.Repositories
{
    public class OrdemServicoItemRepository : IOrdemServicoItem
    {
        public List<OrdemServicoItem> GetByOrdemServicoID(int ordemServicoID)
        {
            List<OrdemServicoItem> itens = null;

            using (var ctx = new Context.Context())
            {
                itens = ctx.OrdemServicoItens.Include("Item.CamposExtras").Where(x => x.OrdemServicoID == ordemServicoID).ToList();
            }

            return itens;
        }

        public void Insert(OrdemServicoItem ordemServicoItem)
        {
            using (var ctx = new Context.Context())
            {
                ctx.OrdemServicoItens.Add(ordemServicoItem);
                ctx.SaveChanges();
            }
        }
    }
}
