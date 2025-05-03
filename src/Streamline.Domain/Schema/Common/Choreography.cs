using Streamline.Domain.Schema.Activities;
using Streamline.Domain.Schema.Data;
using Streamline.Domain.Schema.Events;
using Streamline.Domain.Schema.Flow;
using Streamline.Domain.Schema.Gateways;
using Task = Streamline.Domain.Schema.Activities.Task;

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tChoreography", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("choreography", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GlobalChoreographyTask))]
public partial class Choreography : Collaboration
{
        
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

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Choreography" /> class.</para>
    /// </summary>
    public Choreography()
    {
        _flowElement = [];
    }
}