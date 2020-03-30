namespace CentralAtivos.Domain.Entities
{
    public class Enums
    {
        public enum TipoItem
        {
            VARCHAR = 1, DECIMAL = 2, INT = 3, BOOLEAN = 4, DATETIME = 5
        }

        public enum Entidade
        {
            FILIAL = 1, GRUPO = 2, PROPRIEDADE = 3, RESPONSAVEL = 4, ITEM = 5, ESPECIE = 6, LOCAL = 7, CENTRO_CUSTO = 8, CONTA_CONTABIL = 9
        }

        public enum StatusUpload
        {
            EM_FILA_DE_PROCESSAMENTO = 1, PROCESSANDO = 2, PROCESSADO_OK = 3, PROCESSADO_COM_CRITICAS = 4, ERRO = 5
        }

        public enum StatusSincronizacao
        {
            NOVA = 1, PROCESSANDO = 2, PROCESSADO_OK = 3, CRITICAS = 4, ERRO = 5
        }

        public enum StatusOrdemServico
        {
            NOVO = 1, APROVADO = 2, REPROVADO = 3
        }

        public enum AcaoFinalOrdemServico
        {
            ATUALIZAR_LOCAL_DESTINO = 1, EXCLUIR_ITEM = 2, NENHUMA_ACAO = 3
        }

        public enum TipoMenu
        {
            ADMINISTRADOR = 1, EMPRESA = 2
        }

        public enum StatusItem
        {
            NAO_INVENTARIADO = 1, INSERIDO = 2, REVISADO = 3, ALTERADO = 4, BAIXADO = 5
        }

        public enum AppID
        {
            WEB = 1, MOBILE = 2
        }
    }
}
