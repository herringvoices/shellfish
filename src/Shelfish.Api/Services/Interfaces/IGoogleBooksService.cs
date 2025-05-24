namespace Shelfish.Api.Services.Interfaces;

public interface IGoogleBooksService
{
    Task<GoogleBook?> FetchAsync(string isbn);
}

/* Simple object graph for the parts we care about */
public class GoogleBook
{
    public VolumeInfo VolumeInfo { get; set; } = new();
    public class VolumeInfo
    {
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public List<string>? Authors { get; set; }
        public string? Description { get; set; }
        public ImageLinks? ImageLinks { get; set; }
        public class ImageLinks
        {
            public string? SmallThumbnail { get; set; }
            public string? Thumbnail { get; set; }
        }
    }
}
