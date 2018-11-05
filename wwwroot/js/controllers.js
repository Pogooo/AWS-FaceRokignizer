angular.module('tvlControllers', ['tvlServices'])
.controller('HeaderCtrller', function($scope)
{
  //TODO
})
.controller('MainpageCtrller', function($scope, webcam, drawOnCanvas, apiServices)
{
  $scope.webcamEnabled = false;
  $scope.videoWidth = 640;
  $scope.videoHeight = 480;
  $scope.captured = false;
  $scope.capture;
  $scope.boundaryColors = [
    "green","blue", "Aqua", "Yellow", "SpringGreen", "Purple", 
    "Plum", "PaleVioletRed", "MediumVioletRed", "Magenta", "Cyan", "Black"
  ];
  $scope.facesOnImage = [];
  $scope.requireHat = false;

  $scope.init = function() {
    let videoNativeElement = document.querySelector("#videoElement");

    webcam.init(videoNativeElement).then(function(result){
      $scope.webcamEnabled = true;
    });

  }

  $scope.recognize = function(){
    $scope.captured = true;
    var canvasNativeElement = document.querySelector("#canvas");

    $scope.capture = webcam.capture(canvasNativeElement, $scope.videoWidth, $scope.videoHeight);

    apiServices.imageFaceRekognition($scope.capture).then(function(result){
      console.log("rekognize amazon response", result);
      $scope.facesOnImage = result;

      //draw boundary box around the faces
      let boundaryColorIndex = 0;

      //FOR THE HAT
      const personWithTheHatIndex = Math.floor(Math.random() * result.length);
      i = 0;
      //END HAT

      $scope.facesOnImage.forEach(face => {

        const top = Math.round( $scope.videoHeight * face.BoundingBox.Top );
        const left = Math.round( $scope.videoWidth * face.BoundingBox.Left );
        const width = Math.round( $scope.videoWidth * face.BoundingBox.Width );
        const height = Math.round( $scope.videoHeight * face.BoundingBox.Height );

        //FOR THE HAT
        var personWithTheHat = false;
        if(personWithTheHatIndex == i++ && $scope.requireHat == true) {
          personWithTheHat = true;
        }
        //END HAT
        
        drawOnCanvas.boundaryBox(canvasNativeElement, left, top, width, height, $scope.boundaryColors[boundaryColorIndex++], personWithTheHat);

      });
    });
  };
  $scope.init();
});
