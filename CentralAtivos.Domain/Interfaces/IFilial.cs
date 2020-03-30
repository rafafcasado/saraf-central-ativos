using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IFilial
    {
        List<Filial> GetByEmpresaID(int empresaID);
        Filial GetByID(int id);
        Filial GetByCode(int empresaID, string codigo);
        void Insert(Filial filial);
        void Update(Filial filial);
        void Delete(int id);
        void DeleteAll(int empresaID);
    }
}
