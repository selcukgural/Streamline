using Streamline.Domain.Schema.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Events;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tItemDefinition", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("itemDefinition", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class ItemDefinition : RootElement
{
        
    [System.Xml.Serialization.XmlAttributeAttribute("structureRef")]
    public System.Xml.XmlQualifiedName StructureRef { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _isCollection;
        
    [System.ComponentModel.DefaultValueAttribute(false)]
    [System.Xml.Serialization.XmlAttributeAttribute("isCollection")]
    public bool IsCollection
    {
        get => _isCollection;
        set => _isCollection = value;
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private ItemKind _itemKind = ItemKind.Information;
        
    [System.ComponentModel.DefaultValueAttribute(ItemKind.Information)]
    [System.Xml.Serialization.XmlAttributeAttribute("itemKind")]
    public ItemKind ItemKind
    {
        get => _itemKind;
        set => _itemKind = value;
    }
}