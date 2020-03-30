using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IInventarioUsuario
    {
        List<InventarioUsuario> GetByInventarioID(int inventarioID);
        InventarioUsuario GetByID(int id);
        void Insert(InventarioUsuario inventarioUsuario);
        void Delete(int id);
    }
}
