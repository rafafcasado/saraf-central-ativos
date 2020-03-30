using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IPlacaGrupo
    {
        PlacaGrupo GetByID(int ID);
        List<PlacaGrupo> GetByInventarioID(int inventarioID);
        void Insert(PlacaGrupo placaGrupo);
        void Delete(int id);
    }
}
