#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Di;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("BPMNPlane", Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("BPMNPlane", Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
public partial class BpmnPlane : Plane
{
        
    [System.Xml.Serialization.XmlAttributeAttribute("bpmnElement")]
    public System.Xml.XmlQualifiedName BpmnElement { get; set; }
}