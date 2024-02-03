namespace GestioneSagre.Web.Services.Generico;

public interface IGenericoService
{
    Task<string> GetIdFestaAttivaAsync();
    Task<List<CategoriaMinimalViewModel>> GetListCategorieAsync();
}