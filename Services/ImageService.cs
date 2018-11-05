using RealEyes.Amazon;
using Amazon.Rekognition.Model;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEyes.Services {
    class ImageService {
        private Rekognition amazonRekognition;
        public ImageService(string Base64string){
            Image amazonImage = new Image(){
                Bytes = new MemoryStream(GetImageMemoryStream(Base64string))
            };
                        
            // Image amazonImage = new Image(){
            //     S3Object = new S3Object(){
            //         Bucket = "dnugao",
            //         Name = "IMG_20170824_192125.jpg"
            //     }
            // };
            this.amazonRekognition = new Rekognition(amazonImage);
        }

        public async Task<List<FaceDetail>> FaceRekognition(bool ForDetailedAnalysis = true) {
            return await this.amazonRekognition.FaceAnalyser(ForDetailedAnalysis);
        }

        public static byte[] GetImageMemoryStream(string Base64string)
        {
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(Base64string);
            return bytes;
        }
    }
}