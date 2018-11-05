var myApp = angular.module('myApp', ['ngAnimate', 'ngMaterial', 'tvlControllers']);

myApp.config(function($mdThemingProvider) {
  $mdThemingProvider.theme('default')
  .primaryPalette('green')
  .accentPalette('green');
});

/* States initialization */
myApp.config(function($locationProvider, $interpolateProvider) {
  $locationProvider.html5Mode({enabled: true, requireBase: false});

  $interpolateProvider.startSymbol('<%');
  $interpolateProvider.endSymbol('%>');

})
.run(function ($q, $http) {
  //TODO
});
