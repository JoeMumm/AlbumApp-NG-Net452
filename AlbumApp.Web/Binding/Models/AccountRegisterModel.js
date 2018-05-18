
(function (aa) {
  var AccountRegisterModelStep1 = function () {
    var self = this;
    self.FirstName = '';    self.LastName = '';    self.Address = ''; 
    self.City = ''; self.State = ''; self.ZipCode = '';
    self.Initialized = false;   }
  aa.AccountRegisterModelStep1 = AccountRegisterModelStep1;
}(window.AlbumApp));

(function (aa) {
  var AccountRegisterModelStep2 = function () {
    var self = this;
    self.LoginEmail = '';      self.Password = '';
    self.PasswordConfirm = '';       self.Initialized = false;    }
  aa.AccountRegisterModelStep2 = AccountRegisterModelStep2;
}(window.AlbumApp));

(function (aa) {
  var AccountRegisterModelStep3 = function () {
    var self = this;
    self.CreditCard = ''; self.ExpDate = '';  
    self.Initialized = false;      }
  aa.AccountRegisterModelStep3 = AccountRegisterModelStep3;
}(window.AlbumApp));