
var orderAlbumModule = angular.module('orderAlbum', ['common'])
  .config(function ($routeProvider, $locationProvider) {
    $routeProvider.when(AlbumApp.rootPath + 'customer/orderAlbum',
            { templateUrl: AlbumApp.rootPath + 'Templates/Order.html',
              controller: 'OrderViewModel' });
    $routeProvider.when(AlbumApp.rootPath + 'customer/orderAlbum/albumList',
            { templateUrl: AlbumApp.rootPath + 'Templates/AlbumList.html',
              controller: 'AlbumListViewModel' });
    $routeProvider.otherwise({ redirectTo: AlbumApp.rootPath + 'customer/orderAlbum' });
    $locationProvider.html5Mode({ enabled: true, requireBase: false }); });

accountRegisterModule.controller("OrderAlbumViewModel",
    function ($scope, $http, $location, $window, viewModelHelper) {
      $scope.viewModelHelper = viewModelHelper;
      $scope.orderModel = new AlbumApp.OrderAlbumModel();
      $scope.previous = function () { $window.history.back(); } });

accountRegisterModule.controller("OrderViewModel",
    function ($scope, $http, $location, $window, viewModelHelper, validator) {
      viewModelHelper.modelIsValid = true; viewModelHelper.modelErrors = []; var OrderAlbumModelRules = [];
  
      var setupRules = function () {
        OrderAlbumModelRules.push(new validator.PropertyRule("Genre", {
          required: { message: "Genre is required" } }));   }
      
      $scope.step2 = function () {
        validator.ValidateModel($scope.orderModel, OrderAlbumModelRules);
        viewModelHelper.modelIsValid = $scope.orderModel.isValid;
        viewModelHelper.modelErrors = $scope.orderModel.errors;
        if (viewModelHelper.modelIsValid) { /* api/account/register/validate1 */
          viewModelHelper.apiPost('api/account/register/validate1', $scope.accountModelStep1,
            function (result) {
              $scope.accountModelStep1.Initialized = true;
              $location.path(AlbumApp.rootPath + 'account/register/step2');
            });
        }
        else viewModelHelper.modelErrors = $scope.accountModelStep1.errors;
      }
      setupRules();

    });

accountRegisterModule.controller("AlbumListViewModel",
    function ($scope, $http, $location, $window, viewModelHelper, validator) {

    });