namespace GestioneSagre.Web.Services.Prodotti;

public interface IProdottiService
{
    Task<List<ProdottoViewModel>> GetListaProdotti(Guid idFesta);
    Task<ProdottoViewModel> GetProdottoByID(Guid id, Guid idFesta);
    Task<bool> CreateNuovoProdottoAsync(ProdottoInputModel model);
    Task<bool> UpdateProdottoAsync(ProdottoInputModel model);
    Task<bool> DeleteProdottoAsync(Guid id, Guid idFesta);
    Task<int> CountProdottiByIDFesta(Guid idFesta);
}