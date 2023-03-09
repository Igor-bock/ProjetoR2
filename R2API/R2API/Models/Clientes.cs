using System.Xml.Serialization;

namespace R2API.Models;

[XmlRoot(ElementName = "Clientes", Namespace = "http://tempuri.org/")]
public class Clientes : XmlAttributesSerializer
{
    [XmlElement(ElementName = "Cliente")]
    public List<Cliente> Cliente { get; set; }

    [XmlText]
    public string Text { get; set; }
}
