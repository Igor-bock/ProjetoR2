using System.Xml.Serialization;

namespace rei_logicaRazor.Models;

public interface IRespostaWSDTO
{
    [XmlIgnore]
    public string ErroMensagem { get; set; }
    [XmlIgnore]
    public int? ErroCodigo { get; set; }
}