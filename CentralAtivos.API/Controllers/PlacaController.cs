using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class PlacaController : ApiController
    {
        private readonly IPlaca _repository;
        private readonly IPlacaGrupo _placaGrupoRepository;
        private readonly IItem _itemRepository;

        public PlacaController(IPlaca repository, IPlacaGrupo placaGrupoRepository, IItem itemRepository)
        {
            _repository = repository;
            _placaGrupoRepository = placaGrupoRepository;
            _itemRepository = itemRepository;
        }

        /// <summary>
        /// Retorna Lista de Placas de acordo com o ID do Grupo de Placas enviado
        /// </summary>
        [HttpGet]
        [Route("api/placa/getByPlacaGrupoID/{placaGrupoID}")]
        public IHttpActionResult GetByPlacaGrupoID(int placaGrupoID)
        {
            try
            {
                var placaGrupo = _placaGrupoRepository.GetByID(placaGrupoID);
                var placas = _repository.GetByPlacaGrupoID(placaGrupoID);

                List<object> lista = new List<object>();

                foreach (var placa in placas)
                {
                    string NumeroPlacaFormatada = placaGrupo.AplicaZerosEsquerda ? placa.NumeroPlaca.ToString().PadLeft(placaGrupo.Tamanho, '0') : placa.NumeroPlaca.ToString().PadRight(placaGrupo.Tamanho, '0');
                    var item = placa.ItemID == null ? null : _itemRepository.GetByID((int)placa.ItemID);

                    lista.Add(new
                    {
                        placa.DataCadastroFormatada,
                        placa.ID,
                        placa.ItemID,
                        Item = item == null ? null : new { item.Codigo, item.Nome, item.Incorporacao },
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
        /// Atualiza os dados de uma Placa
        /// </summary>
        /// <param name="id">ID da Placa que será alterada</param>
        /// <param name="placa">Objeto do tipo Placa</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Placa placa)
        {
            try
            {
                var placaDB = _repository.GetByID(id);

                if (placaDB == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest("Verificar campos obrigatórios");

                placaDB.ItemID = placa.ItemID;
                placaDB.Observacao = placa.Observacao;

                _repository.Update(placaDB);

                return Ok(placaDB);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
