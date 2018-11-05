using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEyes.Services;
using Amazon.Rekognition.Model;
using Newtonsoft.Json;

namespace RealEyes.Controllers
{
    public class ImageController : Controller
    {

        [HttpPost]
        public async Task<string> FaceRekognition([FromBody] Image image)
        {
            // return image.Base64;
            ImageService imageService = new ImageService(image.Base64);
            List<FaceDetail> response = await imageService.FaceRekognition(image.ForDetailedAnalysis);

            return JsonConvert.SerializeObject(response);

            foreach (FaceDetail detail in response)
            {
                System.Console.WriteLine("Height: " + detail.BoundingBox.Height);
                System.Console.WriteLine("Width: " + detail.BoundingBox.Width);
                System.Console.WriteLine("Top: " + detail.BoundingBox.Top);
                System.Console.WriteLine("Left: " + detail.BoundingBox.Left);
                System.Console.WriteLine("Gender: " + detail.Gender.Value);
                return "gender: " + detail.Gender.Value;
            }

            return "dsfsdf";
            
        }
    }

    public class Image
    {
        public string Base64 { get; set; }
        public bool ForDetailedAnalysis { get; set; }
    }
}
