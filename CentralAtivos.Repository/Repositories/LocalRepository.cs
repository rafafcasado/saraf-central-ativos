using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class LocalRepository : ILocal
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var local = ctx.Locais.Find(id);

                if (local != null)
                {
                    local.DataExclusao = DateTime.Now;

                    ctx.Entry(local).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }                    
            }
        }

        public List<Local> GetByEmpresaID(int empresaID)
        {
            List<Local> locais = null;

            using (var ctx = new Context.Context())
            {
                locais = ctx.Locais.Include("Filial").Include("Filhos").Where(x => x.DataExclusao == null && x.Filial.EmpresaID == empresaID).ToList();
            }

            return locais;
        }

        public List<Local> GetByFilialID(int filialID)
        {
            List<Local> locais = null;

            using (var ctx = new Context.Context())
            {
                locais = ctx.Locais.Include("Filial").Include("Filhos").Where(x => x.DataExclusao == null && x.FilialID == filialID).ToList();
            }

            return locais;
        }

        public Local GetByID(int id)
        {
            Local local = null;

            using (var ctx = new Context.Context())
            {
                local = ctx.Locais.Include("Filial").Include("Filhos.Filial").Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            return local;
        }

        public Local GetByCode(int empresaID, string codigo)
        {
            Local local = null;

            using (var ctx = new Context.Context())
            {
                local = ctx.Locais.Where(x => x.DataExclusao == null && x.Codigo == codigo && x.Filial.EmpresaID == empresaID).SingleOrDefault();
            }

            return local;
        }

        public void Insert(Local local)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Locais.Add(local);
                ctx.SaveChanges();
            }
        }

        public void Update(Local local)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(local).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void DeleteAll(int empresaID)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Database.ExecuteSqlCommand($"UPDATE LOCAL SET DATAEXCLUSAO = GETDATE() WHERE DATAEXCLUSAO IS NULL AND FILIALID IN (SELECT ID FROM FILIAL WHERE EMPRESAID = {empresaID})");
                ctx.SaveChanges();
            }
        }
    }
}
