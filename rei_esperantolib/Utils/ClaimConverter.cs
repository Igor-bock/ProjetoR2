namespace rei_esperantolib.Utils
{
	public class ClaimConverter : JsonConverter
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject m_jo = JObject.Load(reader);
            string type = (string)m_jo["Type"];
            string value = (string)m_jo["Value"];
            string valueType = (string)m_jo["ValueType"];
            string issuer = (string)m_jo["Issuer"];
            string originalIssuer = (string)m_jo["OriginalIssuer"];
            return new Claim(type, value, valueType, issuer, originalIssuer);
        }

		public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(Claim);

		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();
	}
}
