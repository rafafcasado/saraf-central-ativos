using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IPerfil
    {
        List<Perfil> GetAll();
        Perfil GetByID(int id);
        void Insert(Perfil usuario);
        void Update(Perfil usuario);
        void Delete(int id);
    }
}
