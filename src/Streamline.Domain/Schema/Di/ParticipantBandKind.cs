namespace Streamline.Domain.Schema.Di;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("ParticipantBandKind", Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
public enum ParticipantBandKind
{
        
    [System.Xml.Serialization.XmlEnumAttribute("top_initiating")]
    TopInitiating,
        
    [System.Xml.Serialization.XmlEnumAttribute("middle_initiating")]
    MiddleInitiating,
        
    [System.Xml.Serialization.XmlEnumAttribute("bottom_initiating")]
    BottomInitiating,
        
    [System.Xml.Serialization.XmlEnumAttribute("top_non_initiating")]
    TopNonInitiating,
        
    [System.Xml.Serialization.XmlEnumAttribute("middle_non_initiating")]
    MiddleNonInitiating,
        
    [System.Xml.Serialization.XmlEnumAttribute("bottom_non_initiating")]
    BottomNonInitiating,
}