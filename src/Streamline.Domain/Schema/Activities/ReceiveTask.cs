#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tReceiveTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("receiveTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class ReceiveTask : Task
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private string _implementation = "##WebService";
        
    [System.ComponentModel.DefaultValueAttribute("##WebService")]
    [System.Xml.Serialization.XmlAttributeAttribute("implementation")]
    public string Implementation
    {
        get => _implementation;
        set => _implementation = value;
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _instantiate;
        
    [System.ComponentModel.DefaultValueAttribute(false)]
    [System.Xml.Serialization.XmlAttributeAttribute("instantiate")]
    public bool Instantiate
    {
        get => _instantiate;
        set => _instantiate = value;
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("messageRef")]
    public System.Xml.XmlQualifiedName MessageRef { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("operationRef")]
    public System.Xml.XmlQualifiedName OperationRef { get; set; }
}