using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;

namespace CentralAtivos.Repository.Repositories
{
    public class OrdemServicoAnexoRepository : IOrdemServicoAnexo
    {
        public void Insert(OrdemServicoAnexo anexo)
        {
            using (var ctx = new Context.Context())
            {
                ctx.OrdemServicoAnexos.Add(anexo);
                ctx.SaveChanges();
            }
        }
    }
}
