using AutoMapper;
using Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.AutoMapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMappingFromEntitiesToViewModels();
            CreateMappingFromViewModelsToEntities();

        }

        private void CreateMappingFromViewModelsToEntities()
        {
            CreateMap<UserCreateViewModel, ApplicationUser>();
            CreateMap<UserCreateViewModel, IdentityUser>();
            CreateMap<RoleCreateViewModel, IdentityRole>();
            CreateMap<RoomCreateViewModel, Room>();
            CreateMap<RoomTypeCreateViewModel, TypeRoom>();
        }

        private void CreateMappingFromEntitiesToViewModels()
        {
            // YOUR code
        }
    }
}