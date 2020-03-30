using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var usuario = ctx.Usuarios.Find(id);

                if (usuario != null)
                {
                    usuario.ConfirmacaoSenha = usuario.Senha;
                    usuario.DataExclusao = DateTime.Now;

                    ctx.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();

                    var responsavel = ctx.Responsaveis.Where(x => x.DataExclusao == null && x.UsuarioID == usuario.ID).SingleOrDefault();

                    if (responsavel != null)
                    {
                        responsavel.UsuarioID = null;

                        ctx.Entry(responsavel).State = System.Data.Entity.EntityState.Modified;
                        ctx.SaveChanges();
                    }
                }                    
            }
        }

        public bool EmailExists(string email)
        {
            using (var ctx = new Context.Context())
            {
                var usuario = ctx.Usuarios.Where(x => x.Email.ToLower() == email.ToLower() && x.DataExclusao == null).SingleOrDefault();

                return usuario == null ? false : true;
            }
        }

        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = null;

            using (var ctx = new Context.Context())
            {
                usuarios = ctx.Usuarios.Include("Perfil").Include("Empresa").Where(x => x.DataExclusao == null).OrderBy(x => x.Nome).ToList();
            }

            return usuarios;
        }

        public List<Usuario> GetByEmpresaID(int empresaID)
        {
            List<Usuario> usuarios = null;

            using (var ctx = new Context.Context())
            {
                usuarios = ctx.Usuarios.Include("Empresa").Include("Perfil").Where(x => x.DataExclusao == null && x.EmpresaID == empresaID).ToList();
            }

            return usuarios;
        }

        public Usuario GetByID(int id)
        {
            Usuario usuario = null;

            using (var ctx = new Context.Context())
            {
                usuario = ctx.Usuarios.Include("Empresa").Include("Perfil").Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            return usuario;
        }

        public void Insert(Usuario usuario)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Usuarios.Add(usuario);

                ctx.SaveChanges();

                if (usuario.EmpresaID != null)
                {
                    ctx.Responsaveis.Add(new Responsavel() { Nome = usuario.Nome, EmpresaID = Convert.ToInt32(usuario.EmpresaID), UsuarioID = usuario.ID });

                    ctx.SaveChanges();
                }
            }
        }

        public Usuario Login(string login, string senha)
        {
            Usuario usuario = null;

            using (var ctx = new Context.Context())
            {
                usuario = ctx.Usuarios.Where(x => x.Email.ToLower() == login.ToLower() && x.DataExclusao == null).SingleOrDefault();

                if (usuario != null && usuario.Senha != Helpers.Criptografia.Cripto(senha))
                    usuario = null;
            }

            return usuario;
        }

        public void Update(Usuario usuario)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

    }
}
