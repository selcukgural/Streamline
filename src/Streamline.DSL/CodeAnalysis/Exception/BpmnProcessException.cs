using Streamline.DSL.CodeAnalysis.Bpmn;

namespace Streamline.DSL.CodeAnalysis.Exception;

public sealed class BpmnProcessException(SyntaxKind kind) : System.Exception(
    $"Flow must start with 'Process' step. Process task not found. Excepted {SyntaxKind.BpmnProcessElement} but found {kind}.");
    