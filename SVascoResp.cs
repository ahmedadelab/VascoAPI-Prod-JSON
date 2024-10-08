﻿using System.Xml.Serialization;

namespace VascoAPI
{
    public class SVascoResp
    {
        [XmlRoot(ElementName = "authUserResponse", Namespace = "http://www.vasco.com/IdentikeyServer/IdentikeyTypes/Authentication")]
        public class AuthUserResponse
        {
            [XmlElement(ElementName = "authUserResults")]
            public AuthUserResults AuthUserResults { get; set; }
        }

        public class AuthUserResults
        {
            [XmlElement(ElementName = "results")]
            public CredentialResults Results { get; set; }
        }

        public class CredentialResults
        {
            [XmlElement(ElementName = "resultCodes")]
            public ResultCodes ResultCodes { get; set; }

            [XmlElement(ElementName = "resultAttribute")]
            public CredentialAttributeSet ResultAttribute { get; set; }

            [XmlElement(ElementName = "errorStack")]
            public ErrorStack ErrorStack { get; set; }
        }

        public class ResultCodes
        {
            [XmlElement(ElementName = "returnCodeEnum")]
            public string ReturnCodeEnum { get; set; }

            [XmlElement(ElementName = "statusCodeEnum")]
            public string StatusCodeEnum { get; set; }

            [XmlElement(ElementName = "returnCode")]
            public int ReturnCode { get; set; }

            [XmlElement(ElementName = "statusCode")]
            public int StatusCode { get; set; }
        }

        public class CredentialAttributeSet
        {
            // Define properties for credential attributes if needed
        }

        public class ErrorStack
        {
            [XmlElement(ElementName = "errors")]
            public Error Errors { get; set; }
        }

        public class Error
        {
            [XmlElement(ElementName = "errorCode")]
            public int ErrorCode { get; set; }

            [XmlElement(ElementName = "errorDesc")]
            public string ErrorDesc { get; set; }
        }
    }
}
