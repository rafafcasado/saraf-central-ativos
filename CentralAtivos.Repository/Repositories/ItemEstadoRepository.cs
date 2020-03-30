using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class ItemEstadoRepository : IItemEstado
    {
        public ItemEstado GetByName(string name)
        {
            ItemEstado itemEstado = null;

            name = name.Trim().ToLower();

            using (var ctx = new Context.Context())
            {
                itemEstado = ctx.ItemEstados.Where(x => x.DataExclusao == null && x.Nome.ToLower() == name).SingleOrDefault();
            }

            return itemEstado;
        }

        public List<ItemEstado> GetAll()
        {
            List<ItemEstado> estados = null;

            using (var ctx = new Context.Context())
            {
                estados = ctx.ItemEstados.Where(x => x.DataExclusao == null).OrderBy(x => x.Nome).ToList();
            }

            return estados;
        }

        public void Insert(ItemEstado itemEstado)
        {
            using (var ctx = new Context.Context())
            {
                ctx.ItemEstados.Add(itemEstado);
                ctx.SaveChanges();
            }
        }
    }
}
