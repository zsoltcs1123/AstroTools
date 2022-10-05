using SubLordMapper.Service;
using SubLordMapper.Model;
using SubLordMapper.Model.DataTransfer;
using SubLordMapper.Model.Enums;

Console.WriteLine("Hello, World!");

var subLordTable = "Resources\\sub lords.csv";
var ephemFile = "Resources\\ephem_sidereal_krishnamurti_2016-2023_mean.csv";
var moonEphemFile = "Resources\\moon_ephem_sidereal_krishnamurti_2022Mar1_2022Dec31.csv";

var sublords = new SubLordInitializer().Initialize(subLordTable).ToArray();
var ephemeris = new Ephemerides(new EphemerisInitializer<MultiEphemerisDto>().Initialize(ephemFile)
    .Select(e => new Ephemeris(e)));

var moonEphemeris =new Ephemerides(new EphemerisInitializer<MoonEphemerisDto>().Initialize(moonEphemFile)
    .Select(e => new Ephemeris(e)));


ephemeris.AddSubLordTable(sublords);
moonEphemeris.AddSubLordTable(sublords);

var saturn = ephemeris.GetSubLordMapping(new DateTime(2022, 8, 1), Planet.Saturn, new DateTime(2022, 9, 30));

var moonMapping = moonEphemeris.GetSubLordMapping(new DateTime(2022, 8, 1), Planet.Moon, new DateTime(2022, 9, 30));



Console.WriteLine(string.Join(Environment.NewLine,
    moonMapping.Select(s => $"{s.Date.ToString("MM/dd/yyyy HH:mm")} \t| {s.Longitude} \t|{s.SubLord?.Sign} |{s.SubLord?.Nakshatra} |{s.SubLord?.Sub}")));
Console.ReadLine();