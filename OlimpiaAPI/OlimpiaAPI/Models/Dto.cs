namespace OlimpiaAPI.Models
{
    public record CreatePlayerDto(string? Name, sbyte? Age, int? Height, int? Weight);
    public record UpdatePlayerDto(string? Name, sbyte? Age, int? Height, int? Weight);

    public record CreateDataDto(string? Country, string? County, string? Description, Guid? PlayerId);
    public record UpdateDataDto(string? Country, string? County, string? Description, Guid? PlayerId);
}
