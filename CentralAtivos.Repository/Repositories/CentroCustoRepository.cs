using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class CentroCustoRepository : ICentroCusto
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var centroCusto = ctx.CentrosCusto.Find(id);

                if (centroCusto != null)
                {
                    centroCusto.DataExclusao = DateTime.Now;

                    ctx.Entry(centroCusto).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
        }

        public List<CentroCusto> GetByEmpresaID(int empresaID)
        {
            List<CentroCusto> centrosCusto = null;

            using (var ctx = new Context.Context())
            {
                centrosCusto = ctx.CentrosCusto.Include("Filhos").Where(x => x.DataExclusao == null && x.EmpresaID == empresaID).ToList();
            }

            return centrosCusto;
        }

        public CentroCusto GetByID(int id)
        {
            CentroCusto centroCusto = null;

            using (var ctx = new Context.Context())
            {
                centroCusto = ctx.CentrosCusto.Include("Filhos").Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            return centroCusto;
        }

        public CentroCusto GetByCode(int empresaID, string codigo)
        {
            CentroCusto centroCusto = null;

            using (var ctx = new Context.Context())
            {
                centroCusto = ctx.CentrosCusto.Where(x => x.DataExclusao == null && x.EmpresaID == empresaID && x.Codigo == codigo).SingleOrDefault();
            }

            return centroCusto;
        }

        public void Insert(CentroCusto centroCusto)
        {
            using (var ctx = new Context.Context())
            {
                ctx.CentrosCusto.Add(centroCusto);
                ctx.SaveChanges();
            }
        }

        public void Update(CentroCusto centroCusto)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(centroCusto).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void DeleteAll(int empresaID)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Database.ExecuteSqlCommand($"UPDATE CENTROCUSTO SET DATAEXCLUSAO = GETDATE() WHERE DATAEXCLUSAO IS NULL AND EMPRESAID = {empresaID}");
                ctx.SaveChanges();
            }
        }
    }
}
