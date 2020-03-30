using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class EmpresaRepository : IEmpresa
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var empresa = ctx.Empresas.Find(id);

                if (empresa != null)
                {
                    empresa.DataExclusao = DateTime.Now;

                    ctx.Entry(empresa).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
        }

        public List<Empresa> GetAll()
        {
            List<Empresa> empresas = null;

            using (var ctx = new Context.Context())
            {
                empresas = ctx.Empresas.Where(x => x.DataExclusao == null).ToList();
            }

            return empresas;
        }

        public Empresa GetByID(int id)
        {
            Empresa empresa = null;

            using (var ctx = new Context.Context())
            {
                empresa = ctx.Empresas.Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            return empresa;
        }

        public List<Empresa> GetByInventarioUsuarioID(int usuarioID)
        {
            var empresas = new List<Empresa>();

            using (var ctx = new Context.Context())
            {
                var usuario = ctx.Usuarios.Where(x => x.ID == usuarioID).SingleOrDefault();

                if (usuario.PerfilID == 1)
                {
                    empresas.AddRange(ctx.Empresas.Where(x => x.DataExclusao == null));
                }
                else
                {
                    if (usuario.EmpresaID != null)
                    {
                        empresas.Add(usuario.Empresa);
                    }

                    string comm = @"select distinct
                                e.*
                                from 
                                InventarioUsuario iu
                                join Inventario i on i.ID = iu.InventarioID
                                join Empresa e on e.ID = i.EmpresaID
                                where UsuarioID = @usuarioID and iu.DataExclusao IS NULL";

                    empresas.AddRange(ctx.Empresas.SqlQuery(comm, new SqlParameter("@usuarioID", usuarioID)).ToList<Empresa>());

                    empresas = empresas.Distinct().ToList();
                }
            }

            return empresas.OrderBy(x => x.NomeFantasia).ToList();
        }

        public void Insert(Empresa empresa)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Empresas.Add(empresa);
                ctx.SaveChanges();
            }
        }

        public void Update(Empresa empresa)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(empresa).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

    }
}
