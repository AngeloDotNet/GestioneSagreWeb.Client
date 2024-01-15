namespace GestioneSagre.Web.Services.Categorie;

public interface ICategorieService
{
    Task<List<CategoriaViewModel>> GetListaCategorie(Guid idFesta);
    Task<CategoriaViewModel> GetCategoriaByID(Guid id, Guid idFesta);
    Task<bool> CreateNuovaCategoriaAsync(CategoriaInputModel model);
    Task<bool> UpdateCategoriaAsync(CategoriaInputModel model);
    Task<bool> DeleteCategoriaAsync(Guid id, Guid idFesta);
    Task<int> CountCategorieByIDFesta(Guid idFesta);
}