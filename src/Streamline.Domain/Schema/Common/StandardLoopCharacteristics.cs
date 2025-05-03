using Streamline.Domain.Schema.Activities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tStandardLoopCharacteristics", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("standardLoopCharacteristics", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class StandardLoopCharacteristics : LoopCharacteristics
{
        
    [System.Xml.Serialization.XmlElementAttribute("loopCondition")]
    public Expression LoopCondition { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _testBefore;
        
    [System.ComponentModel.DefaultValueAttribute(false)]
    [System.Xml.Serialization.XmlAttributeAttribute("testBefore")]
    public bool TestBefore
    {
        get => _testBefore;
        set => _testBefore = value;
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("loopMaximum")]
    public string LoopMaximum { get; set; }
}