#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Di;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("BPMNLabel", Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("BPMNLabel", Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
public partial class BpmnLabel : Label
{
        
    [System.Xml.Serialization.XmlAttributeAttribute("labelStyle")]
    public System.Xml.XmlQualifiedName LabelStyle { get; set; }
}