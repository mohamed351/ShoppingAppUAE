using AutoMapper;
using ShoppingAppUAE.Areas.API.DTOS.Categories;
using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.API.Profiler
{
    public class CategoriesProfiler:Profile
    {

        public CategoriesProfiler()
        {
            CreateMap<Category, CategoryGetDTO>()
                .ForMember(a => a.ArabicName, a => a.MapFrom(a => a.CategoryNameArabic))
                .ForMember(a => a.EnglishName, a => a.MapFrom(a => a.CategoryNameEnglish))
                .ForMember(a => a.CategoryID, a => a.MapFrom(a => a.CategoryID))
                .ForMember(a => a.ArabicDescription, a => a.MapFrom(a => a.CategoryDescriptionArabic))
                .ForMember(a => a.EnglishDescription, a => a.MapFrom(a => a.CategoryDescriptionEnglish));
        }
    }
}
