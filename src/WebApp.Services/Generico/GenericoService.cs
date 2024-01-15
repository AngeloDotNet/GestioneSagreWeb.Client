namespace GestioneSagre.Web.Services.Generico;

public class GenericoService : IGenericoService
{
    public HttpClient HttpClient { get; set; }

    public GenericoService(HttpClient httpClient)
    {
        this.HttpClient = httpClient;
    }

    private readonly string endpointGetIdFestaAttiva = FrontendParameters.ENDPOINT_GET_CATEGORIA_IDFESTA;

    public async Task<string> GetIdFestaAttivaAsync()
    {
        try
        {
            var result = await HttpClient.GetFromJsonAsync<string>($"{endpointGetIdFestaAttiva}") ?? string.Empty;

            return result;
        }
        catch (HttpRequestException)
        {
            return string.Empty;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
}