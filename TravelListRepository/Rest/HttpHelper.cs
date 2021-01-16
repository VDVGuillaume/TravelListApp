﻿
//  ---------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
// 
//  The MIT License (MIT)
// 
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
// 
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//  ---------------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TravelListRepository.Rest
{
    /// <summary>
    /// Wrapper for making strongly-typed REST calls. 
    /// </summary>
    internal class HttpHelper
    {
        /// <summary>           
        /// The Base URL for the API.
        /// /// </summary>
        private readonly string _baseUrl;

        public HttpHelper(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        /// <summary>
        /// Makes an HTTP GET request to the given controller and returns the deserialized response content.
        /// </summary>
        public async Task<TResult> GetAsync<TResult>(string controller)
        {
            using (var client = BaseClient())
            {
                var response = await client.GetAsync(controller);
                string json = await response.Content.ReadAsStringAsync();
                TResult obj = JsonConvert.DeserializeObject<TResult>(json);
                return obj;
            }
        }

        public async Task<TResult> GetAsync<TResult>(string controller, string query)
        {
            UriBuilder builder = new UriBuilder(_baseUrl);
            builder.Query = query;
            using (var client = BaseClient())
            {
                var response = await client.GetAsync(builder.Uri);
                string json = await response.Content.ReadAsStringAsync();
                TResult obj = JsonConvert.DeserializeObject<TResult>(json);
                return obj;
            }
        }

        /// <summary>
        /// Makes an HTTP POST request to the given controller with the given object as the body.
        /// Returns the deserialized response content.
        /// </summary>
        public async Task<TResult> PostAsync<TRequest, TResult>(string controller, TRequest body)
        {
            using (var client = BaseClient())
            {
                var response = await client.PostAsync(controller, new JsonStringContent(body));
                string json = await response.Content.ReadAsStringAsync();
                TResult obj = JsonConvert.DeserializeObject<TResult>(json);
                return obj;
            }
        }

        /// <summary>
        /// Makes an HTTP POST request to the given controller with the given object as the body.
        /// Returns the deserialized response content.
        /// </summary>
        public async Task<TResult> PutAsync<TRequest, TResult>(string controller, TRequest body)
        {
            using (var client = BaseClient())
            {
                var response = await client.PutAsync(controller, new JsonStringContent(body));
                string json = await response.Content.ReadAsStringAsync();
                TResult obj = JsonConvert.DeserializeObject<TResult>(json);
                return obj;
            }
        }

        /// <summary>
        /// Makes an HTTP DELETE request to the given controller and includes all the given
        /// object's properties as URL parameters. Returns the deserialized response content.
        /// </summary>
        public async Task DeleteAsync(string controller, int objectId)
        {
            using (var client = BaseClient())
            {
                await client.DeleteAsync($"{controller}/{objectId}");
            }
        }

        public async Task<TResult> UploadAsync<TRequest, TResult>(string controller, string ImageName, int TravelListItemID, byte[] file)
        {
            using (var client = BaseClient())
            {

                var form = new MultipartFormDataContent();
                form.Add(new ByteArrayContent(file, 0, file.Count()), "ImageData", ImageName);
                form.Add(new StringContent(TravelListItemID.ToString()), "TravelListItemID");
                form.Add(new StringContent(ImageName.ToString()), "ImageName");

                var response = await client.PostAsync($"{controller}/{TravelListItemID}", form); //No response if filesize exceeds 20 MB

                string json = await response.Content.ReadAsStringAsync();
                TResult obj = JsonConvert.DeserializeObject<TResult>(json);
                return obj;

            }
        }

        /// <summary>
        /// Makes an HTTP GET request to the given controller and returns the deserialized response content.
        /// </summary>
        public async Task<byte[]> DownloadAsync<TResult>(string controller)
        {
            using (var client = BaseClient())
            {
                var response = await client.GetAsync(controller);                
                byte[] mybytearray = await response.Content.ReadAsByteArrayAsync();
                return mybytearray;
            }
        }

        /// <summary>
        /// Constructs the base HTTP client, including correct authorization and API version headers.
        /// </summary>
        private HttpClient BaseClient() => new HttpClient { BaseAddress = new Uri(_baseUrl) };

        /// <summary>
        /// Helper class for formatting <see cref="StringContent"/> as UTF8 application/json. 
        /// </summary>
        private class JsonStringContent : StringContent
        {
            /// <summary>
            /// Creates <see cref="StringContent"/> formatted as UTF8 application/json.
            /// </summary>
            public JsonStringContent(object obj)
                : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            { }
        }
    }
}
