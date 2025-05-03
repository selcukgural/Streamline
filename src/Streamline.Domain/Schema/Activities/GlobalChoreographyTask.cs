using Streamline.Domain.Schema.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tGlobalChoreographyTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("globalChoreographyTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class GlobalChoreographyTask : Choreography
{
        
    [System.Xml.Serialization.XmlAttributeAttribute("initiatingParticipantRef")]
    public System.Xml.XmlQualifiedName InitiatingParticipantRef { get; set; }
}