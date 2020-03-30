using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IOrdemServico
    {
        List<OrdemServico> GetByEmpresaID(int empresaID);
        OrdemServico GetByID(int id);
        void Insert(OrdemServico ordemServico);
        void Update(OrdemServico ordemServico);
        void Delete(int id);
    }
}
