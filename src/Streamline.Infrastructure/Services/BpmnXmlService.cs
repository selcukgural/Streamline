using System.Xml.Serialization;
using System.Text;
using Streamline.Domain.Schema.Events;
using Streamline.Engine.Abstractions;

namespace Streamline.Infrastructure.Services;

/// <summary>
/// Service for importing and exporting BPMN 2.0.2 XML files using generated C# classes.
/// </summary>
public class BpmnXmlService : IBpmnXmlService
{
    private static readonly XmlSerializer DefinitionsSerializer = new(typeof(Definitions));

    /// <inheritdoc />
    public Definitions Import(string xmlContent)
    {
        if (string.IsNullOrEmpty(xmlContent))
        {
            throw new ArgumentException("XML content cannot be null or empty.", nameof(xmlContent));
        }

        try
        {
            using var reader = new StringReader(xmlContent);
            var definitions = (Definitions?)DefinitionsSerializer.Deserialize(reader);
            if (definitions == null)
            {
                throw new InvalidOperationException("Failed to deserialize BPMN XML into Definitions object.");
            }
            return definitions;
        }
        catch (InvalidOperationException ex)
        {
            // Log the error details? Wrap in a custom exception?
            throw new InvalidOperationException("Error deserializing BPMN XML.", ex);
        }
        catch (Exception ex)
        {
            // Catch other potential exceptions during StringReader usage etc.
            throw new InvalidOperationException("An unexpected error occurred during BPMN import.", ex);
        }
    }

    /// <inheritdoc />
    public string Export(Definitions definitions)
    {
        ArgumentNullException.ThrowIfNull(definitions);

        try
        {
            var sb = new StringBuilder();
            using (var writer = new StreamlineWriterUtf8(sb))
            {
                // Consider adding namespaces for proper BPMN 2.0 XML output if needed
                DefinitionsSerializer.Serialize(writer, definitions);
            }
            return sb.ToString();
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("Error serializing Definitions object to BPMN XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred during BPMN export.", ex);
        }
    }
}

internal sealed class StreamlineWriterUtf8(StringBuilder stringBuilder) : StringWriter(stringBuilder)
{
    public override Encoding Encoding => Encoding.UTF8;
}