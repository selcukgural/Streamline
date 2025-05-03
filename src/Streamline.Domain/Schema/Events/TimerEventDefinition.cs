#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Events;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tTimerEventDefinition", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("timerEventDefinition", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class TimerEventDefinition : EventDefinition
{
        
    [System.Xml.Serialization.XmlElementAttribute("timeDate")]
    public Expression TimeDate { get; set; }
        
    [System.Xml.Serialization.XmlElementAttribute("timeDuration")]
    public Expression TimeDuration { get; set; }
        
    [System.Xml.Serialization.XmlElementAttribute("timeCycle")]
    public Expression TimeCycle { get; set; }
}