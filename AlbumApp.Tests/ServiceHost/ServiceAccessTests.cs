using AlbumApp.Business.Contracts;
using System.ServiceModel;
using Xunit;

namespace AlbumAppCore.Common.ServiceHost.Tests
{
  public class ServiceAccessTests
  {
    [Fact]
    public void test_account_manager_as_service() {
      ChannelFactory<IAccountService> channelFactory =
        new ChannelFactory<IAccountService>("");

      IAccountService proxy = channelFactory.CreateChannel();

      (proxy as ICommunicationObject).Open();
      channelFactory.Close(); }

    [Fact]
    public void test_cart_manager_as_service()
    {
      ChannelFactory<ICartService> channelFactory =
        new ChannelFactory<ICartService>("");

      ICartService proxy = channelFactory.CreateChannel();

      (proxy as ICommunicationObject).Open();
      channelFactory.Close(); }
    
    [Fact]
    public void test_inventory_manager_as_service()
    {
      ChannelFactory<IInventoryService> channelFactory =
        new ChannelFactory<IInventoryService>("");

      IInventoryService proxy = channelFactory.CreateChannel();

      (proxy as ICommunicationObject).Open();
      channelFactory.Close(); }

    [Fact]
    public void test_order_manager_as_service()
    {
      ChannelFactory<IOrderService> channelFactory =
        new ChannelFactory<IOrderService>("");

      IOrderService proxy = channelFactory.CreateChannel();

      (proxy as ICommunicationObject).Open();
      channelFactory.Close(); }

  }
}
