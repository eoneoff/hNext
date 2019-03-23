using CsvHelper;
using hNext.DbAccessMSSQLCore;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace hNext.DataBaseDataFiller
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<hNextDbContext>(options =>
                options.UseSqlServer("Server=DESKTOP-FD6IG6K;Database=hNextDb;Integrated Security=True"))
                .BuildServiceProvider();

            var creator = new ItemCreator();

            //using (TextReader reader = new StreamReader("countries.txt"))
            //{
            //    creator.Countries = reader.ReadToEnd().Split(Environment.NewLine)
            //        .Select(c => new Country
            //        {
            //            Name = c
            //        }).ToList();
            //}

            //Console.WriteLine("Coutries Created");

            using (hNextDbContext db = serviceProvider.GetRequiredService<hNextDbContext>())
            {
                //db.Countries.AddRange(creator.Countries);
                //db.SaveChanges();

                //Console.WriteLine("Coutries saved to DB");

                creator.CountryId = db.Countries.SingleOrDefault(c => c.Name == "Україна").Id;
                List<CsvModel> records = null;
                using (TextReader reader = new StreamReader("houses.csv", Encoding.UTF8))
                {
                    var helper = new CsvReader(reader);
                    helper.Configuration.Delimiter = ";";
                    helper.Configuration.BadDataFound = null;
                    records = helper.GetRecords<CsvModel>().ToList();
                }

                //creator.Regions = records.Select(r => r.Region).Distinct().Select(n => new Region
                //{
                //    CountryId = creator.CountryId,
                //    Name = n
                //}).ToList();

                //Console.WriteLine("Regions created");

                //db.Regions.AddRange(creator.Regions);
                //db.SaveChanges();

                //Console.WriteLine("Regions saved to DB");

                if (creator.Regions == null)
                    creator.Regions = db.Regions.ToList();

                //creator.Districts = records.GroupBy(r => r.Region).SelectMany(g => g.DistinctBy(r => r.District).Where(r => !string.IsNullOrWhiteSpace(r.District)).Select(r => new District
                //{
                //    Name = r.District,
                //    RegionId = creator.Regions.First(reg => reg.Name == r.Region).Id
                //})).ToList();

                //Console.WriteLine("Districts created");

                //db.Districts.AddRange(creator.Districts);
                //db.SaveChanges();

                //Console.WriteLine("Districts saved to DB");

                if (creator.Districts == null)
                    creator.Districts = db.Districts.ToList();

                db.CityTypes.AddRange(new List<CityType>
                {
                    new CityType{ Id = 1, Name = "місто" },
                    new CityType{ Id = 2, Name = "село"},
                    new CityType { Id = 3, Name = "с.м.т."},
                    new CityType { Id = 4, Name = "селище"}
                });
                db.SaveChanges();

                creator.CreateCities(records);

                db.Cities.AddRange(creator.Cities);
                db.SaveChanges();

                Console.WriteLine("Cities saved to DB");

                if (creator.Cities == null)
                    creator.Cities = db.Cities.Include(c => c.Region).Include(c => c.District).ToList();

                db.StreetTypes.AddRange(new List<StreetType>
                {
                    new StreetType{Id = 1, Name = "вулиця"},
                    new StreetType{Id = 2, Name = "провулок"},
                    new StreetType{Id = 3, Name = "проспект"},
                    new StreetType{Id = 4, Name = "бульвар"},
                    new StreetType{Id = 5, Name = "квартал"},
                    new StreetType{Id = 6, Name = "площа"},
                    new StreetType{Id = 7, Name = "проїзд"},
                    new StreetType{Id = 8, Name = "хутір"},
                    new StreetType{Id = 9, Name = "тупік"},
                    new StreetType{Id = 10, Name = "узвіз"},
                    new StreetType{Id = 11, Name = "парк"},
                    new StreetType{Id = 12, Name = "селище"},
                    new StreetType{Id = 13, Name = "шосе"},
                    new StreetType{Id = 14, Name = "жилий масив"},
                    new StreetType{Id = 15, Name = "мікрорайон"},
                    new StreetType{Id = 16, Name = "майдан"},
                    new StreetType{Id = 17, Name = "спуск"},
                    new StreetType{Id = 18, Name = "в'їзд"},
                    new StreetType{Id = 19, Name = "острів"},
                    new StreetType{Id = 20, Name = "містечко"},
                    new StreetType{Id = 21, Name = "лінія"},
                    new StreetType{Id = 22, Name = "шлях"},
                    new StreetType{Id = 23, Name = "набережна"},
                    new StreetType{Id = 24, Name = "алея"},
                    new StreetType{Id = 25, Name = "урочище"},
                    new StreetType{Id = 26, Name = "дорога"},
                    new StreetType{Id = 27, Name = "завулок"}
                });

                db.SaveChanges();

                creator.CreateStreets(records);

                db.Streets.AddRange(creator.Streets);
                db.SaveChanges();

                Console.WriteLine("Streets saved to DB");

                Console.WriteLine("Done");
                Console.ReadLine();
            }
        }
    }
}
