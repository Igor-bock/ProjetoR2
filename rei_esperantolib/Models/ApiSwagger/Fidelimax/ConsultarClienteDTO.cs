namespace rei_esperantolib.Models.ApiSwagger.Fidelimax;

public class ConsultarClienteDTO : IClienteDTO
{
    [System.Text.Json.Serialization.JsonPropertyName("CPF")]
    public string ds_cpf { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("CNPJ")]
    public string ds_cnpj { get; set; }

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Always)]
    public int cd_codigo { get; set; }
}
