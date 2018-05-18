using AlbumApp.Client.Bootstrapper;
using AlbumApp.Client.Contracts;
using AlbumApp.Client.Proxies;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.Core;
using Autofac;
using Xunit;

namespace Core.Common.ClientProxies.Tests
{
  public class ProxyObtainmentTests
  {
    public ProxyObtainmentTests()
    { ObjectBase.Container = AutoFacLoader.Init(); } //builder.Build();
    
    [Fact]
    public void obtain_proxy_from_container_using_service_contract() {
      using (var container = ObjectBase.Container) {
        IInventoryService proxy = container.Resolve<IInventoryService>();
        Assert.True(proxy is InventoryClient); } }

    [Fact]
    public void obtain_proxy_from_service_factory() {
      IServiceFactory factory = new ServiceFactory();
      IInventoryService proxy = factory.CreateClient<IInventoryService>();

      Assert.True(proxy is InventoryClient); }

    [Fact]
    public void obtain_service_factory_and_proxy_from_container() {
      using (var container = ObjectBase.Container) {
        IServiceFactory factory = container.Resolve<IServiceFactory>();
        IInventoryService proxy = factory.CreateClient<IInventoryService>();

        Assert.True(proxy is InventoryClient); } }

  }
}
