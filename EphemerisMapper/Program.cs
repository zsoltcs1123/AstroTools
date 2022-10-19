using EphemerisMapper.Model;
using EphemerisMapper.Service;
using EphemerisMapper.Model.Builder;
using EphemerisMapper.Model.DataTransfer;
using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.ZodiacPosition;
using EphemerisMapper.Service.Builder;
using EphemerisMapper.Service.Divisions;
using EphemerisMapper.Service.Divisions.Cusps;

Console.WriteLine("Hello, World!");

var ephemFile = "Resources\\ephem_sidereal_krishnamurti_2016-2023_mean.csv";
var moonEphemFile = "Resources\\moon_ephem_sidereal_krishnamurti_2022Mar1_2022Dec31.csv";


/*var ephemerisBuilder = new EphemerisBuilder();
var ephemerisInitializer = new EphemerisInitializer<MultiEphemerisDto>();

var ephemerides = ephemerisInitializer.Initialize(ephemFile).SelectMany(e => ephemerisBuilder.Build(e)).ToList();*/

var cuspGenerator = new CuspGenerator<SignEnum>();
var signBuilder = new SignBuilder(cuspGenerator);
var signProvider = new DivisionProvider<Sign>(signBuilder);

var vedicZodiac = new VedicZodiac(signProvider);

var sign = vedicZodiac.GetSign(new Degree(30));

Console.ReadLine();
