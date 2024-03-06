namespace Mediator.API.Model;

public class Link
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; }
    public string ShortenedUrl { get; set; }
    public int UserId { get; set; }

    public User User { get; set; }
}