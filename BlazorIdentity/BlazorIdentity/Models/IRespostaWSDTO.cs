using System.Xml.Serialization;

namespace BlazorIdentity.Models;

public interface IRespostaWSDTO
{
    [XmlIgnore]
    public string ErroMensagem { get; set; }
    [XmlIgnore]
    public int? ErroCodigo { get; set; }
}