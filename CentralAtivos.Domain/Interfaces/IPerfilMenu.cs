using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IPerfilMenu
    {
        List<PerfilMenu> GetByPerfilID(int perfilID);
        PerfilMenu GetByID(int id);
        void Insert(PerfilMenu perfilMenu);
        void Delete(int id);
    }
}
