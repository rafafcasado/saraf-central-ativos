using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IOrdemServicoItem
    {
        void Insert(OrdemServicoItem ordemServicoItem);

        List<OrdemServicoItem> GetByOrdemServicoID(int ordemServicoID);
    }
}
