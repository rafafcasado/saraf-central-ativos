using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class GrupoRepository : IGrupo
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var grupo = ctx.Grupos.Find(id);

                if (grupo != null)
                {
                    grupo.DataExclusao = DateTime.Now;

                    ctx.Entry(grupo).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }                    
            }
        }

        public List<Grupo> GetByEmpresaID(int empresaID)
        {
            List<Grupo> grupos = null;

            using (var ctx = new Context.Context())
            {
                //grupos = ctx.Grupos.Include("Especies.Propriedades.Propriedade.Valores").Where(x => x.DataExclusao == null && x.EmpresaID == empresaID).ToList();
                grupos = ctx.Grupos.Where(x => x.DataExclusao == null && x.EmpresaID == empresaID).ToList();
            }

            return grupos;
        }

        public Grupo GetByID(int id)
        {
            Grupo grupo = null;

            using (var ctx = new Context.Context())
            {
                //grupo = ctx.Grupos.Include("Especies.Propriedades.Propriedade.Valores").Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
                grupo = ctx.Grupos.Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            return grupo;
        }

        public Grupo GetByCode(int empresaID, string codigo)
        {
            Grupo grupo = null;

            using (var ctx = new Context.Context())
            {
                grupo = ctx.Grupos.Where(x => x.DataExclusao == null && x.EmpresaID == empresaID && x.Codigo == codigo).SingleOrDefault();
            }

            return grupo;
        }

        public void Insert(Grupo grupo)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Grupos.Add(grupo);
                ctx.SaveChanges();
            }
        }

        public void Update(Grupo grupo)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(grupo).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void DeleteAll(int empresaID)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Database.ExecuteSqlCommand($"UPDATE GRUPO SET DATAEXCLUSAO = GETDATE() WHERE DATAEXCLUSAO IS NULL AND EMPRESAID = {empresaID}");
                ctx.SaveChanges();
            }
        }
    }
}
