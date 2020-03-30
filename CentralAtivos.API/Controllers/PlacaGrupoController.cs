using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class PlacaGrupoController : ApiController
    {
        private readonly IPlacaGrupo _repository;
        private readonly IPlaca _placaRepository;

        public PlacaGrupoController(IPlacaGrupo repository, IPlaca placaRepository)
        {
            _repository = repository;
            _placaRepository = placaRepository;
        }

        /// <summary>
        /// Retorna Lista de Grupos de Placa Cadastrados de acordo com ID do Inventário enviado
        /// </summary>
        [HttpGet]
        [Route("api/placaGrupo/getByInventarioID/{inventarioID}")]
        public IHttpActionResult GetByInventarioID(int inventarioID)
        {
            try
            {
                return Ok(_repository.GetByInventarioID(inventarioID));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Lista de Pulos de Placas de acordo com o ID do Grupo de Placas enviado
        /// </summary>
        [HttpGet]
        [Route("api/placaGrupo/getJumpsByPlacaGrupoID/{placaGrupoID}")]
        public IHttpActionResult GetJumpsByPlacaGrupoID(int placaGrupoID)
        {
            try
            {
                var placaGrupo = _repository.GetByID(placaGrupoID);
                var placas = _placaRepository.GetJumpsByPlacaGrupoID(placaGrupoID);

                List<object> lista = new List<object>();

                foreach (var placa in placas)
                {
                    string NumeroPlacaFormatada = placaGrupo.AplicaZerosEsquerda ? placa.NumeroPlaca.ToString().PadLeft(placaGrupo.Tamanho, '0') : placa.NumeroPlaca.ToString().PadRight(placaGrupo.Tamanho, '0');

                    lista.Add(new
                    {
                        placa.DataCadastroFormatada,
                        placa.ID,
                        placa.ItemID,
                        placa.NumeroPlaca,
                        placa.Observacao,
                        placa.PlacaGrupoID,
                        NumeroPlacaFormatada,
                        EditarObservacoes = false
                    });
                }

                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui um Grupo de Placas
        /// </summary>
        /// <param name="placaGrupo">Objeto do tipo PlacaGrupo</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] PlacaGrupo placaGrupo)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Verificar campos obrigatórios");

                _repository.Insert(placaGrupo);

                int numeroPlaca = placaGrupo.Inicio;

                while (numeroPlaca <= placaGrupo.Fim)
                {
                    _placaRepository.Insert(new Placa { NumeroPlaca = numeroPlaca, PlacaGrupoID = placaGrupo.ID });

                    numeroPlaca += 1;
                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um Grupo de Placas
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var placaGrupo = _repository.GetByID(id);

                if (placaGrupo == null)
                    return NotFound();

                var placas = _placaRepository.GetByPlacaGrupoID(id).ToList();

                if (placas.Where(x => x.ItemID != null).Count() > 0)
                    return BadRequest("Esse Grupo de Placas não pode ser excluído pois já existem Itens vinculados à ele");

                foreach (var placa in placas)
                {
                    _placaRepository.Delete(placa.ID);
                }

                _repository.Delete(id);

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
