﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibHttpClient
{
    public class HttpClient
    {
        public bool IsException { get; private set; }
        public Exception? Exception { get; private set; }

        public System.Net.Http.HttpResponseMessage? HttpResponseMessage { get; private set; }

        private void Reset()
        {
            IsException = false;
            Exception = null;
            HttpResponseMessage = null;
        }

        public void CertOverride() => ServicePointManager.ServerCertificateValidationCallback += (sender1, certificate, chain, sslPolicyErrors) => true;
        public void CertNormal() => ServicePointManager.ServerCertificateValidationCallback -= (sender1, certificate, chain, sslPolicyErrors) => true;

        public async Task<string> Get_Token(string url, string user, string pass)
        {
            Reset();

            try
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new System.Uri(url);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes($"{user}:{pass}")));
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));

                    System.Net.Http.HttpContent content = new System.Net.Http.StringContent("", UTF8Encoding.UTF8, "*/*");
                    HttpResponseMessage = await client.PutAsync(url, content);

                    if (HttpResponseMessage.IsSuccessStatusCode)
                        return HttpResponseMessage.Headers.GetValues("Authorization").FirstOrDefault();
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                Exception = ex;
                IsException = true;
                return null;
            }
        }

        public async Task<bool> Post(string url, string data, string token)
        {
            Reset();

            try
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new System.Uri(url);
                    if (!string.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));

                    System.Net.Http.HttpContent content = new System.Net.Http.StringContent(data, UTF8Encoding.UTF8, "*/*");
                    HttpResponseMessage = await client.PostAsync(url, content);

                    return HttpResponseMessage.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Exception = ex;
                IsException = true;
                return false;
            }

        }
        public async Task<bool> Put(string url, string data, string token)
        {
            Reset();

            try
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new System.Uri(url);
                    if (!string.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));

                    System.Net.Http.HttpContent content = new System.Net.Http.StringContent(data, UTF8Encoding.UTF8, "*/*");
                    HttpResponseMessage = await client.PutAsync(url, content);

                    return HttpResponseMessage.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Exception = ex;
                IsException = true;
                return false;
            }
        }
        public async Task<bool> Patch(string url, string data, string token)
        {
            Reset();

            try
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new System.Uri(url);
                    if (!string.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));

                    System.Net.Http.HttpContent content = new System.Net.Http.StringContent(data, UTF8Encoding.UTF8, "*/*");
                    var request = new System.Net.Http.HttpRequestMessage(new System.Net.Http.HttpMethod("PATCH"), url)
                    { Content = content };

                    HttpResponseMessage = await client.SendAsync(request);

                    return HttpResponseMessage.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Exception = ex;
                IsException = true;
                return false;
            }

        }
        public async Task<bool> Delete(string url, string token)
        {
            Reset();

            try
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new System.Uri(url);
                    if (!string.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));

                    HttpResponseMessage = await client.DeleteAsync(url);

                    return HttpResponseMessage.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Exception = ex;
                IsException = true;
                return false;
            }

        }
        public async Task<string> Get(string url, string token)
        {
            Reset();

            try
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new System.Uri(url);
                    if (!string.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));

                    HttpResponseMessage = await client.GetAsync(url);

                    if (HttpResponseMessage.IsSuccessStatusCode)
                        return HttpResponseMessage.Content.ReadAsStringAsync().Result;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                Exception = ex;
                IsException = true;
                return null;
            }
        }
        public async Task<byte[]> GetBytes(string url, string token)
        {
            Reset();

            try
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new System.Uri(url);
                    if (!string.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("image/bmp"));

                    HttpResponseMessage = await client.GetAsync(url);

                    if (HttpResponseMessage.IsSuccessStatusCode)
                        return HttpResponseMessage.Content.ReadAsByteArrayAsync().Result;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                Exception = ex;
                IsException = true;
                return null;
            }
        }

        public async Task<Stream> Stream(string url, string token)
        {
            Reset();

            try
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new System.Uri(url);
                    if (!string.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/event-stream"));

                    return await client.GetStreamAsync(url);
                }
            }
            catch (Exception ex)
            {
                Exception = ex;
                IsException = true;
                return null;
            }
        }

    }
}
