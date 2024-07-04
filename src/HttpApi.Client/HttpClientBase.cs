﻿using System.Net.Http.Headers;
using System.Text.Json;

namespace CleanArch.eCode.HttpApi.Client;

public abstract class HttpClientBase(IHttpClientFactory httpClientFactory)
{
    // for inherit classes can overide ClientName
    protected virtual string ClientName { get; } = HttpClientConsts.BackendApi;

    // for inherit classes can config client timeout
    protected virtual int ClientTimeoutSeconds { get; } = 1800;

    // for inherit classes can config client timeout
    protected virtual JsonSerializerOptions JsonSerializerOptions { get; } = HttpClientConsts.JsonSerializerOptions;

    public HttpClient HttpClient
    {
        get
        {
            return CreateClient();
        }
    }

    private HttpClient CreateClient(string? token = null)
    {
        var client = httpClientFactory.CreateClient(ClientName);
        client.Timeout = TimeSpan.FromSeconds(ClientTimeoutSeconds);

        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return client;
    }
}
