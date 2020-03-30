using CentralAtivos.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CentralAtivos.Repository.Context
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=conn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                        .Property(p => p.Latitude)
                        .HasPrecision(9, 6);

            modelBuilder.Entity<Item>()
                        .Property(p => p.Longitude)
                        .HasPrecision(9, 6);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<CampoExtra> CamposExtras { get; set; }
        public DbSet<CentroCusto> CentrosCusto { get; set; }
        public DbSet<Coletor> Coletores { get; set; }
        public DbSet<ContaContabil> ContasContabeis { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EntidadeCampo> EntidadeCampos { get; set; }
        public DbSet<Especie> Especies { get; set; }
        public DbSet<EspeciePropriedade> EspeciePropriedades { get; set; }        
        public DbSet<Filial> Filiais { get; set; }
        public DbSet<Funcionalidade> Funcionalidades { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<InventarioConfig> InventarioConfigs { get; set; }
        public DbSet<InventarioFilial> InventarioFiliais { get; set; }
        public DbSet<InventarioItem> InventarioItens { get; set; }
        public DbSet<InventarioLocal> InventarioLocais { get; set; }
        public DbSet<InventarioUsuario> InventarioUsuarios { get; set; }
        public DbSet<Item> Itens { get; set; }
        public DbSet<ItemEstado> ItemEstados { get; set; }
        public DbSet<ItemPropriedadeValor> ItemPropriedadesValores { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<LogUsuario> LogsUsuarios { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<OrdemServico> OrdensServico { get; set; }
        public DbSet<OrdemServicoAnexo> OrdemServicoAnexos { get; set; }
        public DbSet<OrdemServicoCampo> OrdemServicoCampos { get; set; }
        public DbSet<OrdemServicoItem> OrdemServicoItens { get; set; }
        public DbSet<OrdemServicoMotivo> OrdemServicoMotivos { get; set; }
        public DbSet<OrdemServicoMotivoCampo> OrdemServicoMotivoCampos { get; set; }
        public DbSet<OrdemServicoMotivoCampoValor> OrdemServicoMotivoCampoValores { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<PerfilMenu> PerfilMenus { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }
        public DbSet<Placa> Placas { get; set; }
        public DbSet<PlacaGrupo> PlacasGrupo { get; set; }
        public DbSet<Propriedade> Propriedades { get; set; }
        public DbSet<PropriedadeValor> PropriedadeValores { get; set; }
        public DbSet<Requisicao> Requisicoes { get; set; }
        public DbSet<Responsavel> Responsaveis { get; set; }
        public DbSet<Sincronizacao> Sincronizacoes { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Upload> Uploads { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}