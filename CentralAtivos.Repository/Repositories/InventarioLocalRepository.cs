using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class InventarioLocalRepository : IInventarioLocal
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var inventarioLocal = ctx.InventarioLocais.Find(id);

                if (inventarioLocal != null)
                {
                    inventarioLocal.DataExclusao = DateTime.Now;

                    ctx.Entry(inventarioLocal).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }                    
            }
        }

        public List<InventarioLocal> GetByInventarioID(int inventarioID)
        {
            List<InventarioLocal> inventarioLocais = null;

            List<Local> locais = null;

            using (var ctx = new Context.Context())
            {
                inventarioLocais = ctx.InventarioLocais.Include("Local.Filial").Where(x => x.DataExclusao == null && x.InventarioID == inventarioID).ToList();

                locais = ctx.Locais.ToList();
            }

            if (inventarioLocais.Select(x => x.Local).Count() > 0)
            {
                foreach (var local in inventarioLocais.Select(x => x.Local))
                {
                    bool fim = false;
                    var pai = local.PaiID == null ? null : locais.Where(x => x.ID == local.PaiID).SingleOrDefault();

                    while (!fim)
                    {
                        if (pai == null)
                        {
                            fim = true;
                            local.CaminhoPao = local.CaminhoPao == null || local.CaminhoPao.Length <= 0 ? local.Nome : (local.CaminhoPao + local.Nome);
                        }
                        else
                        {

                            fim = false;
                            local.CaminhoPao = pai.Nome + " > " + local.CaminhoPao;
                            pai = locais.Where(x => x.ID == pai.PaiID).SingleOrDefault();
                        }
                    }

                }
            }

            return inventarioLocais;
        }

        public InventarioLocal GetByID(int id)
        {
            InventarioLocal inventarioLocal = null;

            using (var ctx = new Context.Context())
            {
                inventarioLocal = ctx.InventarioLocais.Where(x => x.ID == id).SingleOrDefault();
            }

            return inventarioLocal;
        }

        public void Insert(InventarioLocal inventarioLocal)
        {
            using (var ctx = new Context.Context())
            {
                var inventarioLocalCadastrado = ctx.InventarioLocais.Where(x => x.DataExclusao == null && x.InventarioID == inventarioLocal.InventarioID && x.LocalID == inventarioLocal.LocalID).SingleOrDefault();

                if (inventarioLocalCadastrado == null)
                {
                    ctx.InventarioLocais.Add(inventarioLocal);
                    ctx.SaveChanges();

                    var local = ctx.Locais.Find(inventarioLocal.LocalID);

                    var inventarioFilial = ctx.InventarioFiliais.Where(x => x.FilialID == local.FilialID && x.InventarioID == inventarioLocal.InventarioID).SingleOrDefault();

                    if (inventarioFilial == null)
                    {
                        ctx.InventarioFiliais.Add(new InventarioFilial() {
                            FilialID = local.FilialID,
                            InventarioID = inventarioLocal.InventarioID
                        });

                        ctx.SaveChanges();
                    }
                }
                else
                    throw new ArgumentException("Local já vinculado a esse Inventário.");
            }
        }

    }
}
