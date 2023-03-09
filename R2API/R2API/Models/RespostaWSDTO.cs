using System.Xml.Serialization;

namespace R2API.Models;

public interface RespostaWSDTO
{
    [XmlIgnore]
    public string ErroMensagem { get; set; }
    [XmlIgnore]
    public int? ErroCodigo { get; set; }
}