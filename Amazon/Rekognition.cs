using System;
using System.Configuration;
using System.Collections.Generic;
using Amazon.Rekognition;
using Amazon;
using Amazon.Rekognition.Model;
using System.Threading.Tasks;
using System.Threading;

namespace RealEyes.Amazon {
    public class Rekognition {
        private AmazonRekognitionClient recognitionClient;
        private Image image;

        public Rekognition(Image image){
            this.initClient();
            this.image = image;
        }

        private void initClient() {
            var appSettings = ConfigurationManager.AppSettings;

            string rootFolder = System.IO.Directory.GetCurrentDirectory();
            string configFilePath = rootFolder + System.IO.Path.DirectorySeparatorChar + appSettings["AWSCredentialsName"];

            Console.WriteLine("configFilePath: " + configFilePath);

            string[] lines = System.IO.File.ReadAllLines(@"credentials");

            string awsAccesKey = "";
            string awsSecretKey = "";
            
            foreach(string line in lines) {
                string[] environmentEntity = line.Split('=');
                if(environmentEntity[0] == "AWS_ACCESS_KEY_ID") {
                    awsAccesKey = environmentEntity[1];
                } else if (environmentEntity[0] == "AWS_SECRET_ACCESS_KEY") {
                    awsSecretKey = environmentEntity[1];
                }
            }

            
            this.recognitionClient = new AmazonRekognitionClient(awsAccesKey,
            awsSecretKey, RegionEndpoint.EUWest1);
        }

        public async Task<List<Label>> DetectLabels() {

            CancellationTokenSource cts = new CancellationTokenSource();

            DetectLabelsRequest detectionRequest = new DetectLabelsRequest(){
                MinConfidence = (float)80,
                MaxLabels = 10,
                Image = this.image
            };  

            DetectLabelsResponse response = await recognitionClient.DetectLabelsAsync(detectionRequest, cts.Token);

            return response.Labels;
        }

        public async Task<List<FaceDetail>> FaceAnalyser(bool fullAnalyzing = false) {

            CancellationTokenSource cts = new CancellationTokenSource();

            DetectFacesRequest detectFacesRequest = new DetectFacesRequest(){
                Attributes = new List<string>(){
                    fullAnalyzing ? "ALL" : "DEFAULT"
                },
                Image = this.image
            };

            DetectFacesResponse response = await recognitionClient.DetectFacesAsync(detectFacesRequest, cts.Token);

            return response.FaceDetails;
        }
    }
}