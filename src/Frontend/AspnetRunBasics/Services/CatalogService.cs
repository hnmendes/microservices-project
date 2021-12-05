using AspnetRunBasics.Contracts;
using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CatalogViewModel>> GetCatalog()
        {
            var response = await _client.GetAsync("/catalog");
            return await response.ReadContentAs<List<CatalogViewModel>>();
        }

        public async Task<CatalogViewModel> GetCatalog(string id)
        {
            var response = await _client.GetAsync($"/catalog/{id}");
            return await response.ReadContentAs<CatalogViewModel>();
        }

        public async Task<IEnumerable<CatalogViewModel>> GetCatalogByCategory(string category)
        {
            var response = await _client.GetAsync($"/catalog/category/{category}");
            return await response.ReadContentAs<List<CatalogViewModel>>();
        }

        public async Task<CatalogViewModel> CreateCatalog(CatalogViewModel model)
        {
            var response = await _client.PostAsJson($"/catalog", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CatalogViewModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
