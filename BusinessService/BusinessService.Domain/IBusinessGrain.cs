namespace BusinessService.Domain
{
    internal interface IBusinessGrain
    {
        Task<Business> Get();
        Task<Business> Create(Name name, UserName userName, Media? media);
    }
}
