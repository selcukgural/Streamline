#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Data;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tDataStore", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("dataStore", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class DataStore : RootElement
{
        
    [System.Xml.Serialization.XmlElementAttribute("dataState")]
    public DataState DataState { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("capacity")]
    public string Capacity { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _isUnlimited = true;
        
    [System.ComponentModel.DefaultValueAttribute(true)]
    [System.Xml.Serialization.XmlAttributeAttribute("isUnlimited")]
    public bool IsUnlimited
    {
        get => _isUnlimited;
        set => _isUnlimited = value;
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("itemSubjectRef")]
    public System.Xml.XmlQualifiedName ItemSubjectRef { get; set; }
}