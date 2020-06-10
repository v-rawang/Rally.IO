using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class Attachment : Content
    {
        public string Url { get; set; }

        public ImageOutputFormat OutputFormat { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public int? Height { get; set; }

        public int? Width { get; set; }

        public long? Size { get; set; }

        public byte[] Bytes { get; set; }

        public bool IsTileImage { get; set; }
    }

    public enum ImageOutputFormat
    {
        BMP = 0,
        JPEG = 1,
        PNG = 2,
        GIF = 3
    }
}
