using System.Xml.Serialization;

namespace VascoAPI.DLL
{
    public class PS
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "authUser", Namespace = "http://www.vasco.com/IdentikeyServer/IdentikeyTypes/Authentication")]
            public AuthUser AuthUser { get; set; }
        }

        [XmlRoot(ElementName = "authUser", Namespace = "http://www.vasco.com/IdentikeyServer/IdentikeyTypes/Authentication")]
        public class AuthUser
        {
            [XmlElement(ElementName = "credentialAttributeSet", Namespace = "http://www.vasco.com/IdentikeyServer/IdentikeyTypes/Authentication")]
            public CredentialAttributeSet CredentialAttributeSet { get; set; }
        }

        public class CredentialAttributeSet
        {
            [XmlElement(ElementName = "attributes", Namespace = "http://www.vasco.com/IdentikeyServer/IdentikeyTypes/Authentication")]
            public List<Attribute> Attributes { get; set; }
        }

        public class Attribute
        {
            [XmlElement(ElementName = "value", Namespace = "http://www.w3.org/2001/XMLSchema")]
            public string Value { get; set; }

            [XmlElement(ElementName = "attributeID", Namespace = "http://www.w3.org/2001/XMLSchema")]
            public string AttributeID { get; set; }
        }


    }
}
