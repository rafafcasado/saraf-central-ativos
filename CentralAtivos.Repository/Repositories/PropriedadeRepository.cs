using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class PropriedadeRepository : IPropriedade
    {
        public List<Propriedade> GetByEmpresaID(int empresaID)
        {
            List<Propriedade> propriedades = null;

            using (var ctx = new Context.Context())
            {
                propriedades = ctx.Propriedades.Include("Valores").Where(x => x.DataExclusao == null && x.EmpresaID == empresaID).ToList();

                foreach (var prop in propriedades)
                {
                    prop.Valores = prop.Valores.Where(x => x.DataExclusao == null).ToList();
                }
            }

            return propriedades;
        }

        public void Insert(Propriedade propriedade)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Propriedades.Add(propriedade);
                ctx.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var resp = ctx.Propriedades.Find(id);

                if (resp != null)
                {
                    resp.DataExclusao = DateTime.Now;

                    ctx.Entry(resp).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
        }

        public Propriedade GetByID(int id)
        {
            Propriedade prop = null;

            using (var ctx = new Context.Context())
            {
                prop = ctx.Propriedades.Include("Valores").Where(x => x.ID == id).SingleOrDefault();

                prop.Valores = prop.Valores.Where(x => x.DataExclusao == null).ToList();
            }

            return prop;
        }

        public void Update(Propriedade prop)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(prop).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void DeleteAll(int empresaID)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Database.ExecuteSqlCommand($"UPDATE PROPRIEDADEVALOR SET DATAEXCLUSAO = NULL WHERE PROPRIEDADEID IN (SELECT ID FROM PROPRIEDADE WHERE EMPRESAID IN ({empresaID}); UPDATE PROPRIEDADE SET DATAEXCLUSAO = GETDATE() WHERE DATAEXCLUSAO IS NULL AND EMPRESAID = {empresaID}");
                ctx.SaveChanges();
            }
        }
    }
}
