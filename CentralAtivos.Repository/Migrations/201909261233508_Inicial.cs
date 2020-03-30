namespace CentralAtivos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CampoExtra",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 20),
                        Valor = c.String(nullable: false, maxLength: 50),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Item", t => t.ItemID)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemEstadoID = c.Int(),
                        ResponsavelID = c.Int(),
                        LocalID = c.Int(nullable: false),
                        EmpresaID = c.Int(nullable: false),
                        EspecieID = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 30),
                        Codigo = c.String(nullable: false, maxLength: 20),
                        Incorporacao = c.Int(),
                        CodigoAnterior = c.String(maxLength: 20),
                        IncorporacaoAnterior = c.Int(),
                        CodigoPM = c.String(maxLength: 20),
                        DadosPM = c.String(maxLength: 50),
                        LocalPM = c.String(maxLength: 100),
                        Tag = c.String(maxLength: 50),
                        Marca = c.String(maxLength: 50),
                        Modelo = c.String(maxLength: 50),
                        NumeroSerie = c.String(maxLength: 50),
                        Observacao = c.String(maxLength: 150),
                        ImagemUrl = c.String(maxLength: 20),
                        Latitude = c.Decimal(precision: 9, scale: 6),
                        Longitude = c.Decimal(precision: 9, scale: 6),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .ForeignKey("dbo.Especie", t => t.EspecieID)
                .ForeignKey("dbo.ItemEstado", t => t.ItemEstadoID)
                .ForeignKey("dbo.Local", t => t.LocalID)
                .ForeignKey("dbo.Responsavel", t => t.ResponsavelID)
                .Index(t => t.ItemEstadoID)
                .Index(t => t.ResponsavelID)
                .Index(t => t.LocalID)
                .Index(t => t.EmpresaID)
                .Index(t => t.EspecieID);
            
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NomeFantasia = c.String(nullable: false, maxLength: 150),
                        RazaoSocial = c.String(nullable: false, maxLength: 250),
                        CNPJ = c.String(nullable: false, maxLength: 20),
                        CEP = c.String(nullable: false, maxLength: 9),
                        Endereco = c.String(nullable: false, maxLength: 150),
                        Numero = c.Int(),
                        Complemento = c.String(maxLength: 50),
                        Bairro = c.String(nullable: false, maxLength: 150),
                        Cidade = c.String(nullable: false, maxLength: 150),
                        UF = c.String(nullable: false, maxLength: 2),
                        Logo = c.String(),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Especie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GrupoID = c.Int(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 50),
                        Nome = c.String(nullable: false, maxLength: 150),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Grupo", t => t.GrupoID)
                .Index(t => t.GrupoID);
            
            CreateTable(
                "dbo.Grupo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmpresaID = c.Int(nullable: false),
                        ContaContabilID = c.Int(),
                        Codigo = c.String(nullable: false, maxLength: 50),
                        Nome = c.String(nullable: false, maxLength: 150),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaContabil", t => t.ContaContabilID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .Index(t => t.EmpresaID)
                .Index(t => t.ContaContabilID);
            
            CreateTable(
                "dbo.ContaContabil",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmpresaID = c.Int(nullable: false),
                        PaiID = c.Int(),
                        Codigo = c.String(nullable: false, maxLength: 50),
                        Nome = c.String(nullable: false, maxLength: 150),
                        NomeAbreviado = c.String(nullable: false, maxLength: 80),
                        CodigoInterno = c.Double(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .ForeignKey("dbo.ContaContabil", t => t.PaiID)
                .Index(t => t.EmpresaID)
                .Index(t => t.PaiID);
            
            CreateTable(
                "dbo.ItemEstado",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 20),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Local",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ResponsavelID = c.Int(),
                        CentroCustoID = c.Int(),
                        FilialID = c.Int(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 50),
                        Nome = c.String(nullable: false, maxLength: 150),
                        PaiID = c.Int(),
                        CodigoInterno = c.Double(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CentroCusto", t => t.CentroCustoID)
                .ForeignKey("dbo.Local", t => t.PaiID)
                .ForeignKey("dbo.Filial", t => t.FilialID)
                .ForeignKey("dbo.Responsavel", t => t.ResponsavelID)
                .Index(t => t.ResponsavelID)
                .Index(t => t.CentroCustoID)
                .Index(t => t.FilialID)
                .Index(t => t.PaiID);
            
            CreateTable(
                "dbo.CentroCusto",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmpresaID = c.Int(nullable: false),
                        PaiID = c.Int(),
                        Codigo = c.String(nullable: false, maxLength: 50),
                        Nome = c.String(nullable: false, maxLength: 150),
                        CodigoInterno = c.Double(nullable: false),
                        ResponsavelID = c.Int(),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .ForeignKey("dbo.CentroCusto", t => t.PaiID)
                .ForeignKey("dbo.Responsavel", t => t.ResponsavelID)
                .Index(t => t.EmpresaID)
                .Index(t => t.PaiID)
                .Index(t => t.ResponsavelID);
            
            CreateTable(
                "dbo.Responsavel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmpresaID = c.Int(nullable: false),
                        UsuarioID = c.Int(),
                        Nome = c.String(nullable: false, maxLength: 150),
                        Cargo = c.String(maxLength: 100),
                        Matricula = c.String(maxLength: 20),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.EmpresaID)
                .Index(t => t.UsuarioID);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmpresaID = c.Int(),
                        PerfilID = c.Int(nullable: false),
                        Matricula = c.String(maxLength: 50),
                        Nome = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 150),
                        Senha = c.String(),
                        PrimeiroAcesso = c.Boolean(nullable: false),
                        TokenSolicitacaoSenha = c.Guid(),
                        DataValidadeTokenSenha = c.DateTime(),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID)
                .Index(t => t.EmpresaID)
                .Index(t => t.PerfilID);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Filial",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmpresaID = c.Int(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 20),
                        Nome = c.String(nullable: false, maxLength: 100),
                        CNPJ = c.String(nullable: false, maxLength: 20),
                        CEP = c.String(nullable: false, maxLength: 9),
                        Endereco = c.String(nullable: false, maxLength: 150),
                        Numero = c.Int(),
                        Complemento = c.String(maxLength: 50),
                        Bairro = c.String(nullable: false, maxLength: 150),
                        Cidade = c.String(nullable: false, maxLength: 150),
                        UF = c.String(nullable: false, maxLength: 2),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .Index(t => t.EmpresaID);
            
            CreateTable(
                "dbo.ItemPropriedadeValor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        PropriedadeID = c.Int(nullable: false),
                        Valor = c.String(maxLength: 100),
                        PropriedadeValorID = c.Int(),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Item", t => t.ItemID)
                .ForeignKey("dbo.Propriedade", t => t.PropriedadeID)
                .ForeignKey("dbo.PropriedadeValor", t => t.PropriedadeValorID)
                .Index(t => t.ItemID)
                .Index(t => t.PropriedadeID)
                .Index(t => t.PropriedadeValorID);
            
            CreateTable(
                "dbo.Propriedade",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmpresaID = c.Int(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 50),
                        Nome = c.String(nullable: false, maxLength: 150),
                        Fixa = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .Index(t => t.EmpresaID);
            
            CreateTable(
                "dbo.PropriedadeValor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PropriedadeID = c.Int(nullable: false),
                        Valor = c.String(maxLength: 100),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Propriedade", t => t.PropriedadeID)
                .Index(t => t.PropriedadeID);
            
            CreateTable(
                "dbo.EntidadeCampo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NomeCampo = c.String(nullable: false, maxLength: 50),
                        Entidade = c.Int(nullable: false),
                        Tipo = c.Int(nullable: false),
                        Obrigatorio = c.Boolean(nullable: false),
                        Tamanho = c.Int(),
                        Regex = c.String(maxLength: 200),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EspeciePropriedade",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EspecieID = c.Int(nullable: false),
                        PropriedadeID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Especie", t => t.EspecieID)
                .ForeignKey("dbo.Propriedade", t => t.PropriedadeID)
                .Index(t => t.EspecieID)
                .Index(t => t.PropriedadeID);
            
            CreateTable(
                "dbo.Funcionalidade",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InventarioFilial",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InventarioID = c.Int(nullable: false),
                        FilialID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filial", t => t.FilialID)
                .ForeignKey("dbo.Inventario", t => t.InventarioID)
                .Index(t => t.InventarioID)
                .Index(t => t.FilialID);
            
            CreateTable(
                "dbo.Inventario",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false, maxLength: 50),
                        Nome = c.String(nullable: false, maxLength: 150),
                        StatusID = c.Int(nullable: false),
                        EmpresaID = c.Int(nullable: false),
                        Geral = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .ForeignKey("dbo.Status", t => t.StatusID)
                .Index(t => t.StatusID)
                .Index(t => t.EmpresaID);
            
            CreateTable(
                "dbo.InventarioLocal",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InventarioID = c.Int(nullable: false),
                        LocalID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Inventario", t => t.InventarioID)
                .ForeignKey("dbo.Local", t => t.LocalID)
                .Index(t => t.InventarioID)
                .Index(t => t.LocalID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InventarioUsuario",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InventarioID = c.Int(nullable: false),
                        UsuarioID = c.Int(nullable: false),
                        PerfilID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Inventario", t => t.InventarioID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.InventarioID)
                .Index(t => t.UsuarioID)
                .Index(t => t.PerfilID);
            
            CreateTable(
                "dbo.LogUsuarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UsuarioID = c.Int(nullable: false),
                        RequisicaoID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Requisicao", t => t.RequisicaoID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.UsuarioID)
                .Index(t => t.RequisicaoID);
            
            CreateTable(
                "dbo.Requisicao",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Entidade = c.String(nullable: false, maxLength: 50),
                        NomeMetodo = c.String(nullable: false, maxLength: 50),
                        MetodoHTTP = c.String(nullable: false, maxLength: 6),
                        Descricao = c.String(nullable: false, maxLength: 150),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 150),
                        Url = c.String(nullable: false, maxLength: 150),
                        Icone = c.String(maxLength: 50),
                        TituloMenu = c.String(nullable: false, maxLength: 50),
                        TipoMenu = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrdemServicoAnexo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrdemServicoID = c.Int(nullable: false),
                        NomeArquivo = c.String(nullable: false, maxLength: 150),
                        Imagem = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.OrdemServico", t => t.OrdemServicoID)
                .Index(t => t.OrdemServicoID);
            
            CreateTable(
                "dbo.OrdemServico",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmpresaID = c.Int(nullable: false),
                        OrdemServicoMotivoID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .ForeignKey("dbo.OrdemServicoMotivo", t => t.OrdemServicoMotivoID)
                .Index(t => t.EmpresaID)
                .Index(t => t.OrdemServicoMotivoID);
            
            CreateTable(
                "dbo.OrdemServicoMotivo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                        AcaoFinal = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrdemServicoCampo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NomeCampo = c.String(nullable: false, maxLength: 150),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrdemServicoItem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrdemServicoID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Item", t => t.ItemID)
                .ForeignKey("dbo.OrdemServico", t => t.OrdemServicoID)
                .Index(t => t.OrdemServicoID)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.OrdemServicoMotivoCampo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrdemServicoMotivoID = c.Int(nullable: false),
                        OrdemServicoCampoID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.OrdemServicoCampo", t => t.OrdemServicoCampoID)
                .ForeignKey("dbo.OrdemServicoMotivo", t => t.OrdemServicoMotivoID)
                .Index(t => t.OrdemServicoMotivoID)
                .Index(t => t.OrdemServicoCampoID);
            
            CreateTable(
                "dbo.OrdemServicoMotivoCampoValor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrdemServicoID = c.Int(nullable: false),
                        OrdemServicoMotivoCampoID = c.Int(nullable: false),
                        Valor = c.String(nullable: false, maxLength: 350),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.OrdemServico", t => t.OrdemServicoID)
                .ForeignKey("dbo.OrdemServicoMotivoCampo", t => t.OrdemServicoMotivoCampoID)
                .Index(t => t.OrdemServicoID)
                .Index(t => t.OrdemServicoMotivoCampoID);
            
            CreateTable(
                "dbo.PerfilMenu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PerfilID = c.Int(nullable: false),
                        MenuID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Menu", t => t.MenuID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID)
                .Index(t => t.PerfilID)
                .Index(t => t.MenuID);
            
            CreateTable(
                "dbo.Permissao",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PerfilID = c.Int(nullable: false),
                        FuncionalidadeID = c.Int(nullable: false),
                        Metodos = c.String(nullable: false, maxLength: 50),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Funcionalidade", t => t.FuncionalidadeID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID)
                .Index(t => t.PerfilID)
                .Index(t => t.FuncionalidadeID);
            
            CreateTable(
                "dbo.Sincronizacao",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InventarioID = c.Int(nullable: false),
                        DataEnvioArquivo = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        UsuarioEnvioID = c.Int(nullable: false),
                        UsuarioProcessamentoID = c.Int(),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Inventario", t => t.InventarioID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioEnvioID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioProcessamentoID)
                .Index(t => t.InventarioID)
                .Index(t => t.UsuarioEnvioID)
                .Index(t => t.UsuarioProcessamentoID);
            
            CreateTable(
                "dbo.Upload",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmpresaID = c.Int(nullable: false),
                        NomeArquivo = c.String(nullable: false, maxLength: 200),
                        UsuarioID = c.Int(nullable: false),
                        DataInicioProcessamento = c.DateTime(),
                        Entidade = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        NomeArquivoCriticas = c.String(maxLength: 200),
                        Observacoes = c.String(maxLength: 350),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.EmpresaID)
                .Index(t => t.UsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Upload", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.Upload", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.Sincronizacao", "UsuarioProcessamentoID", "dbo.Usuario");
            DropForeignKey("dbo.Sincronizacao", "UsuarioEnvioID", "dbo.Usuario");
            DropForeignKey("dbo.Sincronizacao", "InventarioID", "dbo.Inventario");
            DropForeignKey("dbo.Permissao", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.Permissao", "FuncionalidadeID", "dbo.Funcionalidade");
            DropForeignKey("dbo.PerfilMenu", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.PerfilMenu", "MenuID", "dbo.Menu");
            DropForeignKey("dbo.OrdemServicoMotivoCampoValor", "OrdemServicoMotivoCampoID", "dbo.OrdemServicoMotivoCampo");
            DropForeignKey("dbo.OrdemServicoMotivoCampoValor", "OrdemServicoID", "dbo.OrdemServico");
            DropForeignKey("dbo.OrdemServicoMotivoCampo", "OrdemServicoMotivoID", "dbo.OrdemServicoMotivo");
            DropForeignKey("dbo.OrdemServicoMotivoCampo", "OrdemServicoCampoID", "dbo.OrdemServicoCampo");
            DropForeignKey("dbo.OrdemServicoItem", "OrdemServicoID", "dbo.OrdemServico");
            DropForeignKey("dbo.OrdemServicoItem", "ItemID", "dbo.Item");
            DropForeignKey("dbo.OrdemServico", "OrdemServicoMotivoID", "dbo.OrdemServicoMotivo");
            DropForeignKey("dbo.OrdemServico", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.OrdemServicoAnexo", "OrdemServicoID", "dbo.OrdemServico");
            DropForeignKey("dbo.LogUsuarios", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.LogUsuarios", "RequisicaoID", "dbo.Requisicao");
            DropForeignKey("dbo.InventarioUsuario", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.InventarioUsuario", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.InventarioUsuario", "InventarioID", "dbo.Inventario");
            DropForeignKey("dbo.Inventario", "StatusID", "dbo.Status");
            DropForeignKey("dbo.InventarioLocal", "LocalID", "dbo.Local");
            DropForeignKey("dbo.InventarioLocal", "InventarioID", "dbo.Inventario");
            DropForeignKey("dbo.InventarioFilial", "InventarioID", "dbo.Inventario");
            DropForeignKey("dbo.Inventario", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.InventarioFilial", "FilialID", "dbo.Filial");
            DropForeignKey("dbo.EspeciePropriedade", "PropriedadeID", "dbo.Propriedade");
            DropForeignKey("dbo.EspeciePropriedade", "EspecieID", "dbo.Especie");
            DropForeignKey("dbo.Item", "ResponsavelID", "dbo.Responsavel");
            DropForeignKey("dbo.ItemPropriedadeValor", "PropriedadeValorID", "dbo.PropriedadeValor");
            DropForeignKey("dbo.ItemPropriedadeValor", "PropriedadeID", "dbo.Propriedade");
            DropForeignKey("dbo.PropriedadeValor", "PropriedadeID", "dbo.Propriedade");
            DropForeignKey("dbo.Propriedade", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.ItemPropriedadeValor", "ItemID", "dbo.Item");
            DropForeignKey("dbo.Item", "LocalID", "dbo.Local");
            DropForeignKey("dbo.Local", "ResponsavelID", "dbo.Responsavel");
            DropForeignKey("dbo.Local", "FilialID", "dbo.Filial");
            DropForeignKey("dbo.Filial", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.Local", "PaiID", "dbo.Local");
            DropForeignKey("dbo.Local", "CentroCustoID", "dbo.CentroCusto");
            DropForeignKey("dbo.CentroCusto", "ResponsavelID", "dbo.Responsavel");
            DropForeignKey("dbo.Responsavel", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.Usuario", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.Responsavel", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.CentroCusto", "PaiID", "dbo.CentroCusto");
            DropForeignKey("dbo.CentroCusto", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.Item", "ItemEstadoID", "dbo.ItemEstado");
            DropForeignKey("dbo.Item", "EspecieID", "dbo.Especie");
            DropForeignKey("dbo.Especie", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.Grupo", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.Grupo", "ContaContabilID", "dbo.ContaContabil");
            DropForeignKey("dbo.ContaContabil", "PaiID", "dbo.ContaContabil");
            DropForeignKey("dbo.ContaContabil", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.Item", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.CampoExtra", "ItemID", "dbo.Item");
            DropIndex("dbo.Upload", new[] { "UsuarioID" });
            DropIndex("dbo.Upload", new[] { "EmpresaID" });
            DropIndex("dbo.Sincronizacao", new[] { "UsuarioProcessamentoID" });
            DropIndex("dbo.Sincronizacao", new[] { "UsuarioEnvioID" });
            DropIndex("dbo.Sincronizacao", new[] { "InventarioID" });
            DropIndex("dbo.Permissao", new[] { "FuncionalidadeID" });
            DropIndex("dbo.Permissao", new[] { "PerfilID" });
            DropIndex("dbo.PerfilMenu", new[] { "MenuID" });
            DropIndex("dbo.PerfilMenu", new[] { "PerfilID" });
            DropIndex("dbo.OrdemServicoMotivoCampoValor", new[] { "OrdemServicoMotivoCampoID" });
            DropIndex("dbo.OrdemServicoMotivoCampoValor", new[] { "OrdemServicoID" });
            DropIndex("dbo.OrdemServicoMotivoCampo", new[] { "OrdemServicoCampoID" });
            DropIndex("dbo.OrdemServicoMotivoCampo", new[] { "OrdemServicoMotivoID" });
            DropIndex("dbo.OrdemServicoItem", new[] { "ItemID" });
            DropIndex("dbo.OrdemServicoItem", new[] { "OrdemServicoID" });
            DropIndex("dbo.OrdemServico", new[] { "OrdemServicoMotivoID" });
            DropIndex("dbo.OrdemServico", new[] { "EmpresaID" });
            DropIndex("dbo.OrdemServicoAnexo", new[] { "OrdemServicoID" });
            DropIndex("dbo.LogUsuarios", new[] { "RequisicaoID" });
            DropIndex("dbo.LogUsuarios", new[] { "UsuarioID" });
            DropIndex("dbo.InventarioUsuario", new[] { "PerfilID" });
            DropIndex("dbo.InventarioUsuario", new[] { "UsuarioID" });
            DropIndex("dbo.InventarioUsuario", new[] { "InventarioID" });
            DropIndex("dbo.InventarioLocal", new[] { "LocalID" });
            DropIndex("dbo.InventarioLocal", new[] { "InventarioID" });
            DropIndex("dbo.Inventario", new[] { "EmpresaID" });
            DropIndex("dbo.Inventario", new[] { "StatusID" });
            DropIndex("dbo.InventarioFilial", new[] { "FilialID" });
            DropIndex("dbo.InventarioFilial", new[] { "InventarioID" });
            DropIndex("dbo.EspeciePropriedade", new[] { "PropriedadeID" });
            DropIndex("dbo.EspeciePropriedade", new[] { "EspecieID" });
            DropIndex("dbo.PropriedadeValor", new[] { "PropriedadeID" });
            DropIndex("dbo.Propriedade", new[] { "EmpresaID" });
            DropIndex("dbo.ItemPropriedadeValor", new[] { "PropriedadeValorID" });
            DropIndex("dbo.ItemPropriedadeValor", new[] { "PropriedadeID" });
            DropIndex("dbo.ItemPropriedadeValor", new[] { "ItemID" });
            DropIndex("dbo.Filial", new[] { "EmpresaID" });
            DropIndex("dbo.Usuario", new[] { "PerfilID" });
            DropIndex("dbo.Usuario", new[] { "EmpresaID" });
            DropIndex("dbo.Responsavel", new[] { "UsuarioID" });
            DropIndex("dbo.Responsavel", new[] { "EmpresaID" });
            DropIndex("dbo.CentroCusto", new[] { "ResponsavelID" });
            DropIndex("dbo.CentroCusto", new[] { "PaiID" });
            DropIndex("dbo.CentroCusto", new[] { "EmpresaID" });
            DropIndex("dbo.Local", new[] { "PaiID" });
            DropIndex("dbo.Local", new[] { "FilialID" });
            DropIndex("dbo.Local", new[] { "CentroCustoID" });
            DropIndex("dbo.Local", new[] { "ResponsavelID" });
            DropIndex("dbo.ContaContabil", new[] { "PaiID" });
            DropIndex("dbo.ContaContabil", new[] { "EmpresaID" });
            DropIndex("dbo.Grupo", new[] { "ContaContabilID" });
            DropIndex("dbo.Grupo", new[] { "EmpresaID" });
            DropIndex("dbo.Especie", new[] { "GrupoID" });
            DropIndex("dbo.Item", new[] { "EspecieID" });
            DropIndex("dbo.Item", new[] { "EmpresaID" });
            DropIndex("dbo.Item", new[] { "LocalID" });
            DropIndex("dbo.Item", new[] { "ResponsavelID" });
            DropIndex("dbo.Item", new[] { "ItemEstadoID" });
            DropIndex("dbo.CampoExtra", new[] { "ItemID" });
            DropTable("dbo.Upload");
            DropTable("dbo.Sincronizacao");
            DropTable("dbo.Permissao");
            DropTable("dbo.PerfilMenu");
            DropTable("dbo.OrdemServicoMotivoCampoValor");
            DropTable("dbo.OrdemServicoMotivoCampo");
            DropTable("dbo.OrdemServicoItem");
            DropTable("dbo.OrdemServicoCampo");
            DropTable("dbo.OrdemServicoMotivo");
            DropTable("dbo.OrdemServico");
            DropTable("dbo.OrdemServicoAnexo");
            DropTable("dbo.Menu");
            DropTable("dbo.Requisicao");
            DropTable("dbo.LogUsuarios");
            DropTable("dbo.InventarioUsuario");
            DropTable("dbo.Status");
            DropTable("dbo.InventarioLocal");
            DropTable("dbo.Inventario");
            DropTable("dbo.InventarioFilial");
            DropTable("dbo.Funcionalidade");
            DropTable("dbo.EspeciePropriedade");
            DropTable("dbo.EntidadeCampo");
            DropTable("dbo.PropriedadeValor");
            DropTable("dbo.Propriedade");
            DropTable("dbo.ItemPropriedadeValor");
            DropTable("dbo.Filial");
            DropTable("dbo.Perfil");
            DropTable("dbo.Usuario");
            DropTable("dbo.Responsavel");
            DropTable("dbo.CentroCusto");
            DropTable("dbo.Local");
            DropTable("dbo.ItemEstado");
            DropTable("dbo.ContaContabil");
            DropTable("dbo.Grupo");
            DropTable("dbo.Especie");
            DropTable("dbo.Empresa");
            DropTable("dbo.Item");
            DropTable("dbo.CampoExtra");
        }
    }
}
