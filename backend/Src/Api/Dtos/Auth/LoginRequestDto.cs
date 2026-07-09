using System.ComponentModel.DataAnnotations;

namespace Api.Src.Api.Dtos.Auth;

public record LoginRequestDto(
    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [MinLength(6)]
    string Password
);
