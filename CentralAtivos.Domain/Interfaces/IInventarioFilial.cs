using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IInventarioFilial
    {
        List<InventarioFilial> GetByInventarioID(int inventarioID);
        InventarioFilial GetByID(int id);
        void Insert(InventarioFilial inventarioFilial);
        void Delete(int id);
    }
}
