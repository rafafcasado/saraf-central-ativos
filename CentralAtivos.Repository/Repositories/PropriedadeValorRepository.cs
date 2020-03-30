using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class PropriedadeValorRepository : IPropriedadeValor
    {
        public void Insert(PropriedadeValor propriedadeValor)
        {
            using (var ctx = new Context.Context())
            {
                ctx.PropriedadeValores.Add(propriedadeValor);
                ctx.SaveChanges();
            }
        }

        public void Update(PropriedadeValor propriedadeValor)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(propriedadeValor).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var resp = ctx.PropriedadeValores.Find(id);

                if (resp != null)
                {
                    resp.DataExclusao = DateTime.Now;

                    ctx.Entry(resp).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
        }

        public PropriedadeValor GetByID(int id)
        {
            PropriedadeValor val = null;

            using (var ctx = new Context.Context())
            {
                val = ctx.PropriedadeValores.Where(x => x.ID == id).SingleOrDefault();
            }

            return val;
        }
    }
}
