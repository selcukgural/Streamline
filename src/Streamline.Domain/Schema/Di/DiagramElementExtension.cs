namespace Streamline.Domain.Schema.Di;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("DiagramElementExtension", Namespace="http://www.omg.org/spec/DD/20100524/DI", AnonymousType=true)]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class DiagramElementExtension
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlElement> _any;
        
    [System.Xml.Serialization.XmlAnyElementAttribute]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlElement> Any
    {
        get => _any;
        private set => _any = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Any collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool AnySpecified => Any.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="DiagramElementExtension" /> class.</para>
    /// </summary>
    public DiagramElementExtension()
    {
        _any = [];
    }
}