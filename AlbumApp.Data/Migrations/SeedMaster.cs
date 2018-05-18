namespace AlbumApp.Data.xx
{
  using Business.Entities;
  using Microsoft.AspNet.Identity;
  using Microsoft.AspNet.Identity.EntityFramework;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;

  class Configurationxx 
  {

    Random _rand;

    protected void Seed(AlbumApp.Data.AlbumContext context) // override
    {
        //  This method will be called after migrating to the latest version.

        //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //  to avoid creating duplicate seed data.

    List<ApplicationUser> users = new List<ApplicationUser> {
      new ApplicationUser { Email = "jsbach@gmail.com", UserName =  "jsbach@gmail.com",
        AccountSet = new Account { FirstName = "Johann-Sebastin", LastName = "Bach",
        Address = "12 Baroque Rd.", City = "Leipzig",  State = "ND", ZipCode = "58562", CreditCard = "1234143078241234",
          ExpDate = "1124", AccountId = 1  }  }, // },
      new ApplicationUser { Email = "jrmorton@gmail.com", UserName =  "jrmorton@gmail.com",
          AccountSet = new Account { FirstName = "Jelly-Roll", LastName = "Morton",
          Address = "23 Bourbon St.", City = "New Orleans",  State = "LA", ZipCode = "70130", CreditCard = "2249123412341234",
          ExpDate = "1123", AccountId = 2 } },
      new ApplicationUser { Email = "cschuman@gmail.com", UserName =  "cschuman@gmail.com",
          AccountSet = new Account { FirstName = "Clara", LastName = "Schuman",
          Address =  "7456 Romance Ave.", City = "Leipzig",  State = "ND", ZipCode = "58562", CreditCard = "1234123436826834",
          ExpDate = "1123", AccountId = 3 } },
    new ApplicationUser { Email = "bbritten@gmail.com", UserName =  "bbritten@gmail.com",
          AccountSet = new Account { FirstName = "Benjamin", LastName = "Britten",
          Address = "45 Suffolk Blvd.", City = "New London",  State = "CT", ZipCode = "06320", CreditCard = "1234144712341234",
          ExpDate = "1123", AccountId = 4 } },
    new ApplicationUser { Email = "eelias@gmail.com", UserName =  "eelias@gmail.com",
          AccountSet = new Account { FirstName = "Elaine", LastName = "Elias",
          Address = "3827 Latin Ln.", City = "New York",  State = "NY", ZipCode = "10034", CreditCard = "1232953412341234",
          ExpDate = "1123", AccountId = 5 }
} };

      ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
    foreach (var u in users) { manager.Create(u, "P@$$w0rd"); }

    context.AlbumSet.AddOrUpdate(a => a.Title,
      new Album { AlbumNumber = "BRJZ-MrDo-001", Title = "To Brazil and Bacharach", Artist = "Mary D' Orazi",
                  Genre = 2, Price = 15m, StockAmount = 1000 },
      new Album { AlbumNumber = "ACJZ-MnSt-010",Title = "Afro Rhythm", Artist = "Mongo Santamaria",
                  Genre = 1, Price = 15m, StockAmount = 1000 },
      new Album { AlbumNumber = "ClCN-NTWB-050",Title = "Dream Catchers", Artist = "North Texas Wind Symphony",
                  Genre = 3, Price = 15m, StockAmount = 1000 },
      new Album { AlbumNumber = "RCRL-Btls-012",Title = "Sgt. Pepper's Lonely Hearts Club Band", Artist = "Beatles",
                  Genre = 5, Price = 15m, StockAmount = 1000 });
      context.SaveChanges();

      _rand = new Random();
    for (int i = 5; i < 20; i++) {
      context.AlbumSet.AddOrUpdate(a => a.Title,
      new Album { AlbumNumber = "XYZZ-AbCd-" + i, Title = ((char)GetRandom(65, 91)).ToString() + ((char) GetRandom(97, 123)).ToString() + "Album # " + i,
          Artist = "Artist" + i, Genre = GetRandom(1, 5), Price = GetRandom(5, 20), StockAmount = GetRandom(1, 1000) }); }

    context.TrackSet.AddOrUpdate(t => t.Name,
      new Track { AlbumId = 1, Number = 1, Name = "Chico Hipocondria", Minutes = 3, Seconds = 37 },
      new Track { AlbumId = 1, Number = 2, Name = "I Say a Little Prayer", Minutes = 3, Seconds = 23 },
      new Track { AlbumId = 1, Number = 3, Name = "Fotografia", Minutes = 5, Seconds = 22 },
      new Track { AlbumId = 1, Number = 4, Name = "Coração Vira Lata", Minutes = 4, Seconds = 18 },
      new Track { AlbumId = 1, Number = 5, Name = "A House Is Not a Home", Minutes = 5, Seconds = 12 },
      new Track { AlbumId = 1, Number = 6, Name = "Da Licença", Minutes = 3, Seconds = 24 },
      new Track { AlbumId = 1, Number = 7, Name = "Walk On By", Minutes = 3, Seconds = 49 },
      new Track { AlbumId = 1, Number = 8, Name = "Alfie", Minutes = 5, Seconds = 38 },
      new Track { AlbumId = 1, Number = 9, Name = "Meu Canário Vizinho Azul", Minutes = 4, Seconds = 12 },
      new Track { AlbumId = 1, Number = 10, Name = "Vieste", Minutes = 4, Seconds = 48 },
      new Track { AlbumId = 1, Number = 11, Name = "Barra-Joá", Minutes = 4, Seconds = 55 },
      new Track { AlbumId = 1, Number = 12, Name = "(They Long to Be) Close to You", Minutes = 4, Seconds = 46 },

      new Track { AlbumId = 2, Number = 1, Name = "Jamaicuba", Minutes = 3, Seconds = 37 },
      new Track { AlbumId = 2, Number = 2, Name = "Manila", Minutes = 3, Seconds = 23 },
      new Track { AlbumId = 2, Number = 3, Name = "He Guapacha", Minutes = 5, Seconds = 22 },
      new Track { AlbumId = 2, Number = 4, Name = "Cha Cha Rock", Minutes = 4, Seconds = 18 },
      new Track { AlbumId = 2, Number = 5, Name = "Vengan Pollos", Minutes = 5, Seconds = 12 },
      new Track { AlbumId = 2, Number = 6, Name = "Barandanga", Minutes = 3, Seconds = 24 },
      new Track { AlbumId = 2, Number = 7, Name = "Linda Guajira", Minutes = 3, Seconds = 49 },
      new Track { AlbumId = 2, Number = 8, Name = "Vamos A Gozar", Minutes = 5, Seconds = 38 },
      new Track { AlbumId = 2, Number = 9, Name = "Miss Patti Cha Cha", Minutes = 4, Seconds = 12 },
      new Track { AlbumId = 2, Number = 10, Name = "Viva La Felicidad", Minutes = 4, Seconds = 48 },
      new Track { AlbumId = 2, Number = 11, Name = "Tele Mina For Chango", Minutes = 4, Seconds = 55 },
      new Track { AlbumId = 2, Number = 12, Name = "Olla De For Olla", Minutes = 4, Seconds = 46 },

      new Track { AlbumId = 3, Number = 1, Name = "Danza de los Duendes", Minutes = 9, Seconds = 28 },
      new Track { AlbumId = 3, Number = 2, Name = "Dreamcatcher", Minutes = 12, Seconds = 52 },
      new Track { AlbumId = 3, Number = 3, Name = "Lullaby for Kirsten", Minutes = 2, Seconds = 59 },
      new Track { AlbumId = 3, Number = 4, Name = "From a Dark Millennium", Minutes = 12, Seconds = 24 },
      new Track { AlbumId = 3, Number = 5, Name = "Softly and Tenderly, Jesus is Calling", Minutes = 0, Seconds = 57 },
      new Track { AlbumId = 3, Number = 6, Name = "Waking Angels", Minutes = 11, Seconds = 09 },
      new Track { AlbumId = 3, Number = 7, Name = "The Dream of Oenghus, Op. 37: Part I", Minutes = 6, Seconds = 47 },
      new Track { AlbumId = 3, Number = 8, Name = "The Dream of Oenghus, Op. 37: Part II", Minutes = 14, Seconds = 17 },

      new Track { AlbumId = 4, Number = 1, Name = "Sgt. Pepper's Lonely Hearts Club Band", Minutes = 9, Seconds = 28 },
      new Track { AlbumId = 4, Number = 2, Name = "With A Little Help From My Friends", Minutes = 12, Seconds = 52 },
      new Track { AlbumId = 4, Number = 3, Name = "Lucy In The Sky With Diamonds", Minutes = 2, Seconds = 59 },
      new Track { AlbumId = 4, Number = 4, Name = "Getting Better", Minutes = 12, Seconds = 24 },
      new Track { AlbumId = 4, Number = 5, Name = "Fixing A Hole", Minutes = 0, Seconds = 57 },
      new Track { AlbumId = 4, Number = 6, Name = "She's Leaving Home", Minutes = 11, Seconds = 09 },
      new Track { AlbumId = 4, Number = 7, Name = "Being For The Benefit Of Mr Kite", Minutes = 6, Seconds = 47 },
      new Track { AlbumId = 4, Number = 8, Name = "Within You Without You", Minutes = 14, Seconds = 17 },
      new Track { AlbumId = 4, Number = 9, Name = "When I'm Sixty Four", Minutes = 14, Seconds = 17 },
      new Track { AlbumId = 4, Number = 10, Name = "Lovely Rita", Minutes = 4, Seconds = 48 },
      new Track { AlbumId = 4, Number = 11, Name = "Good Morning Good Morning", Minutes = 4, Seconds = 55 },
      new Track { AlbumId = 4, Number = 12, Name = "Sgt. Pepper's Lonely Hearts Club Band (Reprise)", Minutes = 4, Seconds = 46 },
      new Track { AlbumId = 4, Number = 13, Name = "A Day In The Life", Minutes = 4, Seconds = 46 },
      new Track { AlbumId = 4, Number = 14, Name = "Sgt. Pepper's Lonely Hearts Club Band Documentary", Minutes = 4, Seconds = 46 });

      for (int i = 5; i < 19; i++) {
        for (int j = 1; j < GetRandom(6, 12); j++) {
          context.TrackSet.AddOrUpdate(t => t.Name,
          new Track { AlbumId = i, Number = j, Name = $"Album {i} Track #: {j}",
            Minutes = GetRandom(2, 8), Seconds = GetRandom(0, 60) }); } }
    context.SaveChanges();

    context.OrderSet.AddOrUpdate(o => o.AccountId,
      new Order { AccountId = 0, Created = DateTime.Now.AddDays(-60) }, 
      new Order { AccountId = 1, Amount = 55m, Discount = .07m, Tax = 3.07m, Shipping = 3.95m,
          Created = DateTime.Now.AddDays(-8), Shipped = DateTime.Now.AddDays(-6) },
      new Order { AccountId = 1, Amount = 21.69m, Discount = .05m,  Tax = 1.24m, Shipping = 3.95m,  
          Created = DateTime.Now.AddDays(-6) },
      new Order { AccountId = 1, Amount = 45m, Discount = .02m,  Tax = 1.76m, Shipping = 3.95m,  
          Created = DateTime.Now.AddDays(-4), Shipped = DateTime.Now.AddDays(-2) }, // 
      new Order { AccountId = 2,  Amount = 30m, Discount = .00m, Tax = 0m, Shipping = 3.95m,  
          Created = DateTime.Now.AddDays(-18) },
      new Order { AccountId = 2, Amount = 27.19m, Discount = .0m, Tax = .25m, Shipping = 3.95m,  
          Created = DateTime.Now.AddDays(-4) }); 
      context.SaveChanges();

      context.CartItemSet.AddOrUpdate(c => c.OrderId,
        new CartItem { OrderId = 1, AccountId = 1, AlbumId = 1, Quantity = 1, Price = 15m, Created = DateTime.Now },
        new CartItem { OrderId = 1, AccountId = 1, AlbumId = 2, Quantity = 1, Price = 15m, Created = DateTime.Now },
        new CartItem { OrderId = 1, AccountId = 1, AlbumId = 3, Quantity = 2, Price = 15m, Created = DateTime.Now },
        new CartItem { OrderId = 1, AccountId = 1, AlbumId = 4, Quantity = 1, Price = 15m, Created = DateTime.Now },
        new CartItem { OrderId = 1, AccountId = 1, AlbumId = 5, Quantity = 10, Price = 15m, Created = DateTime.Now },
        new CartItem { OrderId = 1, AccountId = 1, AlbumId = 6, Quantity = 1, Price = 15m, Created = DateTime.Now },
        new CartItem { OrderId = 1, AccountId = 1, AlbumId = 7, Quantity = 2, Price = 15m, Created = DateTime.Now },
        new CartItem { OrderId = 1, AccountId = 1, AlbumId = 11, Quantity = 12, Price = 15m, Created = DateTime.Now },
        new CartItem { OrderId = 1, AccountId = 1, AlbumId = 14, Quantity = 1, Price = 15m, Created = DateTime.Now },
        new CartItem { OrderId = 1, AccountId = 1, AlbumId = 17, Quantity = 17, Price = 15m, Created = DateTime.Now },
        new CartItem { OrderId = 1, AccountId = 1, AlbumId = 18, Quantity = 21, Price = 15m, Created = DateTime.Now },
        new CartItem { OrderId = 1, AccountId = 1, AlbumId = 19, Quantity = 1, Price = 15m, Created = DateTime.Now },
        new CartItem { OrderId = 2, AccountId = 1, AlbumId = 8, Quantity = 2, Price = 12.5m, Created = DateTime.Now.AddDays(-8) },
        new CartItem { OrderId = 2, AccountId = 1, AlbumId = 4, Quantity = 1, Price = 15m, Created = DateTime.Now.AddDays(-8) },
        new CartItem { OrderId = 2, AccountId = 1, AlbumId = 2, Quantity = 1, Price = 15m, Created = DateTime.Now.AddDays(-8) },
        new CartItem { OrderId = 3, AccountId = 1, AlbumId = 19, Quantity = 1, Price = 11.25m, Created = DateTime.Now.AddDays(-6) },
        new CartItem { OrderId = 3, AccountId = 1, AlbumId = 13, Quantity = 1, Price = 10.44m, Created = DateTime.Now.AddDays(-6) },

        new CartItem { OrderId = 1, AccountId = 2, AlbumId = 2, Quantity = 1, Price = 15m, Created = DateTime.Now.AddDays(-8) },
        new CartItem { OrderId = 4, AccountId = 2, AlbumId = 2, Quantity = 1, Price = 15m, Created = DateTime.Now.AddDays(-6) },
        new CartItem { OrderId = 4, AccountId = 2, AlbumId = 3, Quantity = 1, Price = 15m, Created = DateTime.Now.AddDays(-6) },
        new CartItem { OrderId = 4, AccountId = 2, AlbumId = 4, Quantity = 1, Price = 15m, Created = DateTime.Now.AddDays(-6) },

        new CartItem { OrderId = 5, AccountId = 3, AlbumId = 2, Quantity = 1, Price = 15m, Created = DateTime.Now.AddDays(-5) },
        new CartItem { OrderId = 5, AccountId = 3, AlbumId = 3, Quantity = 1, Price = 15m, Created = DateTime.Now.AddDays(-5) },

        new CartItem { OrderId = 6, AccountId = 4, AlbumId = 15, Quantity = 1, Price = 12.65m, Created = DateTime.Now.AddDays(-4) }
        );
      context.SaveChanges();

    }

    int GetRandom(int low, int high) {
      var number = _rand.Next(low, high); return number; } 

  }
}