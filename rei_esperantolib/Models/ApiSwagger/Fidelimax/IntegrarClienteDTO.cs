namespace rei_esperantolib.Models.ApiSwagger.Fidelimax;

public class IntegrarClienteDTO : IClienteDTO
{
    [System.Text.Json.Serialization.JsonPropertyName("Código")]
    public int cd_codigo { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("CNPJ")]
    public string ds_cnpj { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("CPF")]
    public string ds_cpf { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("Data de Nascimento")]
    public DateTime? dt_nascimento { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("Nome")]
    public string ds_nome { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("Telefone")]
    public string ds_fone1 { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("Email")]
    public string ds_email { get; set; }
}
