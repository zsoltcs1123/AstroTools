using AstroTools.Common.Extensions;
using AstroTools.Common.Repository;
using AstroTools.Common.Service.DataProvider;
using AstroTools.Ephemerides.Factory;
using AstroTools.Ephemerides.Model.DataTransfer;
using AstroTools.Events.Factory;
using AstroTools.Events.Model;
using AstroTools.Scripts.Service;
using AstroTools.Zodiac.Factory;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;
using AstroTools.Zodiac.Model.Enums;
using AstroTools.Zodiac.Service;
using AstroTools.Zodiac.Service.Cusp;
using AstroTools.Zodiac.Service.Repository;
using AstroTools.Zodiac.Service.SubDivision;


var signCuspGenerator = new CuspGenerator<SignEnum>();
var starCuspGenerator = new CuspGenerator<StarEnum>();

var planetFactory = new PlanetFactory();
var planetRepository = new PlanetRepository(planetFactory);

var starSubDivisionBuilder = new NakshatraSubDivisionBuilder(planetRepository);

var signFactory = new SignFactory(signCuspGenerator, planetRepository, null);
var starFactory = new NakshatraFactory(starCuspGenerator, planetRepository, starSubDivisionBuilder);

var signRepository = new DivisionRepository<Sign>(signFactory);
var starRepository = new DivisionRepository<Nakshatra>(starFactory);

var vedicZodiac = new VedicZodiac(signRepository, starRepository);

var ephemFile = "Resources\\ephem_sidereal_krishnamurti_2022-2023_4h_mean.csv";
var ephemerisBuilder = new EphemerisFactory(vedicZodiac, planetFactory);
var ephemerisInitializer = new CsvDataProvider<MultiEphemerisDto>();

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
        astroEventRepository.Get(e => e.Planet.PlanetEnum == planetEnum && e.Date.IsBetween(startDate, endDate));

    var script = scriptGenerator.Generate(eventsForPlanet, "Resources/tv_vedic_sun.txt");

    File.WriteAllText(outputDir + $"vedic {planetEnum}.txt", script);
}