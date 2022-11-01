namespace AstroTools.Common.Service.DataProvider
{
    internal interface IDataProvider<out T, in TReq>
    {
        public T Provide(TReq fileName); 
    }
}
