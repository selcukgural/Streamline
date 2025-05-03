#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Data;

namespace Streamline.Domain.Schema.Events;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tThrowEvent", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("throwEvent", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EndEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ImplicitThrowEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(IntermediateThrowEvent))]
public abstract partial class ThrowEvent : Event
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<DataInput> _dataInput;
        
    [System.Xml.Serialization.XmlElementAttribute("dataInput")]
    public System.Collections.ObjectModel.Collection<DataInput> DataInput
    {
        get => _dataInput;
        private set => _dataInput = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the DataInput collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool DataInputSpecified => DataInput.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="ThrowEvent" /> class.</para>
    /// </summary>
    protected ThrowEvent()
    {
        _dataInput = [];
        _dataInputAssociation = [];
        _eventDefinition = [];
        _eventDefinitionRef = [];
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<DataInputAssociation> _dataInputAssociation;
        
    [System.Xml.Serialization.XmlElementAttribute("dataInputAssociation")]
    public System.Collections.ObjectModel.Collection<DataInputAssociation> DataInputAssociation
    {
        get => _dataInputAssociation;
        private set => _dataInputAssociation = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the DataInputAssociation collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool DataInputAssociationSpecified => DataInputAssociation.Count != 0;

    [System.Xml.Serialization.XmlElementAttribute("inputSet")]
    public InputSet InputSet { get; set; }
        
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
}