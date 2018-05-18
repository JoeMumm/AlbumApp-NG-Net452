using AlbumApp.Client.Proxies;
using Xunit;

namespace Core.Common.ClientProxies.Tests
{
  public class ServiceAccessTests
  {
    [Fact]
    public void test_account_client_connection() {
      AccountClient proxy = new AccountClient();
      proxy.Open(); }

    [Fact]
    public void test_cart_client_connection() {
      CartClient proxy = new CartClient();
      proxy.Open(); }  
    [Fact]

    public void test_inventory_client_connection() {
      InventoryClient proxy = new InventoryClient();
      proxy.Open(); } 
    
    [Fact]
    public void test_order_client_connection() {
      OrderClient proxy = new OrderClient();
      proxy.Open(); } 
  }
}
