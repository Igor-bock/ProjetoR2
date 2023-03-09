namespace rei_esperantolib.Extensions;

public static class SystemStringExtensions
{
    public static string CMX_LimitarCaracteres(this string p_String, int p_numeroCaracteres)
    {
        p_String = string.IsNullOrEmpty(p_String) ? string.Empty : p_String.Trim();

        if (p_numeroCaracteres > 0 && p_String.Length > p_numeroCaracteres)
            return p_String.Substring(0, p_numeroCaracteres);
        return p_String;
    }

    public static bool CMX_MaiorQue(this string p_string, string p_stringAComparar)
        => string.Compare(p_string, p_stringAComparar, StringComparison.OrdinalIgnoreCase) > 0;

    public static bool CMX_MenorQue(this string p_string, string p_stringAComparar)
        => string.Compare(p_string, p_stringAComparar, StringComparison.OrdinalIgnoreCase) < 0;
}
