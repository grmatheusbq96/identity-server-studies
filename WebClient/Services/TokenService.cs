﻿using IdentityModel.Client;

namespace WebClient.Services;

public class TokenService : ITokenService
{
    private DiscoveryDocumentResponse _discDocument { get; set; }

    public TokenService()
    {
        using (var client = new HttpClient())
        {
            _discDocument = client.GetDiscoveryDocumentAsync("https://localhost:7039/.well-known/openid-configuration").Result;
        }
    }

    //Criar métodos para diferentes tipos de token (hierarquia, ex: "token gerente", "token aplicação", "token pagamento", "token de extrato")
    public async Task<TokenResponse> GetToken(string scope)
    {
        using (var client = new HttpClient())
        {
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _discDocument.TokenEndpoint,
                ClientId = "cwm.client",
                Scope = scope,
                ClientSecret = "secret"
            });
            if (tokenResponse.IsError)
            {
                throw new Exception("Token Error");
            }
            return tokenResponse;
        }
    }
}