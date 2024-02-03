namespace GestioneSagre.Web.Services.Generico;

public class GenericoService : IGenericoService
{
    private readonly HttpClient httpClient;

    public GenericoService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    private readonly string endpointGetIdFestaAttiva = FrontendParameters.ENDPOINT_GET_CATEGORIA_IDFESTA;
    private readonly string endpointGetListaCategorie = FrontendParameters.ENDPOINT_GET_CATEGORIE;

    public async Task<string> GetIdFestaAttivaAsync()
    {
        try
        {
            var result = await httpClient.GetFromJsonAsync<string>($"{endpointGetIdFestaAttiva}") ?? string.Empty;

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

    public async Task<List<CategoriaMinimalViewModel>> GetListCategorieAsync()
    {
        try
        {
            var result = await httpClient.GetFromJsonAsync<List<CategoriaViewModel>>($"{endpointGetListaCategorie}") ?? null;
            var list = new List<CategoriaMinimalViewModel>();

            foreach (var item in result)
            {
                list.Add(new CategoriaMinimalViewModel
                {
                    Id = item.Id,
                    CategoriaVideo = item.CategoriaVideo
                });
            }

            return list;
        }
        catch (HttpRequestException)
        {
            return null;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
}