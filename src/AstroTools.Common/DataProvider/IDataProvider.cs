namespace AstroTools.Common.DataProvider
{
    internal interface IDataProvider<out T, in TReq>
    {
        public T Provide(TReq fileName); 
    }
}
