using System.Net.Sockets;

namespace rei_esperantolib.Models.Email;

public class EnvioEmail : IEnvioEmail
{
    private readonly Email _configuracaoEmail;

    public EnvioEmail(Email p_configuracaoEmail)
    {
        _configuracaoEmail = p_configuracaoEmail;
    }

    public async Task CM_PreparacaoEEnvioDeEmailAsync(Mensagem p_mensagem)
    {
        var m_mensagemEmail = CM_CriarMensagemDeEmail(p_mensagem);

        await CM_EnviarMensagemAsync(m_mensagemEmail);
    }

    public async Task CM_EnviarMensagemAsync(MimeMessage p_mensagemEmail)
    {
        using var m_client = new SmtpClient();

        try
        {
            await m_client.ConnectAsync(_configuracaoEmail.C_SmtpServer, _configuracaoEmail.C_Porta, true);
            m_client.AuthenticationMechanisms.Remove("XOAUTH2");
            await m_client.AuthenticateAsync(_configuracaoEmail.C_Usuario, _configuracaoEmail.C_Senha);

            await m_client.SendAsync(p_mensagemEmail);
        }
        catch(SocketException)
        {
            throw new Exception($"O host {_configuracaoEmail.C_SmtpServer} - Porta: {_configuracaoEmail.C_Porta} é inválido.");
        }
        catch(MailKit.Security.AuthenticationException m_auth)
        {
            throw new Exception($"Um erro aconteceu com a conexão ou autenticação: {m_auth.Message}");
        }
        catch (Exception m_exception)
        {
            throw new Exception(m_exception.Message);
        }
        finally
        {
            await m_client.DisconnectAsync(true);
            m_client.Dispose();
        }
    }

    public MimeMessage CM_CriarMensagemDeEmail(Mensagem p_mensagem)
    {
        var m_listaDestinatarios = new List<MailboxAddress>();
        foreach(var m_destinatario in p_mensagem.C_Destinatarios)
            m_listaDestinatarios.Add(new MailboxAddress(m_destinatario, m_destinatario));

        var m_mensagemEmail = new MimeMessage();
        m_mensagemEmail.From.Add(new MailboxAddress(_configuracaoEmail.C_Nome, _configuracaoEmail.C_Remetente));
        m_mensagemEmail.To.AddRange(m_listaDestinatarios);
        m_mensagemEmail.Subject = p_mensagem.C_Titulo;

        var m_conteudoNoCorpo = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2>", p_mensagem.C_Conteudo) };

        if(p_mensagem.C_Anexos != null && p_mensagem.C_Anexos.Any())
        {
            byte[] m_arquivoEmBytes;
            foreach(var m_anexo in p_mensagem.C_Anexos)
            {
                using var m_stream = new MemoryStream();
                m_anexo.CopyTo(m_stream);
                m_arquivoEmBytes = m_stream.ToArray();

                m_conteudoNoCorpo.Attachments.Add(m_anexo.FileName, m_arquivoEmBytes, ContentType.Parse(m_anexo.ContentType));
            }        
        }

        m_mensagemEmail.Body = m_conteudoNoCorpo.ToMessageBody();
        return m_mensagemEmail;
    }
}
