#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Data;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tDataAssociation", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("dataAssociation", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataInputAssociation))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataOutputAssociation))]
public partial class DataAssociation : BaseElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<string> _sourceRef;
        
    [System.Xml.Serialization.XmlElementAttribute("sourceRef")]
    public System.Collections.ObjectModel.Collection<string> SourceRef
    {
        get => _sourceRef;
        private set => _sourceRef = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the SourceRef collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool SourceRefSpecified => SourceRef.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="DataAssociation" /> class.</para>
    /// </summary>
    public DataAssociation()
    {
        _sourceRef = [];
        _assignment = [];
    }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("targetRef")]
    public string TargetRef { get; set; }
        
    [System.Xml.Serialization.XmlElementAttribute("transformation")]
    public FormalExpression Transformation { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Assignment> _assignment;
        
    [System.Xml.Serialization.XmlElementAttribute("assignment")]
    public System.Collections.ObjectModel.Collection<Assignment> Assignment
    {
        get => _assignment;
        private set => _assignment = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Assignment collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool AssignmentSpecified => Assignment.Count != 0;
}