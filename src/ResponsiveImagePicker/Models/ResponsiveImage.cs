using System.Collections.Generic;

namespace ResponsiveImagePicker.Models
{
    public class ResponsiveImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public List<Crop> Crops { get; set; }
        public FocalPoint FocalPoint { get; set; }
        public string DefaultPictureClasses { get; set; }
        public string DefaultImageClasses { get; set; }
    }
}