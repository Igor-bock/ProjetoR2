using System.Xml.Serialization;

namespace Maui_Lib.Models;

public interface IRespostaWSDTO
{
    [XmlIgnore]
    public string ErroMensagem { get; set; }
    [XmlIgnore]
    public int? ErroCodigo { get; set; }
}