#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Data;
using Streamline.Domain.Schema.Events;

namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tMultiInstanceLoopCharacteristics", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("multiInstanceLoopCharacteristics", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class MultiInstanceLoopCharacteristics : LoopCharacteristics
{
        
    [System.Xml.Serialization.XmlElementAttribute("loopCardinality")]
    public Expression LoopCardinality { get; set; }
        
    [System.Xml.Serialization.XmlElementAttribute("loopDataInputRef")]
    public System.Xml.XmlQualifiedName LoopDataInputRef { get; set; }
        
    [System.Xml.Serialization.XmlElementAttribute("loopDataOutputRef")]
    public System.Xml.XmlQualifiedName LoopDataOutputRef { get; set; }
        
    [System.Xml.Serialization.XmlElementAttribute("inputDataItem")]
    public DataInput InputDataItem { get; set; }
        
    [System.Xml.Serialization.XmlElementAttribute("outputDataItem")]
    public DataOutput OutputDataItem { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<ComplexBehaviorDefinition> _complexBehaviorDefinition;
        
    [System.Xml.Serialization.XmlElementAttribute("complexBehaviorDefinition")]
    public System.Collections.ObjectModel.Collection<ComplexBehaviorDefinition> ComplexBehaviorDefinition
    {
        get => _complexBehaviorDefinition;
        private set => _complexBehaviorDefinition = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the ComplexBehaviorDefinition collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ComplexBehaviorDefinitionSpecified => ComplexBehaviorDefinition.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="MultiInstanceLoopCharacteristics" /> class.</para>
    /// </summary>
    public MultiInstanceLoopCharacteristics()
    {
        _complexBehaviorDefinition = new System.Collections.ObjectModel.Collection<ComplexBehaviorDefinition>();
    }
        
    [System.Xml.Serialization.XmlElementAttribute("completionCondition")]
    public Expression CompletionCondition { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _isSequential;
        
    [System.ComponentModel.DefaultValueAttribute(false)]
    [System.Xml.Serialization.XmlAttributeAttribute("isSequential")]
    public bool IsSequential
    {
        get => _isSequential;
        set => _isSequential = value;
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private MultiInstanceFlowCondition _behavior = MultiInstanceFlowCondition.All;
        
    [System.ComponentModel.DefaultValueAttribute(MultiInstanceFlowCondition.All)]
    [System.Xml.Serialization.XmlAttributeAttribute("behavior")]
    public MultiInstanceFlowCondition Behavior
    {
        get => _behavior;
        set => _behavior = value;
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("oneBehaviorEventRef")]
    public System.Xml.XmlQualifiedName OneBehaviorEventRef { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("noneBehaviorEventRef")]
    public System.Xml.XmlQualifiedName NoneBehaviorEventRef { get; set; }
}