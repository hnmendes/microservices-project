using AspnetRunBasics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics.Contracts
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogViewModel>> GetCatalog();
        Task<IEnumerable<CatalogViewModel>> GetCatalogByCategory(string category);
        Task<CatalogViewModel> GetCatalog(string id);
        Task<CatalogViewModel> CreateCatalog(CatalogViewModel model);
    }
}
