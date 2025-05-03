#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Data;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tDataStoreReference", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("dataStoreReference", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class DataStoreReference : FlowElement
{
        
    [System.Xml.Serialization.XmlElementAttribute("dataState")]
    public DataState DataState { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("itemSubjectRef")]
    public System.Xml.XmlQualifiedName ItemSubjectRef { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("dataStoreRef")]
    public System.Xml.XmlQualifiedName DataStoreRef { get; set; }
}