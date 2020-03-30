using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class InventarioFilialRepository : IInventarioFilial
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var inventarioFilial = ctx.InventarioFiliais.Find(id);

                if (inventarioFilial != null)
                {
                    inventarioFilial.DataExclusao = DateTime.Now;

                    ctx.Entry(inventarioFilial).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }                    
            }
        }

        public List<InventarioFilial> GetByInventarioID(int inventarioID)
        {
            List<InventarioFilial> inventarioFiliais = null;

            using (var ctx = new Context.Context())
            {
                inventarioFiliais = ctx.InventarioFiliais.Include("Filial").Where(x => x.DataExclusao == null && x.InventarioID == inventarioID).ToList();
            }

            return inventarioFiliais;
        }

        public InventarioFilial GetByID(int id)
        {
            InventarioFilial inventarioFilial = null;

            using (var ctx = new Context.Context())
            {
                inventarioFilial = ctx.InventarioFiliais.Where(x => x.ID == id).SingleOrDefault();
            }

            return inventarioFilial;
        }

        public void Insert(InventarioFilial inventarioFilial)
        {
            using (var ctx = new Context.Context())
            {
                var inventarioFilialCadastrado = ctx.InventarioFiliais.Where(x => x.DataExclusao == null && x.InventarioID == inventarioFilial.InventarioID && x.FilialID == inventarioFilial.FilialID).SingleOrDefault();

                if (inventarioFilialCadastrado == null)
                {
                    ctx.InventarioFiliais.Add(inventarioFilial);
                    ctx.SaveChanges();
                }
                else
                    throw new ArgumentException("Filial já vinculada a esse Inventário.");
            }
        }

    }
}
