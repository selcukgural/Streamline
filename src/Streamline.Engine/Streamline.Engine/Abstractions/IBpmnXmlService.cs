

// Namespace updated to Engine.Abstractions

using Streamline.Domain.Schema.Events;

namespace Streamline.Engine.Abstractions; 

/// <summary>
/// Defines the contract for importing and exporting BPMN 2.0.2 XML.
/// Resides in Engine.Abstractions as it's a core engine capability contract.
/// </summary>
public interface IBpmnXmlService
{
    /// <summary>
    /// Imports a BPMN 2.0.2 XML string and deserializes it into a Definitions object.
    /// </summary>
    /// <param name="xmlContent">The BPMN XML content as a string.</param>
    /// <returns>The deserialized Definitions object.</returns>
    /// <exception cref="System.ArgumentException">Thrown if the XML content is null or empty.</exception>
    /// <exception cref="System.InvalidOperationException">Thrown if XML deserialization fails.</exception>
    Definitions Import(string xmlContent);

    /// <summary>
    /// Exports a Definitions object into a BPMN 2.0.2 XML string.
    /// </summary>
    /// <param name="definitions">The Definitions object to serialize.</param>
    /// <returns>The BPMN XML content as a string.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown if the definitions object is null.</exception>
    /// <exception cref="System.InvalidOperationException">Thrown if XML serialization fails.</exception>
    string Export(Definitions definitions);
} 