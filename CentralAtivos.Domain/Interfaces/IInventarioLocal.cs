using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IInventarioLocal
    {
        List<InventarioLocal> GetByInventarioID(int inventarioID);
        InventarioLocal GetByID(int id);
        void Insert(InventarioLocal inventarioLocal);
        void Delete(int id);
    }
}
