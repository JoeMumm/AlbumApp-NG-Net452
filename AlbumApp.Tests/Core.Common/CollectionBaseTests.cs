using System;
using System.Collections.Generic;
using AlbumApp.Core.Common.Contracts;
using Xunit;
using Tests.Core.Common;

namespace Core.Common.Tests
{
  public class CollectionBaseTests
  {
    [Fact]
    public void test_collection_item_collection_and_property_change_notification()
    {
      TestList objList = new TestList();
      TestClass objTest = new TestClass();
      bool collectionChanged = false;
      bool propertyChanged = false;

      objList.CollectionChanged += (s, e) => collectionChanged = true;

      objList.Add(objTest);

      Assert.True(collectionChanged, "Collection change should have fired.");

      objList.ItemPropertyChanged += (s, e) =>
      {
        if (e.PropertyName == "DirtyProp")
          propertyChanged = true;
      };

      objTest.DirtyProp = "test value";

      Assert.True(propertyChanged, "Item property change should have fired.");
    }

    [Fact]
    public void test_collection_dirtiness()
    {
      TestList objList = new TestList();
      TestClass objTest = new TestClass();

      objList.Add(objTest);

      objTest.DirtyProp = "test value";

      Assert.True(objList.IsDirty, "Collection should be dirty.");
    }

    [Fact]
    public void test_collection_property_dirtyness()
    {
      TestClass objTest = new TestClass();
      TestChild objChild = new TestChild();

      objTest.Children.Add(objChild);

      Assert.False(objTest.IsAnythingDirty(), "Nothing in the object graph should be dirty.");

      objChild.ChildName = "test value";

      Assert.True(objTest.IsAnythingDirty(), "The test object should be reflecting dirtiness within.");
    }

    [Fact]
    public void test_dirty_collection_aggregating()
    {
      TestClass objTest = new TestClass();
      TestChild objChild = new TestChild();

      List<IDirtyCapable> dirtyObjects = objTest.GetDirtyObjects();

      objTest.Children.Add(objChild);

      Assert.True(dirtyObjects.Count == 0, "There should be no dirty object returned.");

      objChild.ChildName = "test value";
      dirtyObjects = objTest.GetDirtyObjects();

      Assert.True(dirtyObjects.Count == 1, "There should be one dirty object.");

      objTest.DirtyProp = "test value";
      dirtyObjects = objTest.GetDirtyObjects();

      Assert.True(dirtyObjects.Count == 2, "There should be two dirty object.");

      objTest.CleanAll();
      dirtyObjects = objTest.GetDirtyObjects();

      Assert.True(dirtyObjects.Count == 0, "There should be no dirty object returned.");
    }
  }
}
