using CentralAtivos.Domain.Entities;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IOrdemServicoMotivoCampoValor
    {
        void Insert(OrdemServicoMotivoCampoValor ordemServicoMotivoCampoValor);

        OrdemServicoMotivoCampoValor Get(int ordemServicoID, int ordemServicoMotivoCampoID);
    }
}
