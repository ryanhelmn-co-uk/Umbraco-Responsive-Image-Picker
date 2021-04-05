namespace ResponsiveImagePicker.Models
{
    public class Crop
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int BreakPoint { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}