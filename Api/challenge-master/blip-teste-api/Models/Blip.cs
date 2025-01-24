

namespace blip_teste_api.Models
{
    public class CarouselItem
    {
        public Header? header { get; set; }
        public object[]? options { get; set; }
    }

    public class Header
    {
        public string? type { get; set; }
        public HeaderValue? value { get; set; }
    }

    public class HeaderValue
    {
        public string? title { get; set; }
        public string? text { get; set; }
        public string? type { get; set; }
        public string? uri { get; set; }
    }

    public class Option
    {
        public Label? label { get; set; }
        public object? value { get; set; }
    }

    public class Label
    {
        public string? type { get; set; }
        public object? value { get; set; }
    }

    public class WebLinkValue
    {
        public string? title { get; set; }
        public string? uri { get; set; }
    }

    public class JsonValue
    {
        public string? type { get; set; }
        public object? value { get; set; }
    }

    public class AdditionalInfo
    {
        public string? language { get; set; }
        public int? stars { get; set; }
    }

    public class ResponseObject
    {
        public string? itemType { get; set; }
        public List<CarouselItem>? items { get; set; }
    }

}
