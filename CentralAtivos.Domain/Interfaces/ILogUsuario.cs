using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface ILogUsuario
    {
        void Insert(LogUsuario logUsuario);
    }
}
