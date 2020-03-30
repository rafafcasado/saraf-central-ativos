using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IRequisicao
    {
        List<Requisicao> GetAll();
        Requisicao Get(string nomeMetodo, string entidade);
    }
}
