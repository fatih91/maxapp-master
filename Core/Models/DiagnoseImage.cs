namespace maxapp.Core.Models
{
    public class DiagnoseImage
    {
        public int DiagnoseId { get; set; }
        public string FileName { get; set; }

        public Diagnose Diagnose { get; set; }
        public Image Image { get; set; }
    }
}