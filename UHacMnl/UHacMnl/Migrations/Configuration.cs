namespace UHacMnl.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UHacMnl.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<UHacMnl.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UHacMnl.Models.ApplicationDbContext context)
        {
            if (context.Subscribers.Find(1) == null)
            {
                context.Subscribers.Add(new Subscriber { SubscriberId = 1, FirstName = "SYSTEM" });
            }
            base.Seed(context);
        }
    }
}
