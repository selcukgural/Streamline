namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tTransaction", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("transaction", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class Transaction : SubProcess
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private string _method = "##Compensate";
        
    [System.ComponentModel.DefaultValueAttribute("##Compensate")]
    [System.Xml.Serialization.XmlAttributeAttribute("method")]
    public string Method
    {
        get => _method;
        set => _method = value;
    }
}