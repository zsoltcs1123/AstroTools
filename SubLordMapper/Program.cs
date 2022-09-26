// See https://aka.ms/new-console-template for more information
using SubLordMapper.Service;
using SubLordMapper.Model;

Console.WriteLine("Hello, World!");

var subLordTable = "Resources\\sub lords.csv";
var ephemFile = "Resources\\ephem_sidereal_krishnamurti_2016-2023_mean.csv"; 

var sublords = new SubLordInitializer().Initialize(subLordTable).ToArray();
var ephemeris = new EphemerisInitializer().Initialize(ephemFile);

ephemeris.AddSubLordTable(sublords);

var mapping = ephemeris.GetSubLordMapping(new DateTime(2020, 1, 1), Planet.Jupiter, new DateTime(2022, 9,30));

Console.WriteLine(string.Join(Environment.NewLine, mapping.Select(s => $"{s.Date} \t| {s.Longitude} \t| {s.SubLord.Sub}")));
Console.ReadLine();