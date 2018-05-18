using AlbumApp.Business.Bootstrapper;
using AlbumApp.Business.Entities;
using AlbumApp.Business.Managers;
using AlbumApp.Core.Common.Core;
using Autofac;
using Autofac.Integration.Wcf;
using System;
using System.Security.Principal;
using System.Threading;
using System.Timers;
using System.Transactions;
using SM = System.ServiceModel;

namespace AlbumApp.ServiceHost
{
  class Program
  {
    static CartManager _cartManager;
    static void Main(string[] args)
    {
      GenericPrincipal principal = new GenericPrincipal(
        new GenericIdentity("Admin"), new string[] { "AlbumAppAdmin" });
      Thread.CurrentPrincipal = principal;

      Console.WriteLine("Starting up services ...");
      Console.WriteLine("");

      ObjectBase.Container = AutoFacLoader.Init();
      AutofacHostFactory.Container = AutoFacLoader.Init();
      using (var container = AutofacHostFactory.Container) // ObjectBase.Container
      {
        SM.ServiceHost hostAccountManager = new SM.ServiceHost(typeof(AccountManager));
        SM.ServiceHost hostInventoryManager = new SM.ServiceHost(typeof(InventoryManager));
        SM.ServiceHost hostCartManager = new SM.ServiceHost(typeof(CartManager));
        SM.ServiceHost hostOrderManager = new SM.ServiceHost(typeof(OrderManager));
        StartService(hostAccountManager, "AccountManager");
        StartService(hostInventoryManager, "InventoryManager");
        StartService(hostCartManager, "CartManager");
        StartService(hostOrderManager, "OrderManager");

        _cartManager = container.Resolve<CartManager>();

        System.Timers.Timer timer = new System.Timers.Timer(60000);
        timer.Elapsed += OnTimerElapsed;
        timer.Start();

        Console.WriteLine("Cart Item monitor started.");
        Console.WriteLine("");
        Console.WriteLine("Press [Enter] to exit.");
        Console.ReadLine();

        timer.Stop();
        Console.WriteLine("Cart Item monitor stopped.");

        StopService(hostAccountManager, "AccountManager");
        StopService(hostInventoryManager, "InventoryManager");
        StopService(hostCartManager, "CartManager");
        StopService(hostOrderManager, "OrderManager");

      }
    }

    static void OnTimerElapsed(object sender, ElapsedEventArgs e) {
      Console.WriteLine($"Looking for dead cart items at {DateTime.Now.ToString()}.");

    CartItem[] cartItems =  _cartManager.GetDeadCartItems();
    if (cartItems != null)
      foreach (var item in cartItems)
        using (TransactionScope scope = new TransactionScope()) {
          try { _cartManager.DeleteCartItem(item.CartItemId);
            Console.WriteLine($"Deleting Cart Item ID: '{item.CartItemId}'");
                scope.Complete(); }
          catch (Exception ex) {
            Console.WriteLine(
              $"There was an exception when attempting to delete cart item ID: '{item.CartItemId}'.");
          } } }
    
    static void StartService(SM.ServiceHost host, string serviceDescription) {
      host.Open();
      Console.WriteLine($"Service {serviceDescription} started.");

      foreach (var endpoint in host.Description.Endpoints) {
        Console.WriteLine(string.Format("Listening on endpoint"));
        Console.WriteLine(string.Format($"Address: {endpoint.Address.Uri}"));
        Console.WriteLine(string.Format($"Binding: {endpoint.Binding.Name}"));
        Console.WriteLine(string.Format($"Contract: {endpoint.Contract.ConfigurationName}")); }
      Console.WriteLine(); }

    static void StopService(SM.ServiceHost host, string serviceDescription) {
      host.Close();        Console.WriteLine($"Service {serviceDescription} stopped."); }
  }
}
