using CentralAtivos.Domain.Entities;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IOrdemServicoCampo
    {
        OrdemServicoCampo GetByName(string name);
    }
}
