using MongoDB.Bson;

namespace SwitchAPI.Models
{
    public class FilesModel
    {
        public ObjectId Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public DateTime uploadDate { get; set; }
    }
}
