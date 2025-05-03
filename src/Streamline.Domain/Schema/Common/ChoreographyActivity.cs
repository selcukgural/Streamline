#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Activities;

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tChoreographyActivity", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("choreographyActivity", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CallChoreography))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ChoreographyTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SubChoreography))]
public abstract partial class ChoreographyActivity : FlowNode
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _participantRef;
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("participantRef")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> ParticipantRef
    {
        get => _participantRef;
        private set => _participantRef = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="ChoreographyActivity" /> class.</para>
    /// </summary>
    protected ChoreographyActivity()
    {
        _participantRef = [];
        _correlationKey = [];
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<CorrelationKey> _correlationKey;
        
    [System.Xml.Serialization.XmlElementAttribute("correlationKey")]
    public System.Collections.ObjectModel.Collection<CorrelationKey> CorrelationKey
    {
        get => _correlationKey;
        private set => _correlationKey = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the CorrelationKey collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool CorrelationKeySpecified => CorrelationKey.Count != 0;

    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("initiatingParticipantRef")]
    public System.Xml.XmlQualifiedName InitiatingParticipantRef { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private ChoreographyLoopType _loopType = ChoreographyLoopType.None;
        
    [System.ComponentModel.DefaultValueAttribute(ChoreographyLoopType.None)]
    [System.Xml.Serialization.XmlAttributeAttribute("loopType")]
    public ChoreographyLoopType LoopType
    {
        get => _loopType;
        set => _loopType = value;
    }
}