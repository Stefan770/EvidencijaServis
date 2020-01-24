namespace EvidencijaServis.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EvidencijaServis.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EvidencijaServis.Models.ApplicationDbContext context)
        {
            context.Kompanije.AddOrUpdate(x => x.Id,
                new Models.Kompanija { Id = 1, Naziv = "Google", Godina = 1998 },
                new Models.Kompanija { Id = 2, Naziv = "Apple", Godina = 1976 },
                new Models.Kompanija { Id = 3, Naziv = "Microsoft", Godina = 1975 }
                );

            context.Zaposleni.AddOrUpdate(x => x.Id,
                new Models.Zaposleni { Id = 1, ImeIPrezime = "Pera Peric", GodinaRodjenja = 1980, GodinaZaposlenja = 2008, Plata = 3000, KompanijaId = 1 },
                new Models.Zaposleni { Id = 2, ImeIPrezime = "Mika Mikic", GodinaRodjenja = 1976, GodinaZaposlenja = 2005, Plata = 6000, KompanijaId = 1 },
                new Models.Zaposleni { Id = 3, ImeIPrezime = "Iva Ivic", GodinaRodjenja = 1990, GodinaZaposlenja = 2016, Plata = 4000, KompanijaId = 2 },
                new Models.Zaposleni { Id = 4, ImeIPrezime = "Zika Zikic", GodinaRodjenja = 1985, GodinaZaposlenja = 2005, Plata = 5000, KompanijaId = 2 },
                new Models.Zaposleni { Id = 5, ImeIPrezime = "Sara Saric", GodinaRodjenja = 1982, GodinaZaposlenja = 2007, Plata = 5500, KompanijaId = 3 }
                );
        }
    }
}
