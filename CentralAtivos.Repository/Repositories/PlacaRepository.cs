using System;
using System.Collections.Generic;
using System.Linq;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;

namespace CentralAtivos.Repository.Repositories
{
    public class PlacaRepository : IPlaca
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var placa = ctx.Placas.Find(id);

                if (placa != null)
                {
                    placa.DataExclusao = DateTime.Now;
                }

                ctx.Entry(placa).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public Placa GetByID(int id)
        {
            Placa placa = null;

            using (var ctx = new Context.Context())
            {
                placa = ctx.Placas.Find(id);
            }

            return placa;
        }

        public List<Placa> GetByPlacaGrupoID(int placaGrupoID)
        {
            List<Placa> placas = null;

            using (var ctx = new Context.Context())
            {
                placas = ctx.Placas.Where(x => x.PlacaGrupoID == placaGrupoID && x.DataExclusao == null).OrderBy(x => x.NumeroPlaca).ToList();
            }

            return placas;
        }

        public List<Placa> GetJumpsByPlacaGrupoID(int placaGrupoID)
        {
            List<Placa> placas = null;

            using (var ctx = new Context.Context())
            {
                placas = ctx.Placas.Where(x => x.PlacaGrupoID == placaGrupoID && x.ItemID == null && x.DataExclusao == null).OrderBy(x => x.NumeroPlaca).ToList();
            }

            return placas;
        }

        public void Insert(Placa placa)
        {
            using(var ctx = new Context.Context())
            {
                ctx.Placas.Add(placa);
                ctx.SaveChanges();
            }
        }

        public void Update(Placa placa)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(placa).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
