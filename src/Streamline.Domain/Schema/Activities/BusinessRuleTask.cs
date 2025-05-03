namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tBusinessRuleTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("businessRuleTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class BusinessRuleTask : Task
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private string _implementation = "##unspecified";
        
    [System.ComponentModel.DefaultValueAttribute("##unspecified")]
    [System.Xml.Serialization.XmlAttributeAttribute("implementation")]
    public string Implementation
    {
        get => _implementation;
        set => _implementation = value;
    }
}