namespace Meerkat.Dtos
{
    public class CreateFrameDto
    {
        public string Function { get; set; }
        public string Module { get; set; }
        public string ContextLine { get; set; }
        public bool InApp { get; set; }
        public int LineNumber { get; set; }
        public int ColumnNumber { get; set; }
        public string FileName { get; set; }
    }
}
