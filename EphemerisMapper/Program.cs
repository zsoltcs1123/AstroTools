using EphemerisMapper.Service;
using EphemerisMapper.Model.Builder;
using EphemerisMapper.Model.DataTransfer;

Console.WriteLine("Hello, World!");

var ephemFile = "Resources\\ephem_sidereal_krishnamurti_2016-2023_mean.csv";
var moonEphemFile = "Resources\\moon_ephem_sidereal_krishnamurti_2022Mar1_2022Dec31.csv";


var ephemerisBuilder = new EphemerisBuilder();
var ephemerisInitializer = new EphemerisInitializer<MultiEphemerisDto>();

var ephemerides = ephemerisInitializer.Initialize(ephemFile).SelectMany(e => ephemerisBuilder.Build(e));


