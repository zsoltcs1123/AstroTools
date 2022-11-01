using EphemerisMapper.Model;
using EphemerisMapper.Model.DataTransfer;
using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.Units;
using EphemerisMapper.Service.Builder.CelestialObjects;
using EphemerisMapper.Service.Builder.Cusp;
using EphemerisMapper.Service.Builder.Division;
using EphemerisMapper.Service.Builder.Ephemeris;
using EphemerisMapper.Service.Builder.SubDivision;
using EphemerisMapper.Service.Manager;
using EphemerisMapper.Service.Repository.CelestialObjects;
using EphemerisMapper.Service.Repository.Division;
using EphemerisMapper.Service.Zodiac;


var signCuspGenerator = new CuspGenerator<SignEnum>();
var starCuspGenerator = new CuspGenerator<StarEnum>();

var planetBuilder = new PlanetBuilder();
var planetRepository = new PlanetRepository(planetBuilder);

var starSubDivisionBuilder = new NakshatraSubDivisionBuilder(planetRepository);

var signBuilder = new SignBuilder(signCuspGenerator, planetRepository);
var starBuilder = new NakshatraBuilder(starCuspGenerator, planetRepository, starSubDivisionBuilder);


var signRepository = new DivisionRepository<Sign>(signBuilder);
var starRepository = new DivisionRepository<Nakshatra>(starBuilder);

var vedicZodiac = new VedicZodiac(signRepository, starRepository);

/*var sign = vedicZodiac.GetSign(new Degree(30));
var nakshatra = vedicZodiac.GetNakshatra(new Degree(5, 13, 5));
var sl = vedicZodiac.GetSubLord(new Degree(0, 20, 1));*/

Moon();

Console.ReadLine();


void Moon()
{
    var moonEphemFile = "Resources\\moon_ephem_sidereal_krishnamurti_2022Mar1_2022Dec31.csv";

    var ephemerisBuilder = new EphemerisBuilder(vedicZodiac);
    var ephemerisInitializer = new EphemerisInitializer<MoonEphemerisDto>();

    var ephemerides = ephemerisInitializer.Initialize(moonEphemFile).SelectMany(e => ephemerisBuilder.Build(e))
        .ToList();
    var ephemerisManager = new EphemerisManager(ephemerides);

    var moon = ephemerisManager.GetByPlanet(PlanetEnum.Moon,
        new DateTime(2022, 10, 1), DateTime.Now.AddDays(10));

    foreach (var ephemeris in moon)
    {
        Console.WriteLine($"{ephemeris.Key} [{ephemeris.Value.Longitude.Dec}]: " +
                          $"{ephemeris.Value.Mapped["Sign"].Name} | " +
                          $"{ephemeris.Value.Mapped["Star"].Name} | " +
                          $" {ephemeris.Value.Mapped["SubLord"].Name}");
    }
}

void Multi()
{
    var ephemFile = "Resources\\ephem_sidereal_krishnamurti_2016-2023_mean.csv";

    var ephemerisBuilder = new EphemerisBuilder(vedicZodiac);
    var ephemerisInitializer = new EphemerisInitializer<MultiEphemerisDto>();

    var ephemerides = ephemerisInitializer.Initialize(ephemFile).SelectMany(e => ephemerisBuilder.Build(e)).ToList();

    var ephemerisManager = new EphemerisManager(ephemerides);

    var jup = ephemerisManager.GetByPlanet(PlanetEnum.Jupiter, new DateTime(2022, 1, 1), DateTime.Now.AddMonths(2));

    var today = ephemerisManager.GetByDate(DateTime.Today.AddDays(-3), DateTime.Today.AddDays(2));

/*foreach (var ephemeris in jup)
{
    Console.WriteLine($"{ephemeris.Key} | {ephemeris.Value.Mapped["SubLord"].Name}");
}*/

    foreach (var grp in today)
    {
        Console.WriteLine($"{grp.Key}");
        foreach (var ephemeris in grp)
        {
            Console.WriteLine(
                $"{ephemeris.PlanetEnum} [{ephemeris.Longitude.Dec}]: {ephemeris.Mapped["SubLord"].Name}");
        }

        Console.WriteLine();
    }

    Console.ReadLine();
}