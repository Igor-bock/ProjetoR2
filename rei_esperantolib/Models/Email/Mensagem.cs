namespace rei_esperantolib.Models.Email;

public class Mensagem
{
    public string[] C_Destinatarios { get; set; }
    public string C_Titulo { get; set; }
    public string C_Conteudo { get; set; }
    public IFormFileCollection C_Anexos { get; set; }

    public Mensagem(string[] p_para, string p_subject, string p_conteudo, IFormFileCollection p_anexos)
    {
        C_Destinatarios = p_para;
        C_Titulo = p_subject;
        C_Conteudo = p_conteudo;
        C_Anexos = p_anexos;
    }
    public Mensagem() { }
}
