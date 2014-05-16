namespace RestSys.Migrations
{
    using RestSys.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RSDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(RSDbContext context)
        {
            try
            {
                if (!context.Users.Any())
                {
                    RSUser user0 = new RSUser()
                    {
                        Id = 0,
                        Name = "Admin",
                        Username = "admin",
                        IsAdmin = true,
                        IsWaiter = true
                    };
                    user0.CreatePassword("admin");
                    context.Users.AddOrUpdate(user0);
                }


                if (!context.Styles.Any(s => s.Type == (int)RSStyleType.MenuStyle))
                {
                    context.Styles.Add(new Models.RSStyle()
                    {
                        Path = "/Content/Menu/default.css",
                        Selected = true,
                        Title = "Default Menu Style",
                        Type = (int)RSStyleType.MenuStyle,
                        Notes = "This theme was provided by with the product to showcase menu printing capabilities of the product.",
                    });

                    context.Styles.Add(new Models.RSStyle()
                    {
                        Path = "/Content/Receipt/default.css",
                        Selected = true,
                        Title = "Default Receipt Style",
                        Type = (int)RSStyleType.MenuStyle,
                        Notes = "This theme was provided by with the product to showcase receipt printing capabilities of the product.",
                    });
                }
            }
            catch (Exception ex)
            { 
            }
        }
    }
}
