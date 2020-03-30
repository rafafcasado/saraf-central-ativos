using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IResponsavel
    {
        List<Responsavel> GetByEmpresaID(int empresaID);
        Responsavel GetByID(int id);
        Responsavel GetByUsuarioID(int usuarioID);
        void Insert(Responsavel responsavel);
        void Update(Responsavel responsavel);
        void Delete(int id);
        void DeleteAll(int empresaID);
    }
}
