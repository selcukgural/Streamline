#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tCorrelationProperty", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("correlationProperty", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class CorrelationProperty : RootElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<CorrelationPropertyRetrievalExpression> _correlationPropertyRetrievalExpression;
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("correlationPropertyRetrievalExpression")]
    public System.Collections.ObjectModel.Collection<CorrelationPropertyRetrievalExpression> CorrelationPropertyRetrievalExpression
    {
        get => _correlationPropertyRetrievalExpression;
        private set => _correlationPropertyRetrievalExpression = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="CorrelationProperty" /> class.</para>
    /// </summary>
    public CorrelationProperty()
    {
        _correlationPropertyRetrievalExpression = [];
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("type")]
    public System.Xml.XmlQualifiedName Type { get; set; }
}