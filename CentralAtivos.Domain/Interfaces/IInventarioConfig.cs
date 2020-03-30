using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IInventarioConfig
    {
        List<InventarioConfig> GetByInventarioID(int id);
        InventarioConfig GetByID(int id);
        void Update(InventarioConfig config);
    }
}
