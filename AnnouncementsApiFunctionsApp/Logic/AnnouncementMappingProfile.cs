using AnnouncementsApiFunctionsApp.Dtos;
using AutoMapper;

namespace AnnouncementsApiFunctionsApp
{
    public class AnnouncementMappingProfile:Profile
    {
        public AnnouncementMappingProfile()
        {
            CreateMap<Announcement, AnnouncementResponse>();
        }
    }
}
