using System;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 10000;
        public int PageIndex {get; set;} = 1;
        private int _pageSize = 9;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public Guid? BrandId { get; set; }
        public Guid? TypeId { get; set; }
        public string Sort { get; set; }
        private string _search;
        public string Search 
        { 
            get => _search;
            set => _search = value.ToLower();
         }
    }
}