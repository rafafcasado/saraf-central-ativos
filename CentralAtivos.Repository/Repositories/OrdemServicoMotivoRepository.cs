using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class OrdemServicoMotivoRepository : IOrdemServicoMotivo
    {
        public List<OrdemServicoMotivo> GetAll()
        {
            List<OrdemServicoMotivo> motivos = null;

            using (var ctx = new Context.Context())
            {
                motivos = ctx.OrdemServicoMotivos.Where(x => x.DataExclusao == null).OrderBy(x => x.Nome).ToList();

                foreach (var motivo in motivos)
                {
                    string campos = string.Empty;

                    foreach (var campo in ctx.OrdemServicoMotivoCampos.Where(x => x.OrdemServicoMotivoID == motivo.ID))
                    {
                        campos += campo.OrdemServicoCampo.NomeCampo + ",";
                    }

                    campos = campos.Substring(0, campos.Length - 1);

                    motivo.CamposVinculados = campos;
                }
            }

            return motivos;
        }
    }
}
