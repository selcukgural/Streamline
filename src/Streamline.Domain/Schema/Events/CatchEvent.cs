#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Data;

namespace Streamline.Domain.Schema.Events;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tCatchEvent", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("catchEvent", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BoundaryEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(IntermediateCatchEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(StartEvent))]
public abstract partial class CatchEvent : Event
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<DataOutput> _dataOutput;
        
    [System.Xml.Serialization.XmlElementAttribute("dataOutput")]
    public System.Collections.ObjectModel.Collection<DataOutput> DataOutput
    {
        get => _dataOutput;
        private set => _dataOutput = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the DataOutput collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool DataOutputSpecified => DataOutput.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="CatchEvent" /> class.</para>
    /// </summary>
    protected CatchEvent()
    {
        _dataOutput = [];
        _dataOutputAssociation = [];
        _eventDefinition = [];
        _eventDefinitionRef = [];
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<DataOutputAssociation> _dataOutputAssociation;
        
    [System.Xml.Serialization.XmlElementAttribute("dataOutputAssociation")]
    public System.Collections.ObjectModel.Collection<DataOutputAssociation> DataOutputAssociation
    {
        get => _dataOutputAssociation;
        private set => _dataOutputAssociation = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the DataOutputAssociation collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool DataOutputAssociationSpecified => DataOutputAssociation.Count != 0;

    [System.Xml.Serialization.XmlElementAttribute("outputSet")]
    public OutputSet OutputSet { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<EventDefinition> _eventDefinition;
        
    [System.Xml.Serialization.XmlElementAttribute("cancelEventDefinition", Type=typeof(CancelEventDefinition), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("compensateEventDefinition", Type=typeof(CompensateEventDefinition), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("conditionalEventDefinition", Type=typeof(ConditionalEventDefinition), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("errorEventDefinition", Type=typeof(ErrorEventDefinition), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("escalationEventDefinition", Type=typeof(EscalationEventDefinition), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("linkEventDefinition", Type=typeof(LinkEventDefinition), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("messageEventDefinition", Type=typeof(MessageEventDefinition), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("signalEventDefinition", Type=typeof(SignalEventDefinition), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("terminateEventDefinition", Type=typeof(TerminateEventDefinition), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("timerEventDefinition", Type=typeof(TimerEventDefinition), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("eventDefinition")]
    public System.Collections.ObjectModel.Collection<EventDefinition> EventDefinition
    {
        get => _eventDefinition;
        private set => _eventDefinition = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the EventDefinition collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool EventDefinitionSpecified => EventDefinition.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _eventDefinitionRef;
        
    [System.Xml.Serialization.XmlElementAttribute("eventDefinitionRef")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> EventDefinitionRef
    {
        get => _eventDefinitionRef;
        private set => _eventDefinitionRef = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the EventDefinitionRef collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool EventDefinitionRefSpecified => EventDefinitionRef.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _parallelMultiple;
        
    [System.ComponentModel.DefaultValueAttribute(false)]
    [System.Xml.Serialization.XmlAttributeAttribute("parallelMultiple")]
    public bool ParallelMultiple
    {
        get => _parallelMultiple;
        set => _parallelMultiple = value;
    }
}