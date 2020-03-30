using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IPlaca
    {
        List<Placa> GetByPlacaGrupoID(int placaGrupoID);
        List<Placa> GetJumpsByPlacaGrupoID(int placaGrupoID);
        Placa GetByID(int id);
        void Insert(Placa placa);
        void Update(Placa placa);
        void Delete(int id);
    }
}
