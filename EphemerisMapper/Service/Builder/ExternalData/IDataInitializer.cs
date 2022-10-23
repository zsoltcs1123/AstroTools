namespace EphemerisMapper.Service.Builder.ExternalData
{
    internal interface IDataInitializer<T, Req>
    {
        public T Initialize(Req fileName); 
    }
}
