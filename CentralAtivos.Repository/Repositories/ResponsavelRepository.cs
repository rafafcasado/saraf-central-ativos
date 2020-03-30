using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class ResponsavelRepository : IResponsavel
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var resp = ctx.Responsaveis.Find(id);

                if (resp != null)
                {
                    resp.DataExclusao = DateTime.Now;

                    ctx.Entry(resp).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }                    
            }
        }

        public List<Responsavel> GetByEmpresaID(int empresaID)
        {
            List<Responsavel> resps = null;

            using (var ctx = new Context.Context())
            {
                resps = ctx.Responsaveis.Include("Usuario.Empresa").Include("Usuario.Perfil").Where(x => x.DataExclusao == null && x.EmpresaID == empresaID).ToList();
            }

            return resps;
        }

        public Responsavel GetByID(int id)
        {
            Responsavel resp = null;

            using (var ctx = new Context.Context())
            {
                resp = ctx.Responsaveis.Include("Usuario.Empresa").Include("Usuario.Perfil").Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            return resp;
        }

        public Responsavel GetByUsuarioID(int usuarioID)
        {
            Responsavel resp = null;

            using (var ctx = new Context.Context())
            {
                resp = ctx.Responsaveis.Include("Usuario.Empresa").Include("Usuario.Perfil").Where(x => x.DataExclusao == null && x.UsuarioID == usuarioID).FirstOrDefault();
            }

            return resp;
        }

        public void Insert(Responsavel resp)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Responsaveis.Add(resp);
                ctx.SaveChanges();
            }
        }

        public void Update(Responsavel resp)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(resp).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void DeleteAll(int empresaID)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Database.ExecuteSqlCommand($"UPDATE RESPONSAVEL SET DATAEXCLUSAO = GETDATE() WHERE DATAEXCLUSAO IS NULL AND EMPRESAID = {empresaID}");
                ctx.SaveChanges();
            }
        }
    }
}
