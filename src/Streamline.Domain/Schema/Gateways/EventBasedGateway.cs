namespace Streamline.Domain.Schema.Gateways;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tEventBasedGateway", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("eventBasedGateway", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class EventBasedGateway : Gateway
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _instantiate;
        
    [System.ComponentModel.DefaultValueAttribute(false)]
    [System.Xml.Serialization.XmlAttributeAttribute("instantiate")]
    public bool Instantiate
    {
        get => _instantiate;
        set => _instantiate = value;
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private EventBasedGatewayType _eventGatewayType = EventBasedGatewayType.Exclusive;
        
    [System.ComponentModel.DefaultValueAttribute(EventBasedGatewayType.Exclusive)]
    [System.Xml.Serialization.XmlAttributeAttribute("eventGatewayType")]
    public EventBasedGatewayType EventGatewayType
    {
        get => _eventGatewayType;
        set => _eventGatewayType = value;
    }
}