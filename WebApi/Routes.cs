using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealEyes.WebApi {
    [Produces("application/json")]
    public class RouteHandler {
        private HttpContext context;
        private string path;
        private string apiVersion;
        private string requestMethod;
        public RouteHandler(HttpContext context) {
            this.context = context;
            this.path = context.Request.Path;
            this.apiVersion = "/api/v1";
            this.requestMethod = context.Request.Method;
        }

        public async void processRequest() {
            if( this.path == this.apiVersion + "/image/facerekognition") {
                if(this.requestMethod == "POST") {
                    var data = await context.Request.ReadFormAsync();
                    await context.Response.WriteAsync("Processing image... ->" + data);
                } else {
                    await context.Response.WriteAsync("you should send an image!\n" +
                    "(your request method is wrong on this path: " + this.requestMethod + ")");
                }

            } else {
                await context.Response.WriteAsync("The requested path \'" + this.path + "\' doesn\'t exist");
            }

        }
    }
}
