using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface ILocal
    {
        List<Local> GetByEmpresaID(int empresaID);
        List<Local> GetByFilialID(int filialID);
        Local GetByID(int id);
        Local GetByCode(int empresaID, string codigo);
        void Insert(Local local);
        void Update(Local local);
        void Delete(int id);
        void DeleteAll(int empresaID);
    }
}
