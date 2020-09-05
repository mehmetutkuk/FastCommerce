namespace FastCommerce.Business.ElasticSearch.Abstract
{
    public interface IElasticSearchConfigration
    {
        string ConnectionString { get; }
        string AuthUserName { get; }
        string AuthPassWord { get; }
    }
}