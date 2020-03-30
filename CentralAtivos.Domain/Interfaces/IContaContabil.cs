using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IContaContabil
    {
        List<ContaContabil> GetByEmpresaID(int empresaID);
        ContaContabil GetByID(int id);
        ContaContabil GetByCode(int empresaID, string codigo);
        void Insert(ContaContabil contaContabil);
        void Update(ContaContabil contaContabil);
        void Delete(int id);
    }
}
