using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class ColetorRepository : IColetor
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var coletor = ctx.Coletores.Find(id);

                if (coletor != null)
                {
                    coletor.DataExclusao = DateTime.Now;

                    ctx.Entry(coletor).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
        }

        public List<Coletor> GetAll()
        {
            List<Coletor> coletores = null;

            using (var ctx = new Context.Context())
            {
                coletores = ctx.Coletores.Where(x => x.DataExclusao == null).ToList();
            }

            return coletores;
        }

        public Coletor GetByID(int id)
        {
            Coletor coletor = null;

            using (var ctx = new Context.Context())
            {
                coletor = ctx.Coletores.Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            return coletor;
        }

        public void Insert(Coletor coletor)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Coletores.Add(coletor);

                ctx.SaveChanges();
            }
        }

        public void Update(Coletor coletor)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(coletor).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
