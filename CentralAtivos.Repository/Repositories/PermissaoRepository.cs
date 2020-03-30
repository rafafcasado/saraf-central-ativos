using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class PermissaoRepository : IPermissao
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var permissao = ctx.Permissoes.Find(id);

                if (permissao != null)
                {
                    ctx.Permissoes.Remove(permissao);
                    ctx.SaveChanges();
                }
            }
        }

        public List<Permissao> GetByPerfilID(int PerfilID)
        {
            List<Permissao> permissoes = null;

            using (var ctx = new Context.Context())
            {
                permissoes = ctx.Permissoes.Include("Funcionalidade").Where(x => x.PerfilID == PerfilID).ToList();
            }

            return permissoes;
        }

        public void Insert(Permissao permissao)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Permissoes.Add(permissao);
                ctx.SaveChanges();
            }
        }

        public void Update(int perfilID, string funcionalidade, string metodo)
        {
            using (var ctx = new Context.Context())
            {
                var permissoes = ctx.Permissoes.Where(x => x.PerfilID == perfilID).ToList();

                var permissao = permissoes.Where(x => x.Funcionalidade.Nome.ToLower() == funcionalidade.ToLower() && x.Metodos.ToLower().Contains(metodo.ToLower())).FirstOrDefault();

                List<string> metodos = permissao.Metodos.Split(',').ToList();

                if (permissao == null)
                {
                    metodos.Add(metodo.ToUpper());
                }
                else
                {
                    metodos = metodos.Where(x => x != metodo.ToUpper()).ToList();
                }

                permissao.Metodos = string.Join(",", metodos);

                ctx.Entry(permissao).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
