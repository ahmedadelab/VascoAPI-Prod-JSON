using System.Text.Json.Serialization;

namespace VascoAPI
{
    public class SVASCOJSON
    {
        public class CredentialAttribute
        {
            [JsonPropertyName("value")]
            public string Value { get; set; }

            [JsonPropertyName("attributeID")]
            public string AttributeID { get; set; }
        }

        public class CredentialRequest
        {
            [JsonPropertyName("credentialAttributeSet")]
            public List<CredentialAttribute> CredentialAttributeSet { get; set; }
        }


        public class ResultCodes
        {
            public string ReturnCodeEnum { get; set; }
            public string StatusCodeEnum { get; set; }
            public string ReturnCode { get; set; }
            public string StatusCode { get; set; }
        }

        public class Error
        {
            public string ErrorCode { get; set; }
            public string ErrorDesc { get; set; }
        }

        public class ErrorStack
        {
            public List<Error> Errors { get; set; }
        }

        public class Results
        {
            public ResultCodes ResultCodes { get; set; }
            public ErrorStack ErrorStack { get; set; }
        }

        public class Root
        {
            public Results Results { get; set; }
        }
    }
}
