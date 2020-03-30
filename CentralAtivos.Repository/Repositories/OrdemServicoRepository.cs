using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class OrdemServicoRepository : IOrdemServico
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var ordem = ctx.OrdensServico.Find(id);

                if (ordem != null)
                {
                    ordem.DataExclusao = DateTime.Now;

                    ctx.Entry(ordem).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
        }

        public List<OrdemServico> GetByEmpresaID(int empresaID)
        {
            List<OrdemServico> ordens = null;

            using (var ctx = new Context.Context())
            {
                ordens = ctx.OrdensServico.Include("Empresa").Include("OrdemServicoMotivo").Where(x => x.DataExclusao == null && x.EmpresaID == empresaID).ToList();
            }

            return ordens;
        }

        public OrdemServico GetByID(int id)
        {
            OrdemServico ordem = null;

            using (var ctx = new Context.Context())
            {
                ordem = ctx.OrdensServico.Include("Empresa").Include("OrdemServicoMotivo").Include("Anexos").Where(x => x.ID == id).SingleOrDefault();
            }

            return ordem;
        }

        public void Insert(OrdemServico ordem)
        {
            using (var ctx = new Context.Context())
            {
                ctx.OrdensServico.Add(ordem);
                ctx.SaveChanges();
            }
        }

        public void Update(OrdemServico ordemServico)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(ordemServico).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}