using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class FilialRepository : IFilial
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var filial = ctx.Filiais.Find(id);

                if (filial != null)
                {
                    filial.DataExclusao = DateTime.Now;

                    ctx.Entry(filial).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }                    
            }
        }

        public List<Filial> GetByEmpresaID(int empresaID)
        {
            List<Filial> filiais = null;

            using (var ctx = new Context.Context())
            {
                filiais = ctx.Filiais.Where(x => x.DataExclusao == null && x.EmpresaID == empresaID).ToList();
            }

            return filiais;
        }

        public Filial GetByID(int id)
        {
            Filial filial = null;

            using (var ctx = new Context.Context())
            {
                filial = ctx.Filiais.Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            return filial;
        }

        public Filial GetByCode(int empresaID, string codigo)
        {
            Filial filial = null;

            using (var ctx = new Context.Context())
            {
                filial = ctx.Filiais.Where(x => x.DataExclusao == null && x.EmpresaID == empresaID && x.Codigo == codigo).SingleOrDefault();
            }

            return filial;
        }

        public void Insert(Filial filial)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Filiais.Add(filial);
                ctx.SaveChanges();
            }
        }

        public void Update(Filial filial)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(filial).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void DeleteAll(int empresaID)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Database.ExecuteSqlCommand($"UPDATE FILIAL SET DATAEXCLUSAO = GETDATE() WHERE DATAEXCLUSAO IS NULL AND EMPRESAID = {empresaID}");
                ctx.SaveChanges();
            }
        }
    }
}
