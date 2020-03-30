using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IFuncionalidade
    {
        List<Funcionalidade> GetAll();
    }
}
