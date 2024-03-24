using System.Text.Json.Serialization;

namespace Mediator.API.Model;

public class Link
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; }
    public string ShorteredUrl { get; set; }
    public int UserId { get; set; }

    [JsonIgnore] public User User { get; set; }
}