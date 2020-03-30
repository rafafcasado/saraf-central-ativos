using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class PerfilRepository : IPerfil
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var perfil = ctx.Perfis.Find(id);

                if (perfil != null)
                {
                    perfil.DataExclusao = DateTime.Now;

                    ctx.Entry(perfil).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
        }

        public List<Perfil> GetAll()
        {
            List<Perfil> perfis = null;

            using (var ctx = new Context.Context())
            {
                perfis = ctx.Perfis.Where(x => x.DataExclusao == null).ToList();
            }

            return perfis;
        }

        public Perfil GetByID(int id)
        {
            Perfil perfil = null;

            using (var ctx = new Context.Context())
            {
                perfil = ctx.Perfis.Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            return perfil;
        }

        public void Insert(Perfil perfil)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Perfis.Add(perfil);

                ctx.SaveChanges();

                if (perfil.CopiarPerfil)
                {
                    var permissoesCopia = ctx.Permissoes.Where(x => x.PerfilID == perfil.PerfilCopiaID).ToList();

                    if (permissoesCopia.Count > 0)
                    {
                        foreach (var perm in permissoesCopia)
                        {
                            ctx.Permissoes.Add(new Permissao() {PerfilID = perfil.ID, FuncionalidadeID = perm.FuncionalidadeID, Metodos = perm.Metodos });
                        }
                    }
                }
                else
                {
                    foreach (var func in ctx.Funcionalidades.Where(x => x.DataExclusao == null))
                    {
                        ctx.Permissoes.Add(new Permissao() { PerfilID = perfil.ID, FuncionalidadeID = func.ID, Metodos = "GET,POST,PUT,DELETE" });
                    }
                }

                ctx.SaveChanges();
            }
        }

        public void Update(Perfil perfil)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(perfil).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
