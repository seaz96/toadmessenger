namespace Logic.Models;

public record UpdateUserPhotoRequestModel
{
    public string ImageBase64 { get; set; } = null!;
}