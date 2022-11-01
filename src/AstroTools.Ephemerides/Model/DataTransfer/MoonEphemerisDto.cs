namespace AstroTools.Ephemerides.Model.DataTransfer;

public record MoonEphemerisDto(DateTime Date, decimal Moon) : EphemerisDto(Date);