namespace AnnouncementsApiFunctionsApp.Dtos
{
    public class UpdateAnnouncementRequest
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string Content { get; set; }

        public string AnnouncementType { get; set; }
    }
}
