using FastCommerce.Business.ElasticSearch.Abstract;
using Nest;

namespace FastCommerce.Business.ElasticSearch.Concrete
{
    public class ElasticEntity<TEntityKey> : IElasticEntity<TEntityKey>
    {
        public virtual TEntityKey Id { get; set; }
        public virtual CompletionField Suggest { get; set; }
        public virtual string SearchingArea { get; set; }
        public virtual double? Score { get; set; }
    } 
}