using System.Xml.Serialization;

namespace ReiglassSOAP.Models
{
    [XmlRoot(ElementName = "Cliente", Namespace = "http://tempuri.org/")]
    public class Cliente
    {
        public int cd_codigo { get; set; }
        public string ds_nome { get; set; }
        public string ds_fone { get; set; }
        public string ds_email { get; set; }
        public string ds_bairro { get; set; }
        public string ds_cidade { get; set; }
        public string ds_cep { get; set; }
        public string ds_endereco { get; set; }
        public string ds_imagem { get; set; }
    }
}