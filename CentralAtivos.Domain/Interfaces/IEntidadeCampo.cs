using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IEntidadeCampo
    {
        List<EntidadeCampo> GetByEntidadeID(int id);
    }
}
