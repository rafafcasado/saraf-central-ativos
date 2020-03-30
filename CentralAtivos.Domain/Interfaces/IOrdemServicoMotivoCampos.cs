using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IOrdemServicoMotivoCampo
    {
        OrdemServicoMotivoCampo GetByMotivoCampo(int motivoID, int campoID);
        List<OrdemServicoMotivoCampo> GetByMotivoID(int motivoID);
    }
}
