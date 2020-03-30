using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface ISincronizacao
    {
        List<Sincronizacao> GetAll();
        Sincronizacao GetByID(int id);
        void Insert(Sincronizacao sincronizacao);
        void Update(Sincronizacao sincronizacao);
    }
}
