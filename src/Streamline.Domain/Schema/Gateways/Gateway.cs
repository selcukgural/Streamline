using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Gateways;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tGateway", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("gateway", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ComplexGateway))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EventBasedGateway))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExclusiveGateway))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(InclusiveGateway))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ParallelGateway))]
public partial class Gateway : FlowNode
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private GatewayDirection _gatewayDirection = GatewayDirection.Unspecified;
        
    [System.ComponentModel.DefaultValueAttribute(GatewayDirection.Unspecified)]
    [System.Xml.Serialization.XmlAttributeAttribute("gatewayDirection")]
    public GatewayDirection GatewayDirection
    {
        get => _gatewayDirection;
        set => _gatewayDirection = value;
    }
}