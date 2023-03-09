using System.Xml.Serialization;

namespace R2API.Models;

[XmlRoot(ElementName = "etiquetaDTO", Namespace = "http://tempuri.org/")]
public class ObterEtiquetaDTO : XmlAttributesSerializer
{
    public int p_usuario { get; set; }
    public int p_empresa { get; set; }
    public int p_codigo_etiqueta { get; set; }
}
