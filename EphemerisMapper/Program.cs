﻿using EphemerisMapper.Model;
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


var ephemFile = "Resources\\ephem_sidereal_krishnamurti_2016-2023_mean.csv";
//var moonEphemFile = "Resources\\moon_ephem_sidereal_krishnamurti_2022Mar1_2022Dec31.csv";


var ephemerisBuilder = new EphemerisBuilder(vedicZodiac);
var ephemerisInitializer = new EphemerisInitializer<MultiEphemerisDto>();

var ephemerides = ephemerisInitializer.Initialize(ephemFile).SelectMany(e => ephemerisBuilder.Build(e)).ToList();

var ephemerisManager = new EphemerisManager(ephemerides);

var saturn = ephemerisManager.GetByPlanet(PlanetEnum.Saturn);

Console.ReadLine();
