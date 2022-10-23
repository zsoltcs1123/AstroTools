﻿using EphemerisMapper.Model.DataTransfer;
using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.Units;
using EphemerisMapper.Service.Zodiac;

namespace EphemerisMapper.Service.Builder.Ephemeris;

public class EphemerisBuilder
{
    private readonly IZodiac _zodiac;

    public EphemerisBuilder(IZodiac zodiac)
    {
        _zodiac = zodiac;
    }

    public IEnumerable<Model.Units.Ephemeris> Build(MultiEphemerisDto dto)
    {
        return Enum.GetValues<PlanetEnum>()
            .Where(p => p != PlanetEnum.Moon)
            .Select(p =>
            {
                var degrees = GetDegreesForPlanet(p, dto);
                return new Model.Units.Ephemeris(p, degrees, 0, dto.Date, _zodiac.Map(degrees));
            });
    }

    private static Degree GetDegreesForPlanet(PlanetEnum planetEnum, MultiEphemerisDto dto) => new(planetEnum switch
    {
        PlanetEnum.Ketu => dto.SouthNode,
        PlanetEnum.Venus => dto.Venus,
        PlanetEnum.Sun => dto.Sun,
        PlanetEnum.Mars => dto.Mars,
        PlanetEnum.Rahu => dto.MeanNode,
        PlanetEnum.Jupiter => dto.MeanNode,
        PlanetEnum.Saturn => dto.MeanNode,
        PlanetEnum.Mercury => dto.MeanNode,
        _ => 0m
    });
}