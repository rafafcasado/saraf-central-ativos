using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IEmpresa
    {
        List<Empresa> GetAll();
        List<Empresa> GetByInventarioUsuarioID(int usuarioID);
        Empresa GetByID(int id);
        void Insert(Empresa empresa);
        void Update(Empresa empresa);
        void Delete(int id);
    }
}
