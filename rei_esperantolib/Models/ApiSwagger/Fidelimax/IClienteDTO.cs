namespace rei_esperantolib.Models.ApiSwagger.Fidelimax;

public interface IClienteDTO
{
    [System.Text.Json.Serialization.JsonPropertyName("Código")]
    public int cd_codigo { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("CNPJ")]
    public string ds_cnpj { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("CPF")]
    public string ds_cpf { get; set; }
}
