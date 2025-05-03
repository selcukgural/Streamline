namespace Streamline.DSL.CodeAnalysis.Exception;

public sealed class AttributeAlreadyExistsException(string identifier)
    : System.Exception($"Attribute '{identifier}' already exists.");