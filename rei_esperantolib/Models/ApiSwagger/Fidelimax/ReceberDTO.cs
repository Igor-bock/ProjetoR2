namespace rei_esperantolib.Models.ApiSwagger.Fidelimax;

public class ReceberDTO
{
    [System.Text.Json.Serialization.JsonPropertyName("Código")]
    public int cd_codigo { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("Código Cliente")]
    public int? cd_cliente { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("Valor")]
    public decimal? vl_valor { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("Valor Pago")]
    public decimal? vl_pago { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("Observação")]
    public string obs { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("Situação")]
    public int? situacao { get; set; }
}
