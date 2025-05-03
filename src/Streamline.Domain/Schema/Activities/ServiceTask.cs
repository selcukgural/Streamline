#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tServiceTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("serviceTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class ServiceTask : Task
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
        
    [System.Xml.Serialization.XmlAttributeAttribute("operationRef")]
    public System.Xml.XmlQualifiedName OperationRef { get; set; }
}