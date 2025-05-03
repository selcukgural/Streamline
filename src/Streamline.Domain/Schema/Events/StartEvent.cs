namespace Streamline.Domain.Schema.Events;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tStartEvent", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("startEvent", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class StartEvent : CatchEvent
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _isInterrupting = true;
        
    [System.ComponentModel.DefaultValueAttribute(true)]
    [System.Xml.Serialization.XmlAttributeAttribute("isInterrupting")]
    public bool IsInterrupting
    {
        get => _isInterrupting;
        set => _isInterrupting = value;
    }
}