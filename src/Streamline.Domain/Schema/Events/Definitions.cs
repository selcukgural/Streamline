#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Activities;
using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Data;
using Streamline.Domain.Schema.Di;
using Streamline.Domain.Schema.Flow;

namespace Streamline.Domain.Schema.Events;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tDefinitions", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("definitions", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class Definitions
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Import> _import;
        
    [System.Xml.Serialization.XmlElementAttribute("import")]
    public System.Collections.ObjectModel.Collection<Import> Import
    {
        get => _import;
        private set => _import = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Import collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ImportSpecified => Import.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Definitions" /> class.</para>
    /// </summary>
    public Definitions()
    {
        _import = [];
        _extension = [];
        _rootElement = [];
        _bpmnDiagram = [];
        _relationship = [];
        _anyAttribute = [];
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Extension> _extension;
        
    [System.Xml.Serialization.XmlElementAttribute("extension")]
    public System.Collections.ObjectModel.Collection<Extension> Extension
    {
        get => _extension;
        private set => _extension = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Extension collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ExtensionSpecified => Extension.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<RootElement> _rootElement;
        
    [System.Xml.Serialization.XmlElementAttribute("category", Type=typeof(Category), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("collaboration", Type=typeof(Collaboration), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("choreography", Type=typeof(Choreography), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("globalChoreographyTask", Type=typeof(GlobalChoreographyTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("globalConversation", Type=typeof(GlobalConversation), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("correlationProperty", Type=typeof(CorrelationProperty), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("dataStore", Type=typeof(DataStore), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("endPoint", Type=typeof(EndPoint), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("error", Type=typeof(Error), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("escalation", Type=typeof(Escalation), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("eventDefinition", Type=typeof(EventDefinition), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
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
    [System.Xml.Serialization.XmlElementAttribute("globalBusinessRuleTask", Type=typeof(GlobalBusinessRuleTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("globalManualTask", Type=typeof(GlobalManualTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("globalScriptTask", Type=typeof(GlobalScriptTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("globalTask", Type=typeof(GlobalTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("globalUserTask", Type=typeof(GlobalUserTask), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("interface", Type=typeof(Interface), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("itemDefinition", Type=typeof(ItemDefinition), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("message", Type=typeof(Message), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("partnerEntity", Type=typeof(PartnerEntity), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("partnerRole", Type=typeof(PartnerRole), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("process", Type=typeof(Process), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("resource", Type=typeof(Resource), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("signal", Type=typeof(Signal), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("rootElement")]
    public System.Collections.ObjectModel.Collection<RootElement> RootElement
    {
        get => _rootElement;
        private set => _rootElement = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the RootElement collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool RootElementSpecified => RootElement.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<BpmnDiagram> _bpmnDiagram;
        
    [System.Xml.Serialization.XmlElementAttribute("BPMNDiagram", Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
    public System.Collections.ObjectModel.Collection<BpmnDiagram> BpmnDiagram
    {
        get => _bpmnDiagram;
        private set => _bpmnDiagram = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the BpmnDiagram collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool BpmnDiagramSpecified => BpmnDiagram.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Relationship> _relationship;
        
    [System.Xml.Serialization.XmlElementAttribute("relationship")]
    public System.Collections.ObjectModel.Collection<Relationship> Relationship
    {
        get => _relationship;
        private set => _relationship = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Relationship collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool RelationshipSpecified => Relationship.Count != 0;

    [System.Xml.Serialization.XmlAttributeAttribute("id")]
    public string Id { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("targetNamespace")]
    public string TargetNamespace { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private string _expressionLanguage = "http://www.w3.org/1999/XPath";
        
    [System.ComponentModel.DefaultValueAttribute("http://www.w3.org/1999/XPath")]
    [System.Xml.Serialization.XmlAttributeAttribute("expressionLanguage")]
    public string ExpressionLanguage
    {
        get => _expressionLanguage;
        set => _expressionLanguage = value;
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private string _typeLanguage = "http://www.w3.org/2001/XMLSchema";
        
    [System.ComponentModel.DefaultValueAttribute("http://www.w3.org/2001/XMLSchema")]
    [System.Xml.Serialization.XmlAttributeAttribute("typeLanguage")]
    public string TypeLanguage
    {
        get => _typeLanguage;
        set => _typeLanguage = value;
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("exporter")]
    public string Exporter { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("exporterVersion")]
    public string ExporterVersion { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute> _anyAttribute;
        
    [System.Xml.Serialization.XmlAnyAttributeAttribute]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute> AnyAttribute
    {
        get => _anyAttribute;
        private set => _anyAttribute = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the AnyAttribute collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool AnyAttributeSpecified => AnyAttribute.Count != 0;

    /// <summary>
    /// Finds sequence flow elements within any process definition by their IDs.
    /// </summary>
    /// <param name="ids">A list of sequence flow IDs to find.</param>
    /// <returns>A list of found SequenceFlow elements.</returns>
    public List<SequenceFlow> FindSequenceFlows(List<string> ids)
    {
        var idSet = new HashSet<string>(ids);
        var foundFlows = new List<SequenceFlow>();

        if (RootElementSpecified)
        {
            // Search within Process elements first
            foreach (var process in RootElement.OfType<Process>()) // Assumes Process inherits from RootElement
            {
                // Assumes Process has a FlowElement collection
                if (process.FlowElement != null) 
                {
                    foundFlows.AddRange(process.FlowElement.OfType<SequenceFlow>().Where(sf => sf != null && idSet.Contains(sf.Id)));
                }
            }
        }
        
        // Remove found IDs to avoid returning duplicates if searched elsewhere
        foreach(var foundFlow in foundFlows.ToList()) // Iterate over copy for removal
        {
            if (foundFlow != null) idSet.Remove(foundFlow.Id);
        }
        
        // TODO: Add searches in Collaborations or Choreographies if needed

        return foundFlows;
    }

    /// <summary>
    /// Finds a specific FlowElement (like a Task, Event, Gateway) by its ID across all processes.
    /// </summary>
    /// <typeparam name="TFlowElement">The type of FlowElement to find.</typeparam>
    /// <param name="id">The ID of the element.</param>
    /// <returns>The found element or null.</returns>
    public TFlowElement? FindFlowElementById<TFlowElement>(string id) where TFlowElement : FlowElement
    {
         if (RootElementSpecified)
        {
            // Search within Process elements first
            foreach (var process in RootElement.OfType<Process>())
            {
                // Assumes Process has a FlowElement collection
                if (process.FlowElement != null)
                {
                    var element = process.FlowElement.OfType<TFlowElement>().FirstOrDefault(el => el != null && el.Id == id);
                    if (element != null)
                    {
                        return element;
                    }
                }
            }
        }
        
        // TODO: Add searches in Collaborations or Choreographies if needed

        return null;
    }

    /// <summary>
    /// Finds all sequence flows that target the specified flow node ID.
    /// </summary>
    /// <param name="targetFlowNodeId">The ID of the target flow node.</param>
    /// <returns>A list of incoming sequence flows.</returns>
    public List<SequenceFlow> FindIncomingSequenceFlows(string targetFlowNodeId)
    {
        var incomingFlows = new List<SequenceFlow>();
        if (string.IsNullOrEmpty(targetFlowNodeId)) return incomingFlows;

        if (RootElementSpecified)
        {
            foreach (var process in RootElement.OfType<Process>())
            {
                if (process.FlowElement != null)
                {
                    incomingFlows.AddRange(
                        process.FlowElement.OfType<SequenceFlow>()
                               .Where(sf => sf != null && sf.TargetRef == targetFlowNodeId)
                    );
                }
            }
        }
        
        // TODO: Add searches in Collaborations or Choreographies if needed
        
        return incomingFlows;
    }
}
#pragma warning restore CS8618