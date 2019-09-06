namespace DataBaseHandler.Models
{
    public class FileModel
    {
        public FileModel(string title)
        {
            Title = title;
        }

        public string Content { get; set; }

        public string Title { get; set; }
    }
}
