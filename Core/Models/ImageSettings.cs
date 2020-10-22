using System.IO;
using System.Linq;

namespace maxapp.Core.Models
{
    public class ImageSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        
        
        public bool IsNotSupported(string fileName)
        {
            return AcceptedFileTypes.All(s => s != Path.GetExtension(fileName).ToLower());
        }
    }
}