namespace rei_esperantolib.Utils;

public class Constantes
{
    public const string C_INSERCAO_POR_WS = "Insercao por WS";

    public const int C_CODIGO_PAIS_PY = 5860;

    public const int C_CODIGO_MOEDA_DOLAR = 11;//TODO: Esto se tiene que cambiar en ambiente productivo!

    public const string C_TABELA_CLIENTES = "public.clientes";
    public const string C_TABELA_TABELA_DE_PRECOS = "vendas.tabs_precos";
    public const string C_TABELA_TABELA_PRECOS_ITEMS = "vendas.tabs_precos_itens";
    public const string C_TABELA_FINANCEIRO_COTACOES = "financeiro.cotacoes";
    public const string C_TABELA_FUNCIONARIOS = "public.funcionarios";
    public const string C_TABELA_NEGOCIACOES = "vendas.negociacoes";
    public const string C_TABELA_CONTABIL_LANCAMENTOS = "contabil.lancamentos";
    public const string C_TABELA_VENDEDORES = "public.vendedores";
    public const string C_TABELA_ROTAS = "public.rotas";
    public const string C_TABELA_CIDADES = "public.cidades";
    public const string C_TABELA_ESTADOS = "public.estados";
    public const string C_TABELA_CONDICOES = "public.condicoes";
    public const string C_TABELA_CLASSES_CLIENTE = "public.clacli";
    public const string C_TABELA_CAD_TOP_GRUPOS = "cad.top_grupos";
    public const string C_TABELA_CAD_TOP_FERRAGENS = "cad.top_ferragens";
    public const string C_TABELA_CAD_TOP_BIBLIOTECA = "cad.top_biblioteca";
    public const string C_TABELA_PRODUTOS = "public.produtos";
    public const string C_TABELA_GRUPOS = "public.grupos";
    public const string C_TABELA_SUBGRUPOS = "public.subgrupos";
    public const string C_TABELA_TIPOS = "public.tipos";
    public const string C_TABELA_UNIDADE_MEDIDA = "public.unimed";
    public const string C_TABELA_CLIENTES_CONTATOS = "public.cli_contatos";
    public const string C_TABELA_CAD_WS_SYNC = "cad.ws_sync";
    public const string C_TABELA_FINANCEIRO_UNIDADES_COTACAO = "financeiro.unidades_cotacao";
    public const string C_TABELA_PAISES = "public.paises";
    public const string C_TABELA_FINANCEIRO_CONTAS_BANCARIAS = "financeiro.clientes_contas_bancarias";
    public const string C_CASHBACK = "cashback";
    public const string C_TABELA_PORTADORES = "public.portador";
    public const string C_TABELA_FORNEC_GRUPOS = "public.fornec_grupos";
    public const string C_TABELA_EMPRESA = "public.empresa";
    public const string C_TABELA_NOTAS = "public.notas";
    public const string C_TABELA_FISCAL_MODELOS = "fiscal.modelos";
    public const string C_TABELA_RECIBOS = "financeiro.recibos_cobrancas";
    public const string C_TABELA_ADIANTAMENTOS_COMPRAS = "compras.adiantamentos_compras";
    public const string C_OPERACAO_CONSULTA_CREDITO = "consulta_credito";
    public const string C_OPERACAO_ATUALIZA_ESTADO_PEDIDOS = "atualiza_estado_pedidos";
    public const string C_CONSULTA_PAGOS_RECIBIDOS = "consulta_pagamentos_recebidos";
    public const string C_CONSULTA_PAGOS_EFETUADOS = "consulta_pagamentos_efetuados";
    public const string C_CONSULTA_RECONCILIACOES = "consulta_reconciliacoes";
    public const string C_TABELA_SOLICITACOES_ADIANTAMENTOS = "financeiro.solicitacoes_adiantamentos";
    public const string C_TABELA_ORCAMENTO = "public.orcamento";
    public const string C_TABELA_TALONARIOS = "fiscal.talonarios";
    public const string C_LIMPA_REGISTROS_MAPEADOS_MAS_INEXISTENTES_EM_REIGLASS = "limpa_registros_inexistentes";
    public const string C_TABELA_RECEBER = "public.receber";
    public const string C_FINAN_PONTUA_RECEBER = "public.finan_pontua_receber";
    public const string C_FINAN_ESTORNO_CASHBACK = "public.finan_estorno_cashback";
    public const string C_CONSULTA_COTACOES = "consulta_cotacao";
    public const string C_TABELA_UNIMED = "public.unimed";
    public const string C_NAO_HA_AMBIENTE_PARA_ENTIDADE = "Não há ambiente para a entidade.";
    public const string C_UM_ERRO_ACONTECEU = "Um erro aconteceu:";
    public const string C_VERSAO_INCOMPATIVEL = "A versão atual do Reiglass é incompatível com o integrador Esperanto.";
    public const string C_ERRO_COM_A_CONEXAO = "Ocorreu um erro com a conexão e não foi possível conectar-se com a base de dados. Soluções: Verifique o host, a porta ou o database; Veja se a base de dados está na versão mínima ou superior;";
    public const string C_TABELA_NAO_ENCONTRADA = "A tabela \"{0}\" não foi encontrada, possivelmente a sua base não está configurada na versão correta.";
    public const string C_DATABASE_NAO_EXISTE = "A base de dados \"{0}\" não existe. Altere o host ou a porta.";
    public const string C_IMPOSSIVEL_CONECTAR_AO_HOST = "Não foi possível conectar ao host {0}. Altere e tente novamente.";
    public const string C_CONEXAO_ESTABELECIDA = "A conexão com a base de dados foi estabelecida com sucesso.";
    public const string C_CONEXAO_NAO_ESTABELECIDA = "A conexão com a base de dados não foi estabelecida. Corrija se for necessário e tente novamente.";
    public const string C_PROPRIEDADE_SALVA = "A propriedade {0} foi salva com sucesso.";
    public const string C_PROPRIEDADE_ERRO = "Um erro aconteceu e não foi possível alterar a propriedade {0}";
    public const string C_PROPRIEDADE_NAO_ALTERADA = "O valor da propriedade {0} não foi alterada.";
    public const string C_PROPRIEDADE_RESTAURADA = "A propriedade {0} foi restaurada com o valor {1}.";
    public const string C_PROPRIEDADE_VALOR_PADRAO = "A propriedade {0} já está com o valor padrão.";

    public const string C_VERSAO_MINIMA = "v22.07.01";

    // tarefa #31094
    //public const string BASE_URL_INTEGRA_CAD = "http://r2cad.vidriocar.com.py:83"; // TESTES
    //public const string BASE_URL_INTEGRA_CAD = "http://r2cad.vidriocar.com.py:81"; // Oficial
    public const string BASE_URL_INTEGRA_CAD = "http://10.0.0.40:5000/integra"; // TESTES ANTONI
    public const string C_TABELA_TRANSPORTADORA = "public.transport";
    public const string C_TABELA_EMBARQUES = "compras.embarques";

    public const string C_TABELA_TRANSFERENCIAS = "logistica.transferencias";
    public const string C_TABELA_TICKETS = "crm.tickets";
    public const string C_OBTEM_NOTIFICACAO_WHATSAPP = "obtem_notificacao_whatsapp";

    public const string C_TABELA_LOCAIS = "public.locais";
    public const string C_TABELA_ROMANEIOS = "public.romaneios";
    public const string C_TABELA_CONSULTA_MOVIMENTOS_ESTOQUE = "consulta_ajustes_estoque";

    public const string C_TABELA_SOLICITA_DEVOLUCAO_COMPRA = "solicita_devol_compra";
    public const string C_TABELA_SOLICITA_RECEPCAO_ROMANEIO = "solicita_recepcao_romaneio";

    public const string C_TABELA_COMPRAS_DEVOLUCOES = "compras.devolucoes";

    public const string C_ENDERECO_BASE_API = "http://localhost:51158";
    public const string C_ENDPOINT_CONFIGURACOES = "api/configuracoes";

    //public static string C_ENDERECO_BASE_IDENTITY = "https://localhost:9001";
    //public const string C_ENDERECO_BASE_IDENTITY = "https://9f15-179-109-192-138.sa.ngrok.io";
    public const string C_ENDERECO_BASE_IDENTITY = "https://d655-179-109-192-138.sa.ngrok.io/";//"https://localhost:5001";
    public const string C_ENDPOINT_USUARIOS = "api/usuario";
    public const string C_ENDPOINT_ROLES = "api/roles";

    public const string C_INTEGRADOR_R2 = "Integrador R2 para integração com serviços de terceiros.";
    public const string C_ENDPOINTS_SERVICOS = "Endpoints de integração com {0}";
    public const string C_ESPERANTO = "Esperanto";
    public const string C_FIDELIMAX = "Fidelimax";
    public const string C_SAP = "SAP";
    public const string C_CYGNUS = "CygnusWMS";
    public const string C_MIDIANFC = "MidiaNFC";
    public const string C_WHATSAPP = "Whatsapp";
    public const string C_EMAIL = "Email";
    public const string C_ENDERECO_SWAGGER = "/swagger/{0}/swagger.json";
}
