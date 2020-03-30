using System.Collections.Generic;
using System.Linq;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;

namespace CentralAtivos.Repository.Repositories
{
    public class SincronizacaoRepository : ISincronizacao
    {
        public List<Sincronizacao> GetAll()
        {
            List<Sincronizacao> sincronizacoes = null;

            using (var ctx = new Context.Context())
            {
                sincronizacoes = ctx.Sincronizacoes.Include("Inventario").Include("Inventario.Empresa").Include("Inventario.InventarioFiliais.Filial").Include("Inventario.InventarioLocais").Include("Inventario.Status").Include("Inventario.Usuarios").Include("UsuarioEnvio.Perfil").Where(x => x.DataExclusao == null).ToList();
            }

            return sincronizacoes;
        }

        public void Insert(Sincronizacao sincronizacao)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Sincronizacoes.Add(sincronizacao);
                ctx.SaveChanges();
            }
        }

        public Sincronizacao GetByID(int id)
        {
            Sincronizacao sincronizacao = null;

            using (var ctx = new Context.Context())
            {
                sincronizacao = ctx.Sincronizacoes.Find(id);
            }

            return sincronizacao;
        }

        public void Update(Sincronizacao sincronizacao)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(sincronizacao).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
