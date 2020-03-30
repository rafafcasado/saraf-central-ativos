using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class LocalController : ApiController
    {
        private readonly ILocal _repository;

        public LocalController(ILocal repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista dos Principais Locais Ativos Cadastrados para uma Filial de acordo com o ID informado
        /// </summary>
        /// <param name="filialID">ID da Filial</param>
        [HttpGet]
        [Route("api/local/getPrincipaisByFilialID/{filialID}")]
        public IHttpActionResult GetPrincipaisByFilialID(int filialID)
        {
            try
            {
                var locais = _repository.GetByFilialID(filialID);

                locais = locais.Where(x => x.PaiID == null).ToList();

                return Ok(locais);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Lista dos Sub Locais Ativos Cadastrados para um Local de acordo com o ID informado
        /// </summary>
        /// <param name="localID">ID do Local</param>
        [HttpGet]
        [Route("api/local/getSubLocaisByLocalID/{localID}")]
        public IHttpActionResult GetSubLocaisByLocalID(int localID)
        {
            try
            {
                var local = _repository.GetByID(localID);

                if (local == null)
                    return NotFound();

                return Ok(local.Filhos.Where(x => x.DataExclusao == null));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna todos os Locais de acordo com o ID da Filial informada
        /// </summary>
        /// <param name="filialID">ID da Filial</param>
        [HttpGet, Route("api/local/getAllByFilialID/{filialID}")]
        public IHttpActionResult GetAllByFilialID(int filialID)
        {
            try
            {
                var locais = _repository.GetByFilialID(filialID);

                if (locais.Count > 0)
                {
                    foreach (var local in locais)
                    {
                        bool fim = false;
                        var pai = local.PaiID == null ? null : locais.Where(x => x.ID == local.PaiID).SingleOrDefault();

                        while (!fim)
                        {
                            if (pai == null)
                            {
                                fim = true;
                                local.CaminhoPao = local.CaminhoPao == null || local.CaminhoPao.Length <= 0 ? string.Format("{0} {1}", local.Codigo, local.Nome) : (local.CaminhoPao + string.Format("{0} {1}", local.Codigo, local.Nome));
                            }
                            else
                            {

                                fim = false;
                                local.CaminhoPao = string.Format("{0} {1}", pai.Codigo, pai.Nome) + " > " + local.CaminhoPao;
                                pai = locais.Where(x => x.ID == pai.PaiID).SingleOrDefault();
                            }
                        }

                    }
                }

                locais = locais.OrderBy(x => x.CodigoInterno).ToList();

                return Ok(locais);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna todos os Locais de acordo com vários IDs de Filial informados
        /// </summary>
        /// <param name="filiaisID">IDs de Filiais</param>
        [HttpGet, Route("api/local/getAllByManyFiliaisID/{filiaisID}")]
        public IHttpActionResult GetAllByManyFiliaisID(string filiaisID)
        {
            try
            {
                var locais = new List<Local>();

                var filiais = filiaisID.Split(',');

                foreach (var filial in filiais)
                {
                    locais.AddRange(_repository.GetByFilialID(Convert.ToInt32(filial)));
                }

                if (locais.Count > 0)
                {
                    foreach (var local in locais)
                    {
                        bool fim = false;
                        var pai = local.PaiID == null ? null : locais.Where(x => x.ID == local.PaiID).SingleOrDefault();

                        while (!fim)
                        {
                            if (pai == null)
                            {
                                fim = true;
                                local.CaminhoPao = local.CaminhoPao == null || local.CaminhoPao.Length <= 0 ? local.Nome : (local.CaminhoPao + local.Nome);
                                local.CaminhoPao = local.Filial.Nome + " > " + local.CaminhoPao;
                            }
                            else
                            {

                                fim = false;
                                local.CaminhoPao = pai.Nome + " > " + local.CaminhoPao;
                                local.CaminhoPao = local.Filial.Nome + " > " + local.CaminhoPao;
                                pai = locais.Where(x => x.ID == pai.PaiID).SingleOrDefault();
                            }
                        }

                    }
                }

                locais = locais.OrderBy(x => x.Filial.Nome).ThenBy(x => x.CodigoInterno).ToList();

                return Ok(locais);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna todos os Locais de acordo com o ID da empresa informada
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        [HttpGet, Route("api/local/getbyempresaid/{empresaID}")]
        public IHttpActionResult GetByEmpresaID(int empresaID)
        {
            try
            {
                var locais = _repository.GetByEmpresaID(empresaID);

                if (locais.Count > 0)
                {
                    foreach (var local in locais)
                    {
                        bool fim = false;
                        var pai = local.PaiID == null ? null : locais.Where(x => x.ID == local.PaiID).SingleOrDefault();

                        while (!fim)
                        {
                            if (pai == null)
                            {
                                fim = true;
                                local.CaminhoPao = local.CaminhoPao == null || local.CaminhoPao.Length <= 0 ? local.Nome : (local.CaminhoPao + local.Nome);
                            }
                            else
                            {

                                fim = false;
                                local.CaminhoPao = pai.Nome + " > " + local.CaminhoPao;
                                pai = locais.Where(x => x.ID == pai.PaiID).SingleOrDefault();
                            }
                        }

                    }
                }

                locais = locais.OrderBy(x => x.CodigoInterno).ToList();

                return Ok(locais);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Local relacionado ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var local = _repository.GetByID(id);

                if (local == null)
                    return NotFound();

                return Ok(local);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui um Local
        /// </summary>
        /// <param name="local">Objeto do tipo Local</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Local local)
        {
            try
            {
                var locais = _repository.GetByFilialID(local.FilialID);

                if (locais.Count == 0)
                    local.CodigoInterno = 1;
                else
                {
                    if (local.PaiID == null)
                        local.CodigoInterno = locais.Where(x => x.PaiID == null).Max(x => x.CodigoInterno) + 1;
                    else
                    {
                        var pai = locais.Where(x => x.ID == local.PaiID).Single();

                        double proximoCodigo = 0;

                        if (pai.Filhos.Count == 0)
                            proximoCodigo = Convert.ToDouble(pai.CodigoInterno.ToString().Split(',').Count() == 1 ? pai.CodigoInterno.ToString() + "," + "1" : pai.CodigoInterno.ToString().Split(',')[0] + "," + pai.CodigoInterno.ToString().Split(',')[1] + "1");
                        else
                        {
                            double ultimoCodigo = pai.Filhos.Max(x => x.CodigoInterno);
                            string codigo = (Convert.ToInt32(ultimoCodigo.ToString().Split(',')[1]) + 1).ToString();
                            proximoCodigo = Convert.ToDouble(ultimoCodigo.ToString().Split(',')[0] + "," + codigo);
                        }
                        
                        local.CodigoInterno = proximoCodigo;
                    }
                }

                _repository.Insert(local);

                return Ok(local);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um Local
        /// </summary>
        /// <param name="local">Objeto do tipo Local</param>
        /// <param name="id">ID do Local que será atualizado</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Local local)
        {
            try
            {
                var localDB = _repository.GetByID(id);

                if (localDB == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                localDB.CentroCustoID = local.CentroCustoID;
                localDB.Codigo = local.Codigo;
                localDB.FilialID = local.FilialID;
                localDB.Nome = local.Nome;
                localDB.ResponsavelID = local.ResponsavelID;

                _repository.Update(localDB);

                return Ok(localDB);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um Local
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var local = _repository.GetByID(id);

                if (local == null)
                    return NotFound();

                var filhos = _repository.GetByFilialID(local.FilialID).Where(x => x.CodigoInterno.ToString().StartsWith(local.CodigoInterno.ToString()));

                foreach (var filho in filhos)
                {
                    _repository.Delete(filho.ID);
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
