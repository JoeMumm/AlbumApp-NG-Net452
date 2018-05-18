
appMainModule.controller("AccountLoginViewModel", function ($scope, $http, viewModelHelper,
    validator) {
  
  $scope.viewModelHelper = viewModelHelper;
  $scope.accountModel = new AlbumApp.AccountLoginModel();
  $scope.returnUrl = '';

  var accountModelRules = [];

  var setupRules = function () {
    accountModelRules.push(new validator.PropertyRule("LoginEmail", {
      required: { message: "Login Email is required" }, 
      email: { message: "Login must be in email format" }
    }));
    var pwdLen = 6;
    accountModelRules.push(new validator.PropertyRule("Password", {
      required: { message: "Password is required"  },
      minLength: { message: "Password must be at least " + pwdLen + " characters", params: pwdLen }
    }));
  }

  $scope.login = function () {
    validator.ValidateModel($scope.accountModel, accountModelRules);
    viewModelHelper.modelIsValid = $scope.accountModel.isValid;
    viewModelHelper.modelErrors = $scope.accountModel.errors;
    if (viewModelHelper.modelIsValid) { /* api/account/login */
      viewModelHelper.apiPost('api/account/login', $scope.accountModel, null,
        function (result) {
          if ($scope.returnUrl != null && $scope.returnUrl.length > 1) {
            window.location.href = AlbumApp.rootPath + $scope.returnUrl.substring(1);
          } else window.location.href = AlbumApp.rootPath; });
    } else viewModelHelper.modelErrors = $scope.accountModel.errors;
  }
  setupRules();
});