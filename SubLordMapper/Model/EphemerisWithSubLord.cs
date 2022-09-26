namespace SubLordMapper.Model
{
    internal record EphemerisWithSubLord(Ephemeris Entry, 
        SubLord Sun, SubLord Mercury, SubLord Venus, SubLord Mars, SubLord Jupiter, SubLord Saturn, SubLord Rahu, SubLord Ketu);
}
