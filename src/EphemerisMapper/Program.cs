using AstroTools.Common.DataProvider;
using AstroTools.Common.Extensions;
using AstroTools.Common.Factory;
using AstroTools.Common.Model;
using AstroTools.Common.Model.Degree;
using AstroTools.Common.Repository;
using AstroTools.Ephemerides.Factory;
using AstroTools.Ephemerides.Model.DataTransfer;
using AstroTools.Events.Factory;
using AstroTools.Events.Model;
using AstroTools.Scripts.Generator;
using AstroTools.Zodiac.Builder;
using AstroTools.Zodiac.Builder.Cusp;
using AstroTools.Zodiac.Factory;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;
using AstroTools.Zodiac.Model.Tarot;
using AstroTools.Zodiac.Zodiac;

namespace EphemerisMapper;

internal class Program
{
    public static void Main(string[] args)
    {
        var signCuspGenerator = new CuspGenerator<SignEnum>();

        var planetFactory = new PlanetFactory();
        var planetRepository = new GenericRepository<Planet>(planetFactory.CreateAll());

        var tarotFactory = new TarotCardFactory();
        var tarotRepository = new GenericRepository<TarotCard>(tarotFactory.CreateAll());

        var signSubDivisionBuilder = new SignSubDivisionBuilder(tarotRepository, planetRepository);

        Sign SignCreatorFunc(SignEnum signEnum, DegreeRange range, Planet planet) => new(signEnum, range, planet);
        
        var signFactory = new DivisionFactory<Sign, SignEnum>(
            new Degree(30), 
            SignCreatorFunc, 
            signCuspGenerator,
            planetRepository, 
            signSubDivisionBuilder);
        
        var signRepository = new GenericRepository<Sign>(signFactory.CreateAll());
        
        var ephemFile = @"Resources\sunout_2h_jan_mar_2023.csv";
        var tropicalZodiac = new TropicalZodiac(signRepository);
        var ephemerisBuilder = new EphemerisFactory(tropicalZodiac, planetFactory);
        var ephemerisInitializer = new CsvDataProvider<TropicalEphemerisDto>();
        
        var ephemerides = ephemerisInitializer.Provide(ephemFile).SelectMany(e => ephemerisBuilder.Build(e)).ToList();

        var eventFact = new AstroEventFactory();
        var events = eventFact.CreateAll(ephemerides).ToList();
        
        var astroEventRepository = new GenericRepository<AstroEvent>(events);

        var scriptGenerator = new AstroEventsScriptGenerator();
        
        var outputDir = @"C:\Users\Zsolt\source\repos\tv\";
        
        foreach (var planetEnum in Enum.GetValues<PlanetEnum>())
        {
            DateTime? startDate = planetEnum switch
            {
                PlanetEnum.Sun or PlanetEnum.Mercury or PlanetEnum.Venus => new DateTime(2023, 01, 01),
                PlanetEnum.Mars => new DateTime(2020, 06, 01),
                PlanetEnum.Jupiter or PlanetEnum.Saturn => new DateTime(2020, 3, 1),
                PlanetEnum.Rahu or PlanetEnum.Ketu => new DateTime(2020, 1, 1),
                PlanetEnum.Moon => new DateTime(2023,1,1),
                PlanetEnum.Neptune or PlanetEnum.Pluto or PlanetEnum.Uranus => new DateTime(2023, 01, 01),
                _ => null
            };

            DateTime? endDate = new DateTime(2024, 01, 01);

            var eventsForPlanet =
                astroEventRepository.Get(e =>
                    e.Planet.PlanetEnum == planetEnum && e.Date.IsBetween(startDate, endDate))
                    .Where(e => e.Name.Contains("DecanChange"))
                    .ToArray();

            if (!eventsForPlanet.Any())
            {
                continue;
            }

            var script = scriptGenerator.Generate(eventsForPlanet, "Resources/tv_template.txt");

            File.WriteAllText(outputDir + $"tropical {planetEnum}.txt", script);
            Console.WriteLine($"Generated script for planet {planetEnum} ");
        }


        Console.ReadKey();
    }


    private static void CreateSubLordScripts(IRepository<Sign> signRepository, IRepository<Nakshatra> starRepository,
        IFactory<Planet> planetFactory)
    {
        var vedicZodiac = new VedicZodiac(signRepository, starRepository);

        var ephemFile = "Resources\\ephem_sidereal_krishnamurti_2022-2023_4h_mean.csv";
        var ephemerisBuilder = new EphemerisFactory(vedicZodiac, planetFactory);
        var ephemerisInitializer = new CsvDataProvider<VedicEphemerisDto>();

        var ephemerides = ephemerisInitializer.Provide(ephemFile).SelectMany(e => ephemerisBuilder.Build(e)).ToList();

        var eventFact = new AstroEventFactory();
        var events = eventFact.CreateAll(ephemerides).ToList();

        var astroEventRepository = new GenericRepository<AstroEvent>(events);
        var scriptGenerator = new AstroEventsScriptGenerator();

        var outputDir = @"C:\Users\Zsolt\source\repos\tv\";

        foreach (var planetEnum in Enum.GetValues<PlanetEnum>())
        {
            DateTime? startDate = planetEnum switch
            {
                PlanetEnum.Sun or PlanetEnum.Mercury or PlanetEnum.Venus => new DateTime(2020, 08, 01),
                PlanetEnum.Mars => new DateTime(2020, 06, 01),
                PlanetEnum.Jupiter or PlanetEnum.Saturn => new DateTime(2020, 3, 1),
                PlanetEnum.Rahu or PlanetEnum.Ketu => new DateTime(2020, 1, 1),
                _ => null
            };

            DateTime? endDate = new DateTime(2023, 01, 01);

            var eventsForPlanet =
                astroEventRepository.Get(e =>
                    e.Planet.PlanetEnum == planetEnum && e.Date.IsBetween(startDate, endDate));

            var script = scriptGenerator.Generate(eventsForPlanet, "Resources/tv_vedic_sun.txt");

            File.WriteAllText(outputDir + $"vedic {planetEnum}.txt", script);
        }
    }
}