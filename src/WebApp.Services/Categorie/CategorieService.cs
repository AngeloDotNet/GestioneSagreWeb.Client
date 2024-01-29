namespace GestioneSagre.Web.Services.Categorie;

public class CategorieService : ICategorieService
{
    private readonly HttpClient httpClient;

    public CategorieService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    private readonly string endpointGetAllCategorie = FrontendParameters.ENDPOINT_GET_CATEGORIE;
    private readonly string endpointGetCategoriaByIdFesta = FrontendParameters.ENDPOINT_GET_CATEGORIA_ID;
    private readonly string endpointPostCategoria = FrontendParameters.ENDPOINT_POST_CATEGORIA;
    private readonly string endpointPutCategoria = FrontendParameters.ENDPOINT_PUT_CATEGORIA;
    private readonly string endpointDeleteCategoria = FrontendParameters.ENDPOINT_DELETE_CATEGORIA;

    public async Task<List<CategoriaViewModel>> GetListaCategorie(Guid idFesta)
    {
        try
        {
            var result = await httpClient.GetFromJsonAsync<List<CategoriaViewModel>>($"{endpointGetAllCategorie}") ?? new();

            var response = result.Where(x => x.IdFesta == idFesta).ToList();

            return response;
        }
        catch (HttpRequestException)
        {
            return new List<CategoriaViewModel>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    public async Task<CategoriaViewModel> GetCategoriaByID(Guid id, Guid idFesta)
    {
        try
        {
            var result = await httpClient.GetFromJsonAsync<CategoriaViewModel>($"{endpointGetCategoriaByIdFesta}/{id}/{idFesta}") ?? new();

            return result;
        }
        catch (HttpRequestException)
        {
            return new CategoriaViewModel();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    public async Task<bool> CreateNuovaCategoriaAsync(CategoriaInputModel model)
    {
        var response = await httpClient.PostAsJsonAsync($"{endpointPostCategoria}", model);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCategoriaAsync(CategoriaInputModel model)
    {
        var response = await httpClient.PutAsJsonAsync($"{endpointPutCategoria}", model);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteCategoriaAsync(Guid id, Guid idFesta)
    {
        var response = await httpClient.DeleteAsync($"{endpointDeleteCategoria}/{id}/{idFesta}");

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }

    public async Task<int> CountCategorieByIDFesta(Guid idFesta)
    {
        try
        {
            var listaCategorie = await httpClient.GetFromJsonAsync<List<CategoriaViewModel>>($"{endpointGetAllCategorie}") ?? new();
            var counter = listaCategorie.Where(c => c.IdFesta == idFesta).Count();

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