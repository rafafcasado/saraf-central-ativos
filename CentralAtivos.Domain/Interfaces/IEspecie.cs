using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IEspecie
    {
        List<Especie> GetByEmpresaID(int empresaID);
        List<Especie> GetByGrupoID(int grupoID);
        Especie GetByID(int id);
        Especie GetByCode(int empresaID, string codigo);
        void Insert(Especie especie);
        void Update(Especie especie);
        void Delete(int id);
        void DeleteAll(int empresaID);
    }
}
