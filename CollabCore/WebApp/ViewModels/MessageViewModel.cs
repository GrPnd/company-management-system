using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class MessageViewModel
{
    public Message Message { get; set; } = default!;

    [ValidateNever]
    public SelectList FromUserSelectList { get; set; } = default!;

    [ValidateNever]
    public SelectList ToUserSelectList { get; set; } = default!;
}