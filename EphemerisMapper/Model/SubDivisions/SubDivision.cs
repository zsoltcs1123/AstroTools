﻿namespace EphemerisMapper.Model.SubDivisions;

public record SubDivision(string Name, IEnumerable<SubDivisionRange> Ranges);