
appCartModule.controller("CartViewModel", function ($scope, $http, $timeout,
              viewModelHelper, checkout) {
  $scope.viewModelHelper = viewModelHelper;
  viewModelHelper.isLoading = true;
  $scope.cartItems = [];
  var accountId = 0;
  $scope.subTotal = 0;
  $scope.itemCount = 0;
  $scope.isCheckout = (checkout == "True") ? true : false;
  
  var getAccountId = function () {
    viewModelHelper.apiGet('api/account/getaccountid', null,
      function (result) { viewModelHelper.isLoading = true;
        accountId = result.data;
        getCartItems(); }); };

  var getCartItems = function () { /* api/cart/getpendingcartitemsbyaccountid */
    viewModelHelper.apiGet('api/cart/getpendingcartitemsbyaccountid/' + accountId, null,
    function (result) {
      result.data.forEach((item) => {
        var cartItem = {
          CartItemId: item.CartItem.CartItemId, Image: item.Image, Title: item.Title,
          Quantity: item.CartItem.Quantity, Price: item.CartItem.Price, Display: false };
        $scope.cartItems.push(cartItem);
        $scope.subTotal = $scope.subTotal + (cartItem.Price * cartItem.Quantity);
        $scope.itemCount = $scope.itemCount + cartItem.Quantity; });
      if ($scope.isCheckout) $scope.checkOut();  }); }

  $scope.updateItem = function (cartItemId, quantity) {
    console.log("Update Item " + cartItemId);
    viewModelHelper.apiPost('api/cart/updatecartitem/' + cartItemId +
      '/' + quantity, null, null, function (result) {
        window.location.href = AlbumApp.rootPath + 'cart/index?checkout=' + $scope.isCheckout; }); }

  $scope.deleteItem = function (cartItemID) {
    viewModelHelper.apiPost('api/cart/deletecartitem/' + cartItemID, null, null,
      function (result) {
        window.location.href = AlbumApp.rootPath + 'cart/index?checkout=' + $scope.isCheckout; }); }

  var getUserInfo = function () {
    viewModelHelper.apiGet('api/account/getuserinfo', null,
      function (result) {
        $scope.user = result.data;
        $scope.user.CreditCard = last4Digits($scope.user.CreditCard) }); }

  var last4Digits = function (cc) {
    cc = cc.substring(cc.length - 4, cc.length);
    return cc; }

  $scope.user;
  $scope.shipping = 0;
  $scope.tax = 0;
  $scope.total = 0;
  $scope.checkOut = function () {
    getUserInfo();
    var shipCalc = ($scope.itemCount / 4) < 1 ? 1 : ($scope.itemCount / 4);
    $scope.shipping = parseFloat((shipCalc * 3.99).toFixed(2));
    $scope.tax = parseFloat(($scope.subTotal * .04).toFixed(2));
    $scope.total = parseFloat(($scope.subTotal + $scope.tax + $scope.shipping).toFixed(2));
    $scope.isCheckout = true; }

  var createOrder = function (orderJSON) { /* api/order/create */
    viewModelHelper.apiPost('api/order/create', null, { params: { order: orderJSON } },
    function (result) { var orderId = result.data;
    transferCartItems(orderId); }); }

  var transferCartItems = function (orderId) {  /* api/cart/transfercartitemtoorder */
    viewModelHelper.apiPost('api/cart/transfercartitemtoorder/' +
      orderId + '/' + accountId, null, null,
      function (result) {
        window.location.href = AlbumApp.rootPath + 'order/confirmation?orderId=' + orderId; }); }
  
  $scope.placeOrder = function () {  
    var order = { AccountId : accountId, Amount : $scope.subTotal,
      Tax: $scope.tax, Shipping: $scope.shipping }
    var orderJSON = JSON.stringify(order);
    createOrder(orderJSON); }
  
  getAccountId();

});