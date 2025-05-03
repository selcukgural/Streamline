#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Activities;
using Streamline.Domain.Schema.Artifact;
using Streamline.Domain.Schema.Artifacts;
using Streamline.Domain.Schema.Data;
using Streamline.Domain.Schema.Events;
using Streamline.Domain.Schema.Flow;
using Streamline.Domain.Schema.Gateways;
using Task = Streamline.Domain.Schema.Activities.Task;

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tProcess", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("process", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class Process : CallableElement
{
        
    [System.Xml.Serialization.XmlElementAttribute("auditing")]
    public Auditing Auditing { get; set; }
        
    [System.Xml.Serialization.XmlElementAttribute("monitoring")]
    public Monitoring Monitoring { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Property> _property;
        
    [System.Xml.Serialization.XmlElementAttribute("property")]
    public System.Collections.ObjectModel.Collection<Property> Property
    {
        get => _property;
        private set => _property = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Property collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool PropertySpecified => Property.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Process" /> class.</para>
    /// </summary>
    public Process()
    {
        _property = [];
        _laneSet = [];
        _flowElement = [];
        _artifact = [];
        _resourceRole = [];
        _correlationSubscription = [];
        _supports = [];
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<LaneSet> _laneSet;
        
    [System.Xml.Serialization.XmlElementAttribute("laneSet")]
    public System.Collections.ObjectModel.Collection<LaneSet> LaneSet
    {
        get => _laneSet;
        private set => _laneSet = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the LaneSet collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool LaneSetSpecified => LaneSet.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<FlowElement> _flowElement;
        
    [System.Xml.Serialization.XmlElementAttribute("adHocSubProcess", Type=typeof(AdHocSubProcess), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("boundaryEvent", Type=typeof(BoundaryEvent), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("businessRuleTask", Type=typeof(BusinessRuleTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("callActivity", Type=typeof(CallActivity), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("callChoreography", Type=typeof(CallChoreography), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("choreographyTask", Type=typeof(ChoreographyTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("complexGateway", Type=typeof(ComplexGateway), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("dataObject", Type=typeof(DataObject), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("dataObjectReference", Type=typeof(DataObjectReference), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("dataStoreReference", Type=typeof(DataStoreReference), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("endEvent", Type=typeof(EndEvent), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("event", Type=typeof(Event), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("eventBasedGateway", Type=typeof(EventBasedGateway), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("exclusiveGateway", Type=typeof(ExclusiveGateway), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("implicitThrowEvent", Type=typeof(ImplicitThrowEvent), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("inclusiveGateway", Type=typeof(InclusiveGateway), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("intermediateCatchEvent", Type=typeof(IntermediateCatchEvent), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("intermediateThrowEvent", Type=typeof(IntermediateThrowEvent), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("manualTask", Type=typeof(ManualTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("parallelGateway", Type=typeof(ParallelGateway), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("receiveTask", Type=typeof(ReceiveTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("scriptTask", Type=typeof(ScriptTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("sendTask", Type=typeof(SendTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("sequenceFlow", Type=typeof(SequenceFlow), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("serviceTask", Type=typeof(ServiceTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("startEvent", Type=typeof(StartEvent), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("subChoreography", Type=typeof(SubChoreography), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("subProcess", Type=typeof(SubProcess), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("task", Type=typeof(Task), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("transaction", Type=typeof(Transaction), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("userTask", Type=typeof(UserTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("flowElement")]
    public System.Collections.ObjectModel.Collection<FlowElement> FlowElement
    {
        get => _flowElement;
        private set => _flowElement = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the FlowElement collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool FlowElementSpecified => FlowElement.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Artifacts.Artifact> _artifact;
        
    [System.Xml.Serialization.XmlElementAttribute("association", Type=typeof(Association), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("group", Type=typeof(Group), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("textAnnotation", Type=typeof(TextAnnotation), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("artifact")]
    public System.Collections.ObjectModel.Collection<Artifacts.Artifact> Artifact
    {
        get => _artifact;
        private set => _artifact = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Artifact collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ArtifactSpecified => Artifact.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<ResourceRole> _resourceRole;
        
    [System.Xml.Serialization.XmlElementAttribute("performer", Type=typeof(Performer), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("humanPerformer", Type=typeof(HumanPerformer), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("potentialOwner", Type=typeof(PotentialOwner), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("resourceRole")]
    public System.Collections.ObjectModel.Collection<ResourceRole> ResourceRole
    {
        get => _resourceRole;
        private set => _resourceRole = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the ResourceRole collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ResourceRoleSpecified => ResourceRole.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<CorrelationSubscription> _correlationSubscription;
        
    [System.Xml.Serialization.XmlElementAttribute("correlationSubscription")]
    public System.Collections.ObjectModel.Collection<CorrelationSubscription> CorrelationSubscription
    {
        get => _correlationSubscription;
        private set => _correlationSubscription = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the CorrelationSubscription collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool CorrelationSubscriptionSpecified => CorrelationSubscription.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _supports;
        
    [System.Xml.Serialization.XmlElementAttribute("supports")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> Supports
    {
        get => _supports;
        private set => _supports = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Supports collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool SupportsSpecified => Supports.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private ProcessType _processType = ProcessType.None;
        
    [System.ComponentModel.DefaultValueAttribute(ProcessType.None)]
    [System.Xml.Serialization.XmlAttributeAttribute("processType")]
    public ProcessType ProcessType
    {
        get => _processType;
        set => _processType = value;
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _isClosed;
        
    [System.ComponentModel.DefaultValueAttribute(false)]
    [System.Xml.Serialization.XmlAttributeAttribute("isClosed")]
    public bool IsClosed
    {
        get => _isClosed;
        set => _isClosed = value;
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("isExecutable")]
    public bool IsExecutable { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the IsExecutable property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IsExecutableSpecified { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("definitionalCollaborationRef")]
    public System.Xml.XmlQualifiedName DefinitionalCollaborationRef { get; set; }
}