using AnnouncementsApiFunctionsApp.Domain;
using AnnouncementsApiFunctionsApp.Dtos;
using AutoMapper;

namespace AnnouncementsApiFunctionsApp
{
    public class AnnouncementMappingProfile : Profile
    {
        public AnnouncementMappingProfile()
        {
            CreateMap<Announcement, AnnouncementResponse>();

            CreateMap<AddAnnouncementRequest, Announcement>();

            CreateMap<UpdateAnnouncementRequest, Announcement>()
                .ConvertUsing((src, dest) =>
                {
                    if (src == null)
                    {
                        return null;
                    }

                    dest.Title = src.Title;
                    dest.Content = src.Content;
                    dest.AnnouncementType = src.AnnouncementType;

                    return dest;
                });
        }
    }
}
