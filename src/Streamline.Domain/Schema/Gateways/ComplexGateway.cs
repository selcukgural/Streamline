#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Gateways;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tComplexGateway", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("complexGateway", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class ComplexGateway : Gateway
{
        
    [System.Xml.Serialization.XmlElementAttribute("activationCondition")]
    public Expression ActivationCondition { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("default")]
    public string Default { get; set; }
}