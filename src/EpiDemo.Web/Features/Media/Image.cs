using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;

namespace EpiDemo.Web.Features.Media
{
    [MediaDescriptor(ExtensionString = "jpg,jpeg,png,gif")]
    [ContentType(GUID = "3AD7E3F5-F969-4B35-B091-BDDE341AAEAD", DisplayName = "Image")]
    public class Image : MediaData
    {
        public virtual string Copyright { get; set; }
    }
}