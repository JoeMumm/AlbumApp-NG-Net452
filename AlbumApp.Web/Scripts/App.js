
var commonModule = angular.module('common', ['ngRoute', 'ui.bootstrap']);

// Non-SPA views will use Angular controllers created on the appMainModule.
var appMainModule = angular.module('appMain', ['common']);
//, 'ngTouch', 'ui.grid.cellNav' 'ui.grid.selection', 

var appAlbumModule = angular.module('appAlbum', ['common', 'ui.grid', 'ui.grid.pagination']);

var appCartModule = angular.module('appCart', ['common']);

// SPA-views will attach to their own module and use their own data-ng-app and nested controllers.
// Each MVC-delivered top-spa-level view will link its needed JS files.

// Services attached to the commonModule will be available to all other Angular modules.

commonModule.factory('viewModelHelper', function ($http, $q) {
  return AlbumApp.viewModelHelper($http, $q);
});

commonModule.factory('validator', function () {
  return valJs.validator();
});

(function (aa) {
  var viewModelHelper = function ($http, $q) {

    var self = this;

    self.modelIsValid = true;
    self.modelErrors = [];
    self.isLoading = false;   

    self.apiGet = function (uri, config, success, failure, always) {
      self.isLoading = true;
      self.modelIsValid = true;
      $http.get(AlbumApp.rootPath + uri, config)
          .then(function (result) {
            success(result);
            if (always != null)
              always();
            self.isLoading = false;
          }, function (result) {
            if (failure == null) {
              if (result.status != 400)
                self.modelErrors = [result.status + ': ' + result.statusText +
                  ' - ' + result.data];
              else
                self.modelErrors = [result.data + ''];
              self.modelIsValid = false;
            }
            else
              failure(result);
            if (always != null)
              always();
            self.isLoading = false;
          });
    }

    self.apiPost = function (uri, data, config, success, failure, always) {
      self.isLoading = true;
      self.modelIsValid = true;
      $http.post(AlbumApp.rootPath + uri, data, config)
          .then(function (result) {
            success(result);
            if (always != null)
              always();
            self.isLoading = false;
          }, function (result) {
            if (failure == null) {
              if (result.status != 400)
                self.modelErrors = [result.status + ': ' + result.statusText + ' - ' + result.data];
              else self.modelErrors = [result.data];
              self.modelIsValid = false;
            }
            else failure(result);
            if (always != null) always();
            self.isLoading = false;
          });
    }
    return this;
  }  
  aa.viewModelHelper = viewModelHelper;
}(window.AlbumApp));

(function (aa) {
  var mustEqual = function (value, other) {
    return value == other;
  }
  aa.mustEqual = mustEqual;
}(window.AlbumApp));

// ***************** validation *****************

window.valJs = {};

(function (val) {
  var validator = function () {

    var self = this;

    self.PropertyRule = function (propertyName, rules) {
      var self = this;
      self.PropertyName = propertyName;
      self.Rules = rules;
    };

    self.ValidateModel = function (model, allPropertyRules) {
      var errors = [];
      var props = Object.keys(model);
      for (var i = 0; i < props.length; i++) {
        var prop = props[i];
        for (var j = 0; j < allPropertyRules.length; j++) {
          var propertyRule = allPropertyRules[j];
          if (prop == propertyRule.PropertyName) {
            var propertyRules = propertyRule.Rules;

            var propertyRuleProps = Object.keys(propertyRules);
            for (var k = 0; k < propertyRuleProps.length; k++) {
              var propertyRuleProp = propertyRuleProps[k];
              if (propertyRuleProp != 'custom') {
                var rule = rules[propertyRuleProp];
                var params = null;
                if (propertyRules[propertyRuleProp].hasOwnProperty('params'))
                  params = propertyRules[propertyRuleProp].params;
                var validationResult = rule.validator(model[prop], params);
                if (!validationResult) {
                  errors.push(getMessageDefault(propertyRule, propertyRuleProp, rule.message));
                }
              } else {
                var validator = propertyRules.custom.validator;
                var value = null;
                if (propertyRules.custom.hasOwnProperty('params')) {
                  value = propertyRules.custom.params; }
                var result = validator(model[prop], value());
                if (result != true) {
                  errors.push(getMessageCustom(prop, propertyRules.custom, 'Invalid value.'));
                }
              }
            }
          }
        }
      }
      // this apparently adds an 'errors' and 'isValid' property to the model passed in
      model['errors'] = errors;
      model['isValid'] = (errors.length == 0);
    }
    
    var getMessageDefault = function (propertyRule, propertyRuleProp, defaultMessage) {
      var message = '';
      switch (propertyRuleProp) { // Need case for each rules in setupRules
        case 'required': message = propertyRule.Rules.required.message; break;
        case 'minLength': message = propertyRule.Rules.minLength.message; break;
        case 'pattern': message = propertyRule.Rules.pattern.message; break;
        case 'email': message = propertyRule.Rules.email.message; break;
        default: message = ''; }
      if (message) { return message; }
      return defaultMessage; }

    var getMessageCustom = function (prop, rule, defaultMessage) {
      var message = '';
      if (rule.hasOwnProperty('message'))
        message = rule.message;
      else
        message = prop + ': ' + defaultMessage;
      return message;
    }

    var rules = [];

    var setupRules = function () {

      rules['required'] = {
        validator: function (value, params) {
          return !(value.trim() == '');
        },
        message: 'Value is required.'
      };
      rules['minLength'] = {
        validator: function (value, params) {
          return !(value.trim().length < params);
        },
        message: 'Value does not meet minimum length.'
      };
      rules['pattern'] = {
        validator: function (value, params) {
          var regExp = new RegExp(params);
          return !(regExp.exec(value.trim()) == null);
        },
        message: 'Value must match regular expression.'
      };
      rules['email'] = {
        validator: function (value, params) {
          var regExp = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>() \[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
          return !(regExp.exec(value.trim()) == null);
        },
        message: 'Value must be an email.'
      };
    }

    setupRules();

    return this;
  }
  val.validator = validator;
}(window.valJs));
