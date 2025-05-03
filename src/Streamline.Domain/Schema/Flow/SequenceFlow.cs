#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Flow;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tSequenceFlow", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("sequenceFlow", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class SequenceFlow : FlowElement
{
        
    [System.Xml.Serialization.XmlElementAttribute("conditionExpression")]
    public Expression ConditionExpression { get; set; }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("sourceRef")]
    public string SourceRef { get; set; }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("targetRef")]
    public string TargetRef { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("isImmediate")]
    public bool IsImmediate { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the IsImmediate property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IsImmediateSpecified { get; set; }
}