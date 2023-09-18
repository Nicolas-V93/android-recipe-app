using Imi.Project.Mobile.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public async Task<IEnumerable<T>> GetAllAsync<T>(string url, string token = "")
        {
            try
            {
                string responseStream = string.Empty;

                HttpClient httpClient = CreateHttpClient(token);
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    responseStream = await response.Content.ReadAsStringAsync();

                    var data = JsonConvert.DeserializeObject<IEnumerable<T>>(responseStream);
                    return data;
                }

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new AuthenticationException(responseStream);
                }

                throw new HttpRequestException(responseStream);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<T> PostAsync<T>(T entity, string url, string token = "")
        {
            try
            {
                string responseStream = string.Empty;
                HttpClient httpClient = CreateHttpClient(token);

                string json = JsonConvert.SerializeObject(entity);

                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    responseStream = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<T>(responseStream);
                    return data;

                }

                responseStream = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new AuthenticationException(responseStream);
                }

                throw new HttpRequestException(responseStream);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TResponse> PostAsync<T, TResponse>(T entity, string url, string token = "")
        {
            try
            {
                string responseStream = string.Empty;
                HttpClient httpClient = CreateHttpClient(token);

                string json = JsonConvert.SerializeObject(entity);

                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    responseStream = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<TResponse>(responseStream);
                    return data;
                }

                responseStream = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new AuthenticationException(responseStream);
                }

                throw new HttpRequestException(responseStream);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task DeleteAsync(Guid id, string url, string token = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(token);
                var response = await httpClient.DeleteAsync(url);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<T> PutAsync<T>(T entity, string url, string token = "")
        {
            try
            {
                string responseStream = string.Empty;
                HttpClient httpClient = CreateHttpClient(token);

                string json = JsonConvert.SerializeObject(entity);

                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(url, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    responseStream = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<T>(responseStream);
                    return data;

                }

                responseStream = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new AuthenticationException(responseStream);
                }

                throw new HttpRequestException(responseStream);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Task<T> GetAsync<T>(Guid id, string url, string token = "")
        {
            throw new System.NotImplementedException();
        }
        private HttpClient CreateHttpClient(string token)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            var httpClient = new HttpClient(clientHandler);
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;

        }

    }
}
