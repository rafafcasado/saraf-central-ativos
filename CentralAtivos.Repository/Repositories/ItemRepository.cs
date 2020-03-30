using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class ItemRepository : IItem
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var item = ctx.Itens.Find(id);

                if (item != null)
                {
                    item.DataExclusao = DateTime.Now;

                    ctx.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
        }

        public List<Item> GetByEmpresaID(int empresaID)
        {
            List<Item> itens = null;

            using (var ctx = new Context.Context())
            {
                itens = ctx.Itens.Include("Local.Filial").Include("Especie").Include("CamposExtras").Include("PropriedadesValores").Where(x => x.DataExclusao == null && x.EmpresaID == empresaID).ToList();
            }

            foreach (var item in itens)
            {
                var path = System.Configuration.ConfigurationManager.AppSettings["GaleriaImagens"] + item.ID;

                var files = Directory.GetFiles(path);

                foreach (var file in files)
                {
                    FileInfo info = new FileInfo(file);

                    item.Imagens.Add(System.Configuration.ConfigurationManager.AppSettings["GaleriaImagensLogico"] + item.ID + "/" + info.Name);
                }
            }

            return itens;
        }

        public List<Item> GetByLocalID(int localID)
        {
            List<Item> itens = null;

            using (var ctx = new Context.Context())
            {
                itens = ctx.Itens.Include("Local.Filial").Include("Especie").Include("CamposExtras").Include("PropriedadesValores").Where(x => x.DataExclusao == null && x.LocalID == localID).ToList();
            }

            foreach (var item in itens)
            {
                var path = System.Configuration.ConfigurationManager.AppSettings["GaleriaImagens"] + item.ID;

                var files = Directory.GetFiles(path);

                foreach (var file in files)
                {
                    FileInfo info = new FileInfo(file);

                    item.Imagens.Add(System.Configuration.ConfigurationManager.AppSettings["GaleriaImagensLogico"] + item.ID + "/" + info.Name);
                }
            }

            return itens;
        }

        public Item GetByID(int id)
        {
            Item item = null;

            using (var ctx = new Context.Context())
            {
                item = ctx.Itens.Include("Local.Filial").Include("Especie").Include("CamposExtras").Include("PropriedadesValores").Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            var path = System.Configuration.ConfigurationManager.AppSettings["GaleriaImagens"] + item.ID;

            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                FileInfo info = new FileInfo(file);

                item.Imagens.Add(System.Configuration.ConfigurationManager.AppSettings["GaleriaImagensLogico"] + item.ID + "/" + info.Name);
            }

            return item;
        }

        public void Insert(Item item)
        {
            using (var ctx = new Context.Context())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var propriedadesValores = item.PropriedadesValores;
                        var camposExtras = item.CamposExtras;

                        item.PropriedadesValores = null;
                        item.CamposExtras = null;
                        item.Local = null;
                        item.Especie = null;

                        

                        ctx.Itens.Add(item);
                        ctx.SaveChanges();

                        if (propriedadesValores != null)
                        {
                            foreach (var pv in propriedadesValores)
                            {
                                ctx.ItemPropriedadesValores.Add(new ItemPropriedadeValor()
                                {
                                    ItemID = item.ID,
                                    PropriedadeID = pv.PropriedadeID,
                                    PropriedadeValorID = pv.PropriedadeValorID,
                                    Valor = pv.Valor
                                });

                                ctx.SaveChanges();
                            }
                        }

                        if (camposExtras != null)
                        {
                            foreach (var ce in camposExtras)
                            {
                                ctx.CamposExtras.Add(new CampoExtra()
                                {
                                    ItemID = item.ID,
                                    Nome = ce.Nome,
                                    Valor = ce.Valor
                                });

                                ctx.SaveChanges();
                            }
                        }

                        var path = System.Configuration.ConfigurationManager.AppSettings["GaleriaImagens"] + item.ID + "\\";

                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);


                        if (item.Imagens != null && item.Imagens.Count > 0)
                        {
                            foreach (var img in item.Imagens)
                            {
                                var fileName = path + string.Format("{0:fffssmmHHyyyyMMDD}", DateTime.Now) + ".jpg";

                                string imgPath = img;

                                imgPath = imgPath.Replace("data:image/png;base64,", "");
                                imgPath = imgPath.Replace("data:image/jpeg;base64,", "");

                                Helpers.Imagem.SalvarImagem(imgPath, fileName);
                            }
                        }

                        trans.Commit();
                    }
                    catch (DbEntityValidationException e)
                    {
                        string erros = string.Empty;

                        foreach (var eve in e.EntityValidationErrors)
                        {
                            foreach (var ve in eve.ValidationErrors)
                            {
                                erros += string.Format("{0}, ", ve.ErrorMessage);
                            }
                        }

                        erros = erros.Substring(0, erros.Length - 2);

                        throw new ArgumentException(erros);
                    }
                    catch (Exception ex)
                    {
                        trans.Dispose();
                        throw;
                    }
                }
            }
        }

        public void Update(Item item, bool apenasItem = false)
        {
            using (var ctx = new Context.Context())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        if (apenasItem)
                        {
                            ctx.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            ctx.SaveChanges();

                            trans.Commit();

                            return;
                        }
                        
                        var propriedadesValores = item.PropriedadesValores;
                        var camposExtras = item.CamposExtras;

                        item.PropriedadesValores = null;
                        item.CamposExtras = null;
                        item.Local = null;
                        item.Especie = null;

                        if (item.LocalID <= 0)
                            throw new ArgumentException("Favor informar um LocalID válido");

                        if (item.EspecieID <= 0)
                            throw new ArgumentException("Favor informar uma EspecieID válida");

                        ctx.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        ctx.SaveChanges();

                        if (propriedadesValores != null)
                        {
                            foreach (var pv in propriedadesValores)
                            {
                                if (pv.ID == 0)
                                {
                                    ctx.ItemPropriedadesValores.Add(new ItemPropriedadeValor()
                                    {
                                        ItemID = item.ID,
                                        PropriedadeID = pv.PropriedadeID,
                                        PropriedadeValorID = pv.PropriedadeValorID,
                                        Valor = pv.Valor
                                    });
                                }
                                else
                                {
                                    var itemPropriedadeValor = ctx.ItemPropriedadesValores.Find(pv.ID);

                                    itemPropriedadeValor.PropriedadeID = pv.PropriedadeID;
                                    itemPropriedadeValor.PropriedadeValorID = pv.PropriedadeValorID;
                                    itemPropriedadeValor.Valor = pv.Valor;

                                    ctx.Entry(itemPropriedadeValor).State = System.Data.Entity.EntityState.Modified;
                                }

                                ctx.SaveChanges();
                            }
                        }

                        if (camposExtras != null)
                        {
                            foreach (var ce in camposExtras)
                            {
                                if (ce.ID == 0)
                                {
                                    ctx.CamposExtras.Add(new CampoExtra()
                                    {
                                        ItemID = item.ID,
                                        Nome = ce.Nome,
                                        Valor = ce.Valor
                                    });
                                }
                                else
                                {
                                    var campoExtra = ctx.CamposExtras.Find(ce.ID);

                                    campoExtra.Nome = ce.Nome;
                                    campoExtra.Valor = ce.Valor;

                                    ctx.Entry(campoExtra).State = System.Data.Entity.EntityState.Modified;
                                }

                                ctx.SaveChanges();
                            }
                        }

                        trans.Commit();
                    }
                    catch (DbEntityValidationException e)
                    {
                        string erros = string.Empty;

                        foreach (var eve in e.EntityValidationErrors)
                        {
                            foreach (var ve in eve.ValidationErrors)
                            {
                                erros += string.Format("{0}, ", ve.ErrorMessage);
                            }
                        }

                        erros = erros.Substring(0, erros.Length - 2);

                        throw new ArgumentException(erros);
                    }
                    catch (Exception)
                    {
                        trans.Dispose();

                        throw;
                    }
                }
            }            
        }

        public void DeleteAll(int empresaID)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Database.ExecuteSqlCommand($"UPDATE ITEM SET DATAEXCLUSAO = GETDATE() WHERE DATAEXCLUSAO IS NULL AND EMPRESAID = {empresaID}");
                ctx.SaveChanges();
            }
        }

        public bool VerifyIfExists(int empresaID, int filialID, string codigo, int incorporacao)
        {
            bool itemExiste = false;

            using (var ctx = new Context.Context())
            {
                Empresa empresa = ctx.Empresas.Find(empresaID);

                List<Item> itens = ctx.Itens.Where(x => x.EmpresaID == empresaID).ToList();

                if (empresa.PermiteCodigosIguais == null || empresa.PermiteCodigosIguais ==  false)
                    itens = itens.Where(x => x.Codigo == codigo && x.Incorporacao == incorporacao).ToList();
                else
                    itens = itens.Where(x => x.Local.FilialID == filialID && x.Codigo == codigo && x.Incorporacao == incorporacao).ToList();

                itemExiste = itens.Count() > 0;
            }

            return itemExiste;
        }

        public Item GetByCodigo(int empresaID,string codigo, int incorporacao)
        {
            Item item = null;
            
            using (var ctx = new Context.Context())
            {
                item = ctx.Itens.Include("CamposExtras").Include("PropriedadesValores").Include("Especie").Include("Local.Filial").Where(x => x.EmpresaID == empresaID && x.Codigo == codigo && x.Incorporacao == incorporacao).FirstOrDefault();
            }

            if (item != null)
            {

                var path = System.Configuration.ConfigurationManager.AppSettings["GaleriaImagens"] + item.ID;

                var files = Directory.GetFiles(path);

                foreach (var file in files)
                {
                    FileInfo info = new FileInfo(file);

                    item.Imagens.Add(System.Configuration.ConfigurationManager.AppSettings["GaleriaImagensLogico"] + item.ID + "/" + info.Name);
                }
            }

            return item;
        }
    }
}
