using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Asp.Versioning;


namespace WebApp.ApiControllers.Translation;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class LocalizationController : ControllerBase
{
    private readonly ILogger<LocalizationController> _logger;

    public LocalizationController(ILogger<LocalizationController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get(string language, string resource)
    {
        try
        {
            _logger.LogInformation("Requesting translations for: resource={Resource}, language={Language}", resource, language);

            var culture = new CultureInfo(language);

            var resolved = ResolveResourceInfo(resource);
            if (resolved == null)
            {
                _logger.LogWarning("Unable to resolve assembly for resource '{Resource}'", resource);
                return NotFound(new { error = "Resource not found." });
            }

            var (resourceName, assembly) = resolved.Value;

            _logger.LogInformation("Resolved resource = {ResourceName}, Assembly = {AssemblyName}", resourceName, assembly.GetName().Name);

            var manager = new ResourceManager(resourceName, assembly);
            var resourceSet = manager.GetResourceSet(culture, true, true);

            if (resourceSet == null)
            {
                _logger.LogWarning("ResourceSet was null for resource '{Resource}' and culture '{Culture}'", resourceName, culture.Name);
                return NotFound(new { error = "Resource not found." });
            }

            var translations = new Dictionary<string, string>();
            foreach (DictionaryEntry entry in resourceSet)
            {
                translations[entry.Key.ToString()!] = entry.Value?.ToString() ?? "";
            }

            return Ok(translations);
        }
        catch (CultureNotFoundException)
        {
            return BadRequest(new { error = "Invalid language code." });
        }
        catch (MissingManifestResourceException ex)
        {
            _logger.LogError(ex, "MissingManifestResourceException");
            return NotFound(new { error = "Resource not found." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading translations");
            return StatusCode(500, new { error = "Internal error" });
        }
    }

    private (string ResourceName, Assembly Assembly)? ResolveResourceInfo(string resource)
    {
        if (string.IsNullOrWhiteSpace(resource)) return null;

        return ($"App.Resources.Domain.{resource}", typeof(App.Resources.ResourceAnchor).Assembly);
    }
}