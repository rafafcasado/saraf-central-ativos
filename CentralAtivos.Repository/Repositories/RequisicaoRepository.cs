using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class RequisicaoRepository : IRequisicao
    {
        public Requisicao Get(string nomeMetodo, string entidade)
        {
            Requisicao requisicao = null;

            using (var ctx = new Context.Context())
            {
                requisicao = ctx.Requisicoes.Where(x => x.NomeMetodo.ToLower() == nomeMetodo.ToLower() && x.Entidade.ToLower() == entidade.ToLower()).SingleOrDefault();
            }

            return requisicao;
        }

        public List<Requisicao> GetAll()
        {
            List<Requisicao> requisicoes = null;

            using (var ctx = new Context.Context())
            {
                requisicoes = ctx.Requisicoes.Where(x => x.DataExclusao == null).ToList();
            }

            return requisicoes;
        }
    }
}
