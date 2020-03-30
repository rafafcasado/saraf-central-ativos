using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IInventario
    {
        List<Inventario> GetByEmpresaID(int empresaID);
        List<Inventario> GetByInventarioUsuarioID(int usuarioID);
        Inventario GetByID(int id);
        void Insert(Inventario inventario);
        void Update(Inventario inventario);
        void Delete(int id);
    }
}
