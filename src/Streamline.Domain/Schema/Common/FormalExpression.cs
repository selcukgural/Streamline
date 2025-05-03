#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tFormalExpression", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("formalExpression", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class FormalExpression : Expression
{
        
    [System.Xml.Serialization.XmlAttributeAttribute("language")]
    public string Language { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("evaluatesToTypeRef")]
    public System.Xml.XmlQualifiedName EvaluatesToTypeRef { get; set; }
}