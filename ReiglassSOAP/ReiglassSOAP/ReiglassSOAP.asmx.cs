//using rei_bll.Base;
//using rei_bll.Tipos;
using rei_ef.Entity;
using ReiglassSOAP.Extension;
using ReiglassSOAP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web.Services;
using System.Xml.Serialization;

namespace ReiglassSOAP
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // [System.Web.Script.Services.ScriptService]
    public class ReiglassSOAP : WebService
    {
        private Dictionary<int, string> c_Imagens = new Dictionary<int, string>
        {
            {0, "https://img.freepik.com/fotos-gratis/banner-de-folha-monstera-tropical-neon_53876-138943.jpg?size=626&ext=jpg&ga=GA1.1.834137946.1656873988"},
            {1, "https://img.freepik.com/fotos-gratis/closeup-lindas-folhas-verdes_23-2148245094.jpg?size=626&ext=jpg&ga=GA1.1.834137946.1656873988"},
            {2, "https://img.freepik.com/vetores-gratis/ilustracao-do-conceito-de-upload-de-imagem_23-2148281227.jpg?size=626&ext=jpg&ga=GA1.1.834137946.1656873988"},
            {3, "https://img.freepik.com/fotos-gratis/astronauta-com-a-ferramenta-pencil-pen-tool-criou-um-caminho-de-recorte-incluido-no-jpeg-facil-de-compor_460848-11506.jpg?size=626&ext=jpg&ga=GA1.1.834137946.1656873988"},
            {4, "https://img.freepik.com/fotos-gratis/homem-apresentando-algo_1368-3934.jpg?size=338&ext=jpg&ga=GA1.1.834137946.1656873988"},
            {5, "https://img.freepik.com/fotos-gratis/ferramenta-de-caneta-de-estetoscopio-medico-de-renderizacao-3d-criada-caminho-de-recorte-incluido-em-jpeg-facil-de-compor_460848-12249.jpg?size=626&ext=jpg&ga=GA1.2.834137946.1656873988"},
            {6, "https://img.freepik.com/fotos-gratis/3d-render-pencil-question-mark-pen-tool-criado-clipping-path-incluido-no-jpeg-facil-de-compor_460848-11187.jpg?size=626&ext=jpg&ga=GA1.2.834137946.1656873988"},
            {7, "https://img.freepik.com/fotos-gratis/renderizacao-3d-da-letra-de-fonte-s-de-lapis-curvado-ferramenta-de-caneta-criada-trajeto-de-recorte-incluido-no-jpeg-facil-de-compor_460848-7155.jpg?size=626&ext=jpg&ga=GA1.2.834137946.1656873988"},
            {8, "https://www.infowester.com/img_art/form_img/jpegcomp1.jpg"},
            {9, "https://www.infowester.com/img_art/form_img/compjpeg.jpg"}
        };

        [WebMethod]
        [return: XmlRoot(ElementName = "Clientes")]
        public List<Cliente> CM_ObterClientes()
        {
            using (var m_modelo = new Modelo())
            {
                var m_clientes = m_modelo.clientes.Take(1000).ToList();
                var m_clientesDTO = new List<Cliente>();

                var m_random = new Random();

                foreach (var cliente in m_clientes)
                {
                    var m_sorteio = m_random.Next(10);
                    var m_novoCliente = new Cliente
                    {
                        cd_codigo = cliente.cd_codigo,
                        ds_bairro = cliente.ds_bairro1,
                        ds_cep = cliente.ds_cep1,
                        ds_cidade = cliente.ds_cidade1,
                        ds_email = cliente.ds_email,
                        ds_endereco = cliente.ds_endereco1,
                        ds_fone = cliente.ds_fone1,
                        ds_imagem = c_Imagens.Where(a => a.Key == m_sorteio).Select(a => a.Value).First(),
                        ds_nome = cliente.ds_nome
                    };
                    m_clientesDTO.Add(m_novoCliente);
                }
                return m_clientesDTO;
            }
        }

        [WebMethod]
        [return: XmlRoot(ElementName = "Clientes")]
        public List<Cliente> CM_ObterClientePorNome(string ds_nome)
        {
            using (var m_modelo = new Modelo())
            {
                var m_clientes = m_modelo.clientes.Where(a => a.ds_nome.ToUpper().Contains(ds_nome.ToUpper())).Take(10).ToList();
                var m_clientesDTO = new List<Cliente>();

                var m_random = new Random();
                var m_numerosSorteados = new List<int>();

                foreach (var cliente in m_clientes)
                {
                    var m_sorteio = m_random.Next(10);
                    while (m_numerosSorteados.Contains(m_sorteio))
                        m_sorteio = m_random.Next(10);
                    m_numerosSorteados.Add(m_sorteio);
                    var m_novoCliente = new Cliente
                    {
                        cd_codigo = cliente.cd_codigo,
                        ds_bairro = cliente.ds_bairro1,
                        ds_cep = cliente.ds_cep1,
                        ds_cidade = cliente.ds_cidade1,
                        ds_email = cliente.ds_email,
                        ds_endereco = cliente.ds_endereco1,
                        ds_fone = cliente.ds_fone1,
                        ds_imagem = c_Imagens.Where(a => a.Key == m_sorteio).Select(a => a.Value).First(),
                        ds_nome = cliente.ds_nome
                    };
                    m_clientesDTO.Add(m_novoCliente);
                }
                return m_clientesDTO;
            }
        }

        [WebMethod]
        public Cliente CM_SalvarCliente(string ds_xml, string metodo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ds_xml))
                    return null;

                Cliente m_cliente = null;
                using (var m_reader = new StringReader(ds_xml))
                {
                    m_cliente = (Cliente)new XmlSerializer(typeof(Cliente)).Deserialize(m_reader);
                }

                clientes m_clienteEditarOuAdicionar = new clientes();
                using (var m_modelo = new Modelo())
                {
                    if (metodo == "EDITAR")
                    {
                        m_clienteEditarOuAdicionar = m_modelo.clientes.Find(m_cliente.cd_codigo);
                    }
                    else
                    {
                        m_modelo.clientes.Add(m_clienteEditarOuAdicionar);
                    }

                    m_clienteEditarOuAdicionar.ds_nome = m_cliente.ds_nome;
                    m_clienteEditarOuAdicionar.ds_fone1 = m_cliente.ds_fone;
                    m_clienteEditarOuAdicionar.ds_endereco1 = m_cliente.ds_endereco;
                    m_clienteEditarOuAdicionar.ds_endereco2 = m_cliente.ds_endereco;
                    m_clienteEditarOuAdicionar.ds_bairro1 = m_cliente.ds_bairro;
                    m_clienteEditarOuAdicionar.ds_bairro2 = m_cliente.ds_bairro;
                    m_clienteEditarOuAdicionar.ds_cidade1 = m_cliente.ds_cidade;
                    m_clienteEditarOuAdicionar.ds_cidade2 = m_cliente.ds_cidade;
                    m_clienteEditarOuAdicionar.ds_cep1 = m_cliente.ds_cep;
                    m_clienteEditarOuAdicionar.ds_cep2 = m_cliente.ds_cep;
                    m_clienteEditarOuAdicionar.ds_email = m_cliente.ds_email;
                    m_clienteEditarOuAdicionar.st_cliente = true;
                    m_clienteEditarOuAdicionar.cd_estado1 = "RS";
                    m_clienteEditarOuAdicionar.cd_estado2 = "RS";
                    m_clienteEditarOuAdicionar.cd_cond = 1;
                    m_clienteEditarOuAdicionar.cd_moeda_padrao = 1;
                    m_clienteEditarOuAdicionar.cd_cadastrante = 2;
                    m_clienteEditarOuAdicionar.ds_site = "www.xvidros.com.br";
                    m_clienteEditarOuAdicionar.ds_comple1 = "Casa";
                    m_clienteEditarOuAdicionar.ds_comple2 = "Casa";
                    m_clienteEditarOuAdicionar.cd_tributa = 1;
                    m_clienteEditarOuAdicionar.tipo_cliente = 1;
                    m_clienteEditarOuAdicionar.cd_pis_cofins = 1;
                    m_clienteEditarOuAdicionar.cd_crt = 1;
                    m_clienteEditarOuAdicionar.cd_moeda_padrao = 1;
                    m_clienteEditarOuAdicionar.cd_tipo_documento = 1;
                    m_clienteEditarOuAdicionar.vl_perc_retencao = 1;
                    m_clienteEditarOuAdicionar.cd_tipo_contribuinte = 1;
                    m_clienteEditarOuAdicionar.vl_perc_juros = 1;

                    //str_sql_nao_foi_possivel_completar_op
                    m_modelo.SaveChanges(p_alertarMudancasParaTratadorDeEventos: false);

                    return m_cliente;
                }
            }
            catch (DbUpdateException db_ex)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        [WebMethod]
        public void CM_ApagarCliente(int cd_codigo)
        {
            try
            {
                using (var m_modelo = new Modelo())
                {
                    var m_clienteParaApagar = m_modelo.clientes.Find(cd_codigo);
                    m_modelo.clientes.Remove(m_clienteParaApagar);

                    m_modelo.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public string HelloWorld()
        {
            rei_bll.Base.Contexto.OperarEmModoApi = true;
            var m_Contexto = rei_bll.Base.Contexto.Factory(1, 2);

            return "Olá, Mundo";
        }

        [WebMethod]
        public string HelloWorld22()
        {
            rei_bll.Base.Contexto.OperarEmModoApi = true;
            var m_Contexto = rei_bll.Base.Contexto.Factory(1, 2);

            return "Olá, Mundo";
        }

        [WebMethod]
        public etiquetaDTO CM_ObterEtiqueta(int p_usuario, int p_empresa, int p_codigo_etiqueta)
        {
            try
            {
                rei_bll.Base.Contexto.OperarEmModoApi = true;
                var m_Contexto = rei_bll.Base.Contexto.Factory(p_empresa, p_usuario);

                var m_modelo = m_Contexto.ModeloParaConsultas;

                var m_fonteDados = m_modelo.etiquetas.Where(a => a.id == p_codigo_etiqueta );
                var m_clientes = m_modelo.clientes;
                var m_etiquetas = (from eti in m_fonteDados
                                   join cli in m_clientes on eti.cd_cliente equals cli.cd_codigo into clientes
                                   join setor in m_modelo.setores on eti.status equals setor.id into setores
                                   from set in setores.DefaultIfEmpty()
                                   from cliente in clientes.DefaultIfEmpty()
                                   select new etiquetaDTO()
                                   {
                                       cd_codigo = eti.id,
                                       ds_cliente = cliente.cd_codigo + "-" + cliente.ds_nome,
                                       ds_imagem = eti.ds_img,
                                       ds_setor = eti.status + "-" + set.ds_nomes,
                                       vl_m2 = eti.total_m2

                                   }).FirstOrDefault();
                return m_etiquetas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public etiquetaDTO CM_AtualizarStatus(int p_usuario, int p_empresa, int p_codigo_etiqueta, int p_status)
        {
            try
            {
                rei_bll.Base.Contexto.OperarEmModoApi = true;
                var m_Contexto = rei_bll.Base.Contexto.Factory(p_empresa, p_usuario);

                using (var m_Conexao = m_Contexto.CM_NovaConexao())
                using (var m_TransacaoUniConnection = m_Conexao.BeginTransaction())
                {
                    var m_CodigoDoRomaneio = m_Contexto.ModeloParaConsultas.etiquetas.Find(p_codigo_etiqueta).romaneio;

                    var m_Evento = new rei_bll.Eventos.EV_EtiquetaSolicitaMoverParaSetor(
                        this,
                        p_codigo_etiqueta,
                        m_CodigoDoRomaneio.GetValueOrDefault(),
                        p_status,
                        m_Contexto,
                        m_Conexao
                        );
                    rei_bll.Kernel.CM_DispararEvento(m_Evento);
                    m_TransacaoUniConnection.Commit();
                }

                return CM_ObterEtiqueta(p_usuario, p_empresa, p_codigo_etiqueta);
            }
            catch (Exception ex)
            {
                var m_Retorno = new etiquetaDTO();
                m_Retorno.CMX_DefinirException(ex);
                return m_Retorno;
            }
        }
    }
}
