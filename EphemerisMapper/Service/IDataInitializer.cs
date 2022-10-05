namespace EphemerisMapper.Service
{
    internal interface IDataInitializer<T, Req>
    {
        public T Initialize(Req fileName); 
    }
}
