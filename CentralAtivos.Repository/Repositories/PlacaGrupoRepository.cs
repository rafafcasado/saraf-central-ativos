using System;
using System.Collections.Generic;
using System.Linq;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;

namespace CentralAtivos.Repository.Repositories
{
    public class PlacaGrupoRepository : IPlacaGrupo
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var placaGrupo = ctx.PlacasGrupo.Find(id);

                if (placaGrupo != null)
                {
                    placaGrupo.DataExclusao = DateTime.Now;
                }

                ctx.Entry(placaGrupo).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public PlacaGrupo GetByID(int ID)
        {
            PlacaGrupo placaGrupo = null;

            using (var ctx = new Context.Context())
            {
                placaGrupo = ctx.PlacasGrupo.Find(ID);
            }

            return placaGrupo;
        }

        public List<PlacaGrupo> GetByInventarioID(int inventarioID)
        {
            List<PlacaGrupo> placasGrupo = null;

            using (var ctx = new Context.Context())
            {
                placasGrupo = ctx.PlacasGrupo.Include("Usuario.Empresa").Include("Usuario.Perfil").Where(x => x.InventarioID == inventarioID && x.DataExclusao == null).OrderBy(x => x.Inicio).ToList();
            }

            return placasGrupo;
        }

        public void Insert(PlacaGrupo placaGrupo)
        {
            using(var ctx = new Context.Context())
            {
                ctx.PlacasGrupo.Add(placaGrupo);
                ctx.SaveChanges();
            }
        }
    }
}
