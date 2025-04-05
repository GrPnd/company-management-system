using App.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class TicketViewModel
{
    public Ticket Ticket { get; set; } = default!;

    [ValidateNever]
    public SelectList FromUserSelectList { get; set; } = default!;

    [ValidateNever]
    public SelectList ToUserSelectList { get; set; } = default!;
}