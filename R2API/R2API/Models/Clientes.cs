using System.Xml.Serialization;

namespace R2API.Models;

[XmlRoot(ElementName = "Clientes", Namespace = "http://tempuri.org/")]
public class Clientes
{
    [XmlElement(ElementName = "Cliente")]
    public List<Cliente> Cliente { get; set; }

    [XmlAttribute(AttributeName = "xsd")]
    public string Xsd { get; set; }

    [XmlAttribute(AttributeName = "xsi")]
    public string Xsi { get; set; }

    [XmlAttribute(AttributeName = "xmlns")]
    public string Xmlns { get; set; }

    [XmlText]
    public string Text { get; set; }
}
