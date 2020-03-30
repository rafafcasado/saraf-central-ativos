using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IOrdemServicoMotivo
    {
        List<OrdemServicoMotivo> GetAll();
    }
}
