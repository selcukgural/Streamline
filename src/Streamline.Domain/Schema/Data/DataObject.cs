using Streamline.Domain.Schema.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Data;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tDataObject", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("dataObject", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class DataObject : FlowElement
{
        
    [System.Xml.Serialization.XmlElementAttribute("dataState")]
    public DataState DataState { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("itemSubjectRef")]
    public System.Xml.XmlQualifiedName ItemSubjectRef { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _isCollection;
        
    [System.ComponentModel.DefaultValueAttribute(false)]
    [System.Xml.Serialization.XmlAttributeAttribute("isCollection")]
    public bool IsCollection
    {
        get => _isCollection;
        set => _isCollection = value;
    }
}