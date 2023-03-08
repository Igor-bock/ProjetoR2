using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorIdentity.Services;

public static class TokenServiceStatic
{
    private static string c_Chave = "fedaf7d8863b48e197b9287d492b708e";
    public static string c_Token;

    public static string CM_GerarToken(string p_nome, string p_role)
    {
        if (string.IsNullOrWhiteSpace(p_role))
            throw new Exception();

        if(string.IsNullOrWhiteSpace(p_role))
            throw new Exception();

        var m_tokenHandler = new JwtSecurityTokenHandler();
        var m_segredo = Encoding.ASCII.GetBytes(c_Chave);
        var m_tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, p_nome.ToString()),
                    new Claim(ClaimTypes.Role, p_role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(m_segredo), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = m_tokenHandler.CreateToken(m_tokenDescriptor);
        c_Token = m_tokenHandler.WriteToken(token);
        return m_tokenHandler.WriteToken(token);
    }
}
