using Streamline.Domain.Schema.Activities;
using Streamline.Domain.Schema.Events;
using Streamline.Domain.Schema.Gateways;
using Task = Streamline.Domain.Schema.Activities.Task;

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tFlowNode", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("flowNode", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Activity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AdHocSubProcess))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BoundaryEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BusinessRuleTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CallActivity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CallChoreography))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CatchEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ChoreographyActivity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ChoreographyTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ComplexGateway))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EndEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Event))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EventBasedGateway))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExclusiveGateway))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Gateway))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ImplicitThrowEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(InclusiveGateway))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(IntermediateCatchEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(IntermediateThrowEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ManualTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ParallelGateway))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ReceiveTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ScriptTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SendTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ServiceTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(StartEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SubChoreography))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SubProcess))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Task))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ThrowEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Transaction))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(UserTask))]
public abstract partial class FlowNode : FlowElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _incoming;
        
    [System.Xml.Serialization.XmlElementAttribute("incoming")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> Incoming
    {
        get => _incoming;
        private set => _incoming = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Incoming collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IncomingSpecified => Incoming.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="FlowNode" /> class.</para>
    /// </summary>
    protected FlowNode()
    {
        _incoming = [];
        _outgoing = [];
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _outgoing;
        
    [System.Xml.Serialization.XmlElementAttribute("outgoing")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> Outgoing
    {
        get => _outgoing;
        private set => _outgoing = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Outgoing collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool OutgoingSpecified => Outgoing.Count != 0;
}