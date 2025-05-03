using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Events;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tEventDefinition", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("eventDefinition", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CancelEventDefinition))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CompensateEventDefinition))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ConditionalEventDefinition))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ErrorEventDefinition))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EscalationEventDefinition))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LinkEventDefinition))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(MessageEventDefinition))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SignalEventDefinition))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(TerminateEventDefinition))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(TimerEventDefinition))]
public abstract partial class EventDefinition : RootElement;