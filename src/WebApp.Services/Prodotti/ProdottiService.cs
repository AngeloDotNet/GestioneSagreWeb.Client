namespace GestioneSagre.Web.Services.Prodotti;

public class ProdottiService : IProdottiService
{
    private readonly HttpClient httpClient;

    public ProdottiService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    private readonly string endpointGetAllProdotti = FrontendParameters.ENDPOINT_GET_PRODOTTI;
    private readonly string endpointGetProdottoByIdFesta = FrontendParameters.ENDPOINT_GET_PRODOTTO_ID;
    private readonly string endpointPostProdotto = FrontendParameters.ENDPOINT_POST_PRODOTTO;
    private readonly string endpointPutProdotto = FrontendParameters.ENDPOINT_PUT_PRODOTTO;
    private readonly string endpointDeleteProdotto = FrontendParameters.ENDPOINT_DELETE_PRODOTTO;

    public async Task<List<ProdottoViewModel>> GetListaProdotti(Guid idFesta)
    {
        try
        {
            var result = await httpClient.GetFromJsonAsync<List<ProdottoViewModel>>($"{endpointGetAllProdotti}") ?? new();

            var response = result.Where(x => x.IdFesta == idFesta).ToList();

            return response;
        }
        catch (HttpRequestException)
        {
            return new List<ProdottoViewModel>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    public async Task<ProdottoViewModel> GetProdottoByID(Guid id, Guid idFesta)
    {
        try
        {
            var result = await httpClient.GetFromJsonAsync<ProdottoViewModel>($"{endpointGetProdottoByIdFesta}/{id}/{idFesta}") ?? new();

            return result;
        }
        catch (HttpRequestException)
        {
            return new ProdottoViewModel();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    public async Task<bool> CreateNuovoProdottoAsync(ProdottoInputModel model)
    {
        var response = await httpClient.PostAsJsonAsync($"{endpointPostProdotto}", model);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateProdottoAsync(ProdottoInputModel model)
    {
        var response = await httpClient.PutAsJsonAsync($"{endpointPutProdotto}", model);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteProdottoAsync(Guid id, Guid idFesta)
    {
        var response = await httpClient.DeleteAsync($"{endpointDeleteProdotto}/{id}/{idFesta}");

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }

    public async Task<int> CountProdottiByIDFesta(Guid idFesta)
    {
        try
        {
            var listaProdotti = await httpClient.GetFromJsonAsync<List<ProdottoViewModel>>($"{endpointGetAllProdotti}") ?? new();
            var counter = listaProdotti.Where(c => c.IdFesta == idFesta).Count();

            return counter;
        }
        catch (HttpRequestException)
        {
            return 0;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
}