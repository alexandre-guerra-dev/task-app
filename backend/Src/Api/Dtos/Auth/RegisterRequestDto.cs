using System.ComponentModel.DataAnnotations;

namespace Api.Src.Api.Dtos.Auth;

public record RegisterRequestDto(
    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [MinLength(6)]
    string Password
);