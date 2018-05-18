using AlbumApp.Web.Core;
using AlbumApp.Web.Models;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System;
using System.Text.RegularExpressions;
using System.Web.Http.Results;

namespace AlbumApp.Web.Controllers.API
{
  [RoutePrefix("api/account")]
  public class AccountApiController : ApiControllerBase
  {
    ISecurityAdapter _SecurityAdapter;

    public AccountApiController(ISecurityAdapter securityAdapter)
    { _SecurityAdapter = securityAdapter; }

    [HttpPost]
    [Route("login")]
    public IHttpActionResult  Login(
                      [FromBody]AccountLoginModel accountModel ) {
      return GetHttpResponse(Request, () => {
        //var cartCount = 0;
        bool success = _SecurityAdapter.Login(accountModel.LoginEmail,
                         accountModel.Password, accountModel.RememberMe); // ref cartCount, 
        if (success) {
          return Content(HttpStatusCode.OK, "Authenticated"); }
        else return Content(HttpStatusCode.Unauthorized, "Unauthorized login.");
      }); }

    [HttpPost]
    [Route("register/validate1")]
    public IHttpActionResult ValidateRegistrationStep1(
                  [FromBody]AccountRegisterModel accountModel) {
      return GetHttpResponse(Request, () => {
        List<string> errors = ValidateRegistrationStep1Inner(accountModel);

        if (errors.Count == 0) return Content(HttpStatusCode.OK, "Register Model-1 Valid");
        else return Content(HttpStatusCode.BadRequest, errors.ToArray()); }); }

    private List<string> ValidateRegistrationStep1Inner(AccountRegisterModel accountModel) {
      List<string> errors = new List<string>();

      if (!IsAlphaNum(accountModel.FirstName)) errors.Add("Invalid First Name.");
      if (!IsAlphaNum(accountModel.LastName)) errors.Add("Invalid Last Name.");
      if (!IsAlphaNum(accountModel.Address)) errors.Add("Invalid Address.");
      if (!IsAlphaNum(accountModel.City)) errors.Add("Invalid City.");
      if (!IsDigits(accountModel.ZipCode, 5)) errors.Add("Invalid Zip.");
      if (!string.IsNullOrWhiteSpace(accountModel.State)) {
        List<State> states = UIHelper.GetStates();
        State state = states.FirstOrDefault(item =>
          item.Abbrev.ToUpper() == accountModel.State.ToUpper());      
        if (state == null) errors.Add("Invalid state.");
      } else errors.Add("State is required.");

      return errors; } 

    [HttpPost]
    [Route("register/validate2")]
    public IHttpActionResult ValidateRegistrationStep2(
                [FromBody]AccountRegisterModel accountModel) {
      return GetHttpResponse(Request, () => {
        List<string> errors = ValidateRegistrationStep2Inner(accountModel);
        
        if (errors.Count == 0) return Content(HttpStatusCode.OK, "Register Model-2 Valid");
        else return Content(HttpStatusCode.BadRequest, errors.ToArray()); }); }

    private List<string> ValidateRegistrationStep2Inner(AccountRegisterModel accountModel) {
      List<string> errors = new List<string>();
      if (!string.IsNullOrWhiteSpace(accountModel.LoginEmail)) {            
        var regExp = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        if (!Regex.IsMatch(accountModel.LoginEmail, regExp, RegexOptions.IgnoreCase))
          errors.Add("Email format is not valid");
        if (_SecurityAdapter.UserExists(accountModel.LoginEmail))
          errors.Add("An account is already registered with this email address.");
      } else errors.Add("LoginEmail is required");  

      if (String.IsNullOrWhiteSpace(accountModel.Password) ||
          accountModel.Password.Length < 6) errors.Add("Invalid Password");
      if (String.IsNullOrWhiteSpace(accountModel.PasswordConfirm) ||
                accountModel.Password != accountModel.PasswordConfirm)
          errors.Add("Passwords do not match");

      return errors; }

    [HttpPost]
    [Route("register/validate3")]
    public IHttpActionResult ValidateRegistrationStep3(
                [FromBody]AccountRegisterModel accountModel) {
      return GetHttpResponse(Request, () => {
        List<string> errors = ValidateRegistrationStep3Inner(accountModel);

        if (errors.Count == 0) return Content(HttpStatusCode.OK, "Register Model-3 Valid");
        else return Content(HttpStatusCode.BadRequest, errors.ToArray()); }); }

    private List<string> ValidateRegistrationStep3Inner(AccountRegisterModel accountModel)
    {
      List<string> errors = new List<string>();

      if (!IsDigits(accountModel.CreditCard, 16)) errors.Add("CreditCard needs to be 16 digits.");
      if (!string.IsNullOrWhiteSpace(accountModel.ExpDate)) {
        var regExp = @"^(0[1-9]|1[0-2])\/[0-9]{2}$";
        if (!Regex.IsMatch(accountModel.ExpDate, regExp, RegexOptions.IgnoreCase))
          errors.Add("Expiration Date is not valid format (MM/YY)");
      } else errors.Add("Expiration Date is required (MM/YY)");
      
      return errors; }


    [HttpPost]
    [Route("register")]
    public IHttpActionResult CreatAccount([FromBody]AccountRegisterModel accountModel) {
            return GetHttpResponse(Request, () => {
        if (ValidateRegistrationStep1Inner(accountModel).Count == 0 &&
            ValidateRegistrationStep2Inner(accountModel).Count == 0 &&
            ValidateRegistrationStep3Inner(accountModel).Count == 0) {
          var accountRegisterModel = new AccountRegisterModel {
            FirstName = accountModel.FirstName, LastName = accountModel.LastName,
            Address = accountModel.Address, City = accountModel.City,
            State = accountModel.State, ZipCode = accountModel.ZipCode,
            LoginEmail = accountModel.LoginEmail, CreditCard = accountModel.CreditCard,
            ExpDate = accountModel.ExpDate.Substring(0, 2) + accountModel.ExpDate.Substring(3, 2) };
          _SecurityAdapter.Register(accountModel.Password, accountRegisterModel);
          _SecurityAdapter.Login(accountModel.LoginEmail, accountModel.Password, false);
          return Content(HttpStatusCode.OK, "Account Created"); }
        else return Content(HttpStatusCode.BadRequest, "Account Creation Failed"); }); }

    private bool IsAlphaNum(string field) {
      if (String.IsNullOrWhiteSpace(field)) return false;
      foreach (char ch in field)
                if (!Regex.IsMatch(field, @"^\w+")) return false;
      return true; }

    private bool IsDigits(string field, int len) {
      if (String.IsNullOrWhiteSpace(field)) return false;
      if (!Regex.IsMatch(field, @"^[0-9]{" + len + "}$")) return false;
      return true; }

    [HttpGet]
    [Route("getaccountid")]
    public IHttpActionResult GetAccountId() {
      return GetHttpResponse(Request, () => {
        var accountId = _SecurityAdapter.GetAccountId();
        return Ok(accountId);
      }); }
    
    [HttpGet]
    [Route("getuserinfo")]
    public IHttpActionResult GetUserInfo() {
      return GetHttpResponse(Request, () => {
        var user = _SecurityAdapter.GetUserInfo();
        return Ok(user); }); }

  }
}
