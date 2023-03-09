using System.Xml.Serialization;

namespace R2API.Models;

public class XmlAttributesSerializer
{
    [XmlAttribute(AttributeName = "xsd")]
    public string Xsd { get; set; }

    [XmlAttribute(AttributeName = "xsi")]
    public string Xsi { get; set; }

    [XmlAttribute(AttributeName = "xmlns")]
    public string Xmlns { get; set; }
}
