using System.ComponentModel.DataAnnotations;
using System.Net;

namespace App.DTO.v1.Identity;

public class RestApiErrorResponse
{
    public HttpStatusCode Status { get; set; }
    
    [MaxLength(128)]
    public string Error { get; set; } = default!;
}