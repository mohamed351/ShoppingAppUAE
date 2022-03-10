using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Helpers
{
    public static class ImageHelpers
    {

        public static string ConvertMainImage(string imageBase64, IWebHostEnvironment environment)
        {
            var array = Convert.FromBase64String(imageBase64.Split(";base64,")[1]);
            string imageName = Guid.NewGuid().ToString() + ".png";
            using (FileStream file = new FileStream($"{environment.WebRootPath}//images//{imageName}", FileMode.CreateNew, FileAccess.Write))
            {
                file.Write(array, 0, array.Length);
            }
            return imageName;

        }
        //public static string ConvertImageFile(IFormFile file, IWebHostEnvironment environment)
        //{
        //    string imageName = Guid.NewGuid().ToString() + ".png";

        //    using (var imag = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
        //    {

        //        imag.Mutate(x => x.Resize(1200, 800));
        //        imag.Save($"{environment.WebRootPath}//images//{imageName}");
        //    }
        //    return imageName;
        //}
    }
}
