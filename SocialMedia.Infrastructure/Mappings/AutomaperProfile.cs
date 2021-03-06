using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SocialMedia.Core.Entities;
using SocialMedia.Infrastructure.DTOs;

namespace SocialMedia.Infrastructure.Mappings
{
    public class AutomaperProfile:Profile
    {
        //REGISTRAMOS TIPOS DE COMVERSIONES
        public AutomaperProfile()
        {
            //FUNCIONARA SI LOS ATRIBUTOS SE LLAMAN IGUAL
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
        }
    }
}
