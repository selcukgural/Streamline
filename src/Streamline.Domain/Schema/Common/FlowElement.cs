#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Activities;
using Streamline.Domain.Schema.Data;
using Streamline.Domain.Schema.Events;
using Streamline.Domain.Schema.Flow;
using Streamline.Domain.Schema.Gateways;
using Task = Streamline.Domain.Schema.Activities.Task;

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tFlowElement", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("flowElement", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
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
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataObject))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataObjectReference))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataStoreReference))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EndEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Event))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EventBasedGateway))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExclusiveGateway))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(FlowNode))]
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
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SequenceFlow))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ServiceTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(StartEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SubChoreography))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SubProcess))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Task))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ThrowEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Transaction))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(UserTask))]
public abstract partial class FlowElement : BaseElement
{
        
    [System.Xml.Serialization.XmlElementAttribute("auditing")]
    public Auditing Auditing { get; set; }
        
    [System.Xml.Serialization.XmlElementAttribute("monitoring")]
    public Monitoring Monitoring { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _categoryValueRef;
        
    [System.Xml.Serialization.XmlElementAttribute("categoryValueRef")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> CategoryValueRef
    {
        get => _categoryValueRef;
        private set => _categoryValueRef = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the CategoryValueRef collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool CategoryValueRefSpecified => CategoryValueRef.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="FlowElement" /> class.</para>
    /// </summary>
    protected FlowElement()
    {
        _categoryValueRef = [];
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
}