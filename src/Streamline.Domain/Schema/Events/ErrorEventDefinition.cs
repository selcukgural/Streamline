#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Events;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tErrorEventDefinition", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("errorEventDefinition", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class ErrorEventDefinition : EventDefinition
{
        
    [System.Xml.Serialization.XmlAttributeAttribute("errorRef")]
    public System.Xml.XmlQualifiedName ErrorRef { get; set; }
}