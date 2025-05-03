#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Events;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tCompensateEventDefinition", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("compensateEventDefinition", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class CompensateEventDefinition : EventDefinition
{
        
    [System.Xml.Serialization.XmlAttributeAttribute("waitForCompletion")]
    public bool WaitForCompletion { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the WaitForCompletion property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool WaitForCompletionSpecified { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("activityRef")]
    public System.Xml.XmlQualifiedName ActivityRef { get; set; }
}