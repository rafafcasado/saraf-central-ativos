using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface ICentroCusto
    {
        List<CentroCusto> GetByEmpresaID(int empresaID);
        CentroCusto GetByID(int id);
        CentroCusto GetByCode(int empresaID, string codigo);
        void Insert(CentroCusto centroCusto);
        void Update(CentroCusto centroCusto);
        void Delete(int id);
        void DeleteAll(int empresaID);
    }
}
