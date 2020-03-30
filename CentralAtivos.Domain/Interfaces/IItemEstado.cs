using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IItemEstado
    {
        List<ItemEstado> GetAll();
        ItemEstado GetByName(string name);
        void Insert(ItemEstado itemEstado);
    }
}
