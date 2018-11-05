angular.module('tvlServices', [])
.service('webcam', function($http, $q) {
  var videoNativeElement;
  var canvasNativeElement;

  return {
    init: function(videoElement){
      return $q(function(resolve, reject) {
        window.Realeyesit.EnvironmentDetector.detect()
        .then(function (result) {
          console.log("env detection", result);

          if(result.webcams.length > 0) {
            navigator.mediaDevices.getUserMedia({ video: true }).then(function(stream) {
              /* use the stream */

              //$scope.webcamEnabled = true;
              videoNativeElement = videoElement;

              console.log("stream", stream);

              videoNativeElement.srcObject = stream;
              resolve(stream);
            })
            .catch(function(err) {
              if(err.name === 'NotAllowedError'){
                $mdDialog.show(
                  $mdDialog.alert()
                  .clickOutsideToClose(true)
                  .title('Error occured')
                  .textContent("To continue exercise please refresh the page and allow your webcam!")
                  .ariaLabel('Error')
                  .ok('Okay')
                );
              } else {
                console.log("error", err);
                $mdDialog.show(
                  $mdDialog.alert()
                  .clickOutsideToClose(true)
                  .title('Error occured')
                  .textContent("You cannot perform the actions on the page, because an error: " + err.message)
                  .ariaLabel('Error')
                  .ok('Okay')
                );
              }

            });
          }
        }, function (err) {
          console.log('err: ' + err.toString());
          reject(err);
        });
      });
    },

    capture: function(canvasNativeElement, width, height){
      canvas = canvasNativeElement.getContext("2d");
      canvas.clearRect(0, 0, width, height);
      canvas.beginPath();

      canvas.drawImage(videoNativeElement, 0, 0, width, height);
      

      let dataUrl = canvasNativeElement.toDataURL('image/png');
      return dataUrl.split(",")[1];
      // return dataUrl.substr(22);
    }
  }
})

.service('apiServices', function($http, $q) {
  return {
    imageFaceRekognition: function(base64Image) {
      return $q(function(resolve, reject){
        $http.post("/image/facerekognition", {
          Base64: base64Image,
          ForDetailedAnalysis: true
        })
        .then(function(response){
          resolve(response.data);
        });
      })
    }
  };
})

.service('drawOnCanvas', function() {
  function giveHat (canvasNativeElement, top, left, width){
    var ctx = canvasNativeElement.getContext("2d");
    var hat = document.getElementById("hat");
    const hatOriginalWidth = 1540;
    const hatOriginalHeight = 1187;
    
    const ratioDifference = width / hatOriginalWidth;
    const proportionHeight = hatOriginalHeight * ratioDifference;

    ctx.drawImage(hat, top, left - proportionHeight + 10,  width, proportionHeight);
  };

  return {
    boundaryBox: function(canvasNativeElement, top, left, width, height, color = "red", personWithHat = false) {
      console.log("drawing boundary box");
      console.log("top", top);
      console.log("left", left);
      console.log("width", width);
      console.log("height", height);
      var ctx = canvasNativeElement.getContext("2d");
      ctx.beginPath();
      ctx.strokeStyle=color;
      ctx.rect(top,left,width,height);
      ctx.stroke();

      if(personWithHat) {
        giveHat(canvasNativeElement, top, left, width);
      }
    }
  };
})
;
