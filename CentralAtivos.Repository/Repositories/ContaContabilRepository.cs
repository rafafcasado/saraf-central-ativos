using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class ContaContabilRepository : IContaContabil
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var contaContabil = ctx.ContasContabeis.Find(id);

                if (contaContabil != null)
                {
                    contaContabil.DataExclusao = DateTime.Now;

                    ctx.Entry(contaContabil).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }                    
            }
        }

        public List<ContaContabil> GetByEmpresaID(int empresaID)
        {
            List<ContaContabil> contaContabil = null;

            using (var ctx = new Context.Context())
            {
                contaContabil = ctx.ContasContabeis.Include("Filhos").Include("Pai").Where(x => x.DataExclusao == null && x.EmpresaID == empresaID).ToList();
            }

            return contaContabil;
        }

        public ContaContabil GetByID(int id)
        {
            ContaContabil contaContabil = null;

            using (var ctx = new Context.Context())
            {
                contaContabil = ctx.ContasContabeis.Include("Filhos").Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            return contaContabil;
        }

        public ContaContabil GetByCode(int empresaID, string codigo)
        {
            ContaContabil contaContabil = null;

            using (var ctx = new Context.Context())
            {
                contaContabil = ctx.ContasContabeis.Where(x => x.DataExclusao == null && x.EmpresaID == empresaID && x.Codigo == codigo).SingleOrDefault();
            }

            return contaContabil;
        }

        public void Insert(ContaContabil contaContabil)
        {
            using (var ctx = new Context.Context())
            {
                ctx.ContasContabeis.Add(contaContabil);
                ctx.SaveChanges();
            }
        }

        public void Update(ContaContabil contaContabil)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(contaContabil).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

    }
}
