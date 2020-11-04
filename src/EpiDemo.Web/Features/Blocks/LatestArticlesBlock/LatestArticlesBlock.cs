using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Validation.Internal;

namespace EpiDemo.Web.Features.Blocks
{
    [ContentType(GUID = "11865380-4529-4954-96A3-2137B9D7D2DC", DisplayName = "Latest Articles")]
    public class LatestArticlesBlock : BlockData
    {
        public virtual string Heading { get; set; }
    }
}