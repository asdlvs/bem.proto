using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;

namespace Dnevnik.Less
{
    internal static class LessContentTypeDefinition
    {
        [Name("LESS")]
        [Export(typeof(ContentTypeDefinition))]
        public static ContentTypeDefinition LessContentType { get; set; }

        [FileExtension(".less")]
        [Export(typeof(FileExtensionToContentTypeDefinition))]
        [ContentType("LESS")]
        public static FileExtensionToContentTypeDefinition LessFileExtension { get; set; }
    }
}
