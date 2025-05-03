using Microsoft.AspNetCore.Mvc;
// using Streamline.Domain.Models.BPMN; // Removed incorrect using
// Changed from Infrastructure.Services
using Streamline.Engine.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Text;
using Streamline.Domain.Schema.Events;

namespace Streamline.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class BpmnController(IBpmnXmlService bpmnXmlService, ILogger<BpmnController> logger)
    : ControllerBase
{
    /// <summary>
    /// Imports a BPMN 2.0.2 XML provided as raw text in the request body.
    /// </summary>
    /// <remarks>
    /// Reads the raw request body as a UTF-8 encoded BPMN XML string.
    /// Example request body:
    /// 
    ///     <?xml version="1.0" encoding="UTF-8"?>
    ///     <bpmn:definitions xmlns:bpmn="http://www.omg.org/spec/BPMN/20100524/MODEL" ...>
    ///         ...
    ///     </bpmn:definitions>
    ///
    /// </remarks>
    /// <returns>An ActionResult containing the ID of the imported definitions on success, or an error response.</returns>
    /// <response code="200">Returns the ID of the successfully imported BPMN definitions.</response>
    /// <response code="400">If the request body is empty or the XML is invalid/malformed.</response>
    /// <response code="500">If an unexpected server error occurs.</response>
    [HttpPost("import")]
    [Consumes("application/xml", "text/xml")] // Explicitly allow XML content types
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ImportXmlFromBody()
    {
        string xmlContent;
        try
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                xmlContent = await reader.ReadToEndAsync();
            }

            if (string.IsNullOrWhiteSpace(xmlContent))
            {
                logger.LogWarning("Import request received with empty body.");
                return BadRequest("Request body cannot be empty.");
            }

            logger.LogInformation("Attempting to import BPMN XML.");
            var definitions = bpmnXmlService.Import(xmlContent);
            logger.LogInformation("Successfully imported BPMN definitions.");
                
            // Return the ID of the definitions object
            return Ok("Successfully imported BPMN definitions.");
        }
        catch (ArgumentException ex)
        { 
            logger.LogWarning(ex, "Invalid argument during XML import.");
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex) // Catches deserialization errors
        {
            logger.LogError(ex, "Failed to deserialize BPMN XML.");
            return BadRequest($"Invalid BPMN XML: {ex.Message} (Inner: {ex.InnerException?.Message})");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred during BPMN XML import.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }

    /// <summary>
    /// Exports a given BPMN tDefinitions object (provided as JSON in the request body) to its XML string representation.
    /// </summary>
    /// <remarks>
    /// Note: This endpoint expects the full tDefinitions object graph serialized as JSON.
    /// This is mainly for testing the export functionality.
    /// </remarks>
    /// <param name="definitions">The tDefinitions object (from JSON body).</param>
    /// <returns>The BPMN 2.0.2 XML string.</returns>
    /// <response code="200">Returns the BPMN XML as a string.</response>
    /// <response code="400">If the provided definitions object is null or invalid.</response>
    /// <response code="500">If an unexpected server error occurs during serialization.</response>
    [HttpPost("export")]
    [Produces(MediaTypeNames.Application.Xml)] // Corrected produces type
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult ExportXml([FromBody][Required] Definitions definitions)
    {
        // Basic validation already handled by [Required] and model binding
        // if (definitions == null) <-- Not strictly needed due to [Required]
        // {
        //     return BadRequest("Definitions object cannot be null.");
        // }

        try
        {
            logger.LogInformation("Attempting to export BPMN definitions to XML.");
            string xmlResult = bpmnXmlService.Export(definitions);
            logger.LogInformation("Successfully exported BPMN definitions.");
            // Return the XML string directly. Set the Content-Type header.
            return Content(xmlResult, MediaTypeNames.Application.Xml, Encoding.UTF8);
        }
        catch (ArgumentNullException ex)
        {
            logger.LogWarning(ex, "Null argument provided for export.");
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex) // Catches serialization errors
        {
            logger.LogError(ex, "Failed to serialize tDefinitions to XML.");
            return StatusCode(StatusCodes.Status500InternalServerError, $"XML Serialization failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred during BPMN XML export.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }
}