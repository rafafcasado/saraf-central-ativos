using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class EspecieRepository : IEspecie
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var especie = ctx.Especies.Find(id);

                if (especie != null)
                {
                    especie.DataExclusao = DateTime.Now;

                    ctx.Entry(especie).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }                    
            }
        }

        public List<Especie> GetByEmpresaID(int empresaID)
        {
            List<Especie> especies = null;

            using (var ctx = new Context.Context())
            {
                especies = ctx.Especies.Include("Grupo").Include("EspeciePropriedades.Propriedade.Valores").Where(x => x.DataExclusao == null && x.Grupo.EmpresaID == empresaID && x.Grupo.DataExclusao == null).ToList();
            }

            return especies;
        }

        public List<Especie> GetByGrupoID(int grupoID)
        {
            List<Especie> especies = null;

            using (var ctx = new Context.Context())
            {
                especies = ctx.Especies.Where(x => x.DataExclusao == null && x.GrupoID == grupoID).ToList();
            }

            return especies;
        }

        public Especie GetByID(int id)
        {
            Especie especie = null;

            using (var ctx = new Context.Context())
            {
                especie = ctx.Especies.Include("Grupo").Include("EspeciePropriedades.Propriedade.Valores").Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            return especie;
        }

        public Especie GetByCode(int empresaID, string codigo)
        {
            Especie especie = null;

            using (var ctx = new Context.Context())
            {
                especie = ctx.Especies.Where(x => x.DataExclusao == null && x.Codigo == codigo && x.Grupo.EmpresaID == empresaID).SingleOrDefault();
            }

            return especie;
        }

        public void Insert(Especie especie)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Especies.Add(especie);
                ctx.SaveChanges();
            }
        }

        public void Update(Especie especie)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(especie).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void DeleteAll(int empresaID)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Database.ExecuteSqlCommand($"UPDATE ESPECIE SET DATAEXCLUSAO = GETDATE() WHERE DATAEXCLUSAO IS NULL AND GRUPOID IN (SELECT ID FROM GRUPO WHERE EMPRESAID = {empresaID})");
                ctx.SaveChanges();
            }
        }
    }
}
