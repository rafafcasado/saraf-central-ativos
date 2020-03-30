using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IEspeciePropriedade
    {
        List<EspeciePropriedade> GetByEmpresaID(int empresaID);
        List<EspeciePropriedade> GetByEspecieID(int especieID);
        EspeciePropriedade GetByID(int id);
        void Insert(EspeciePropriedade especiePropriedade);
        void Delete(int id);
    }
}
