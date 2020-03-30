using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class FuncionalidadeRepository : IFuncionalidade
    {
        public List<Funcionalidade> GetAll()
        {
            List<Funcionalidade> funcionalidades = null;

            using (var ctx = new Context.Context())
            {
                funcionalidades = ctx.Funcionalidades.Where(x => x.DataExclusao == null).OrderBy(x => x.Nome).ToList();
            }

            return funcionalidades;
        }
    }
}
