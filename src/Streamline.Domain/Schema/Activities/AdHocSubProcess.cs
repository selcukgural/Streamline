
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tAdHocSubProcess", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("adHocSubProcess", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class AdHocSubProcess : SubProcess
{
        
    [System.Xml.Serialization.XmlElementAttribute("completionCondition")]
    public Expression CompletionCondition { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _cancelRemainingInstances = true;
        
    [System.ComponentModel.DefaultValueAttribute(true)]
    [System.Xml.Serialization.XmlAttributeAttribute("cancelRemainingInstances")]
    public bool CancelRemainingInstances
    {
        get => _cancelRemainingInstances;
        set => _cancelRemainingInstances = value;
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("ordering")]
    public AdHocOrdering Ordering { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the Ordering property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool OrderingSpecified { get; set; }
}