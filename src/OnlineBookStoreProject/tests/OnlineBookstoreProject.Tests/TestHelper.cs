using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain.Entities;

namespace OnlineBookstoreProject.Tests
{
    public static  class TestHelper
    {

        public static readonly IFixture Fixture = GetFixture();
        public static readonly IMapper Mapper = GetDefaultMapper();
        private static IMapper GetDefaultMapper()
        {
            //var profileTypes = AppDomain.CurrentDomain.GetAssemblies()
            //    .SelectMany(a => a.GetTypes().Where(type => typeof(Profile).IsAssignableFrom(type)));

            //IEnumerable<Profile> profiles = profileTypes.ToList();

            //var config = new MapperConfiguration(cfg => {
            //    cfg.AddProfiles(profiles);
            //});



            // Scan all assemblies for classes that inherit from AutoMapper.Profile
            var profileTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IMappingProfile).IsAssignableFrom(type) && !type.IsAbstract);

            // Create a MapperConfiguration and add the found profiles
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profileType in profileTypes)
                {
                    cfg.AddProfile(Activator.CreateInstance(profileType) as Profile);
                }
            });

            // Create a mapper based on the configuration
            return config.CreateMapper();
        }
        private  static IFixture GetFixture()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            fixture.Customize<Book>(c =>
                c.With(b => b.Id, fixture.Create<int>() + 1)
                    .With(b => b.Title, fixture.Create<string>())
                    .With(b => b.Author, fixture.Create<string>())
                    .With(b => b.CategoryId, fixture.Create<int>())
                    .With(b => b.BookshelfId, fixture.Create<int?>())
                    .With(b => b.Description, fixture.Create<string>())
                    .With(b => b.Price, fixture.Create<decimal>())
                    .With(b => b.Discount, fixture.Create<decimal>())
                    .With(b => b.PublicationDate, DateTime.Today)
                    .With(b => b.CoverImagePath, fixture.Create<string>())
            );
            fixture.Customize<Bookshelf>(x => 
                x.With(b=>b.Id,fixture.Create<int>()+1)
                    .With(b=>b.Name,fixture.Create<string>())
                    .With(b=>b.UserId,fixture.Create<int>()+1)
            );
            fixture.Customize<OrderItem>(o =>
                o.With(o => o.Id, fixture.Create<int>() + 1)
                .With(o => o.BookPublicationDateAtThatTime, DateTime.Today)
            );






            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }



    }
}
