using Streamline.Domain.Schema.Artifact;
using Streamline.Domain.Schema.Artifacts;
using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Data;
using Streamline.Domain.Schema.Events;
using Streamline.Domain.Schema.Flow;
using Streamline.Domain.Schema.Gateways;

namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tSubProcess", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("subProcess", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AdHocSubProcess))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Transaction))]
public partial class SubProcess : Activity
{
        
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

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="SubProcess" /> class.</para>
    /// </summary>
    public SubProcess()
    {
        _laneSet = [];
        _flowElement = [];
        _artifact = [];
    }
        
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
    private bool _triggeredByEvent;
        
    [System.ComponentModel.DefaultValueAttribute(false)]
    [System.Xml.Serialization.XmlAttributeAttribute("triggeredByEvent")]
    public bool TriggeredByEvent
    {
        get => _triggeredByEvent;
        set => _triggeredByEvent = value;
    }
}