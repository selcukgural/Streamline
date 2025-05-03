namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tParticipantMultiplicity", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("participantMultiplicity", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class ParticipantMultiplicity : BaseElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private int _minimum;
        
    [System.ComponentModel.DefaultValueAttribute(0)]
    [System.Xml.Serialization.XmlAttributeAttribute("minimum")]
    public int Minimum
    {
        get => _minimum;
        set => _minimum = value;
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private int _maximum = 1;
        
    [System.ComponentModel.DefaultValueAttribute(1)]
    [System.Xml.Serialization.XmlAttributeAttribute("maximum")]
    public int Maximum
    {
        get => _maximum;
        set => _maximum = value;
    }
}