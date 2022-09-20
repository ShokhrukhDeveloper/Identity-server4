using Microsoft.AspNetCore.Components;
using IdentityModel.Client;

namespace client.Pages;
public partial class Index
{
    [Inject]
    public IHttpClientFactory ClientFactory { get; set; }

    public string Content { get; set; } 
    protected override async Task OnInitializedAsync()
    {
        var identityClient=ClientFactory.CreateClient(); 
        var discDoc=await identityClient.GetDiscoveryDocumentAsync("https://localhost:7067/.well-known/openid-configuration"); 
        var tokenResult=await identityClient.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest()
            {
                Address=discDoc.TokenEndpoint,
                ClientId="clientId", 
                ClientSecret="super_hard_to_guess",
                Scope="Api_1"
            }
        );
        var apiClient=ClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResult.AccessToken);
        var content=await apiClient.GetStringAsync("https://localhost:7148/Secret");
        Content=content;
        StateHasChanged();
    }
}