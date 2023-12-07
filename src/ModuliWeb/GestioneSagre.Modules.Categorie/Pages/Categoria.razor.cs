namespace GestioneSagre.Modules.Categorie.Pages;

public partial class Categoria
{
    [Inject] public ICategorieService Service { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }
    [Parameter] public Guid Id { get; set; }
    [Parameter] public Guid IdFesta { get; set; }

    protected override async Task OnInitializedAsync() => await base.OnInitializedAsync();

    public string errorMessage;

    private CategoriaViewModel model = new();
    private readonly List<BreadcrumbItem> items = new()
    {
        new BreadcrumbItem("Home Page", href: "/"),
        new BreadcrumbItem("Magazzino", href: null, disabled: true),
        new BreadcrumbItem("Categorie", href: "categorie"),
        new BreadcrumbItem("Gestione Categoria", href: null, disabled: true)
    };

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        model = await Service.GetCategoriaByID(Id, IdFesta);
    }

    private async Task SaveCategoriaAsync(CategoriaViewModel model)
    {
        try
        {
            model.Id = Guid.NewGuid();
            model.IdFesta = model.IdFesta;

            var inputModelCategoria = CheckConvertCategoriaModelApi(model);

            if (!await Service.CreateNuovaCategoriaAsync(inputModelCategoria))
            {
                Snackbar.Add("Errore durante la creazione della categoria !", Severity.Error);
            }
            else
            {
                Snackbar.Add("Categoria creata con successo !", Severity.Success);

                Navigation.NavigateTo("/Categorie");
            }
        }
        catch (ArgumentNullException ex)
        {
            errorMessage = ex.Message;
        }
        catch (ApplicationException ex)
        {
            errorMessage = ex.Message;
        }
    }

    private async Task UpdateCategoriaAsync(CategoriaViewModel model)
    {
        var inputModelCategoria = CheckConvertCategoriaModelApi(model);

        try
        {

            if (!await Service.UpdateCategoriaAsync(inputModelCategoria))
            {
                Snackbar.Add("Errore durante l'aggiornamento della categoria !", Severity.Error);
            }
            else
            {
                Snackbar.Add("Categoria aggiornata con successo !", Severity.Success);
                Navigation.NavigateTo("/Categorie");
            }
        }
        catch (ApplicationException ex)
        {
            errorMessage = ex.Message;
        }
        catch (ArgumentNullException ex)
        {
            errorMessage = ex.Message;
        }
    }

    private static CategoriaInputModel CheckConvertCategoriaModelApi(CategoriaViewModel model)
    {
        if (model.CategoriaVideo is null)
        {
            throw new ArgumentNullException($"Il campo {nameof(model.CategoriaVideo)} è obbligatorio");
        }

        if (model.CategoriaStampa is null)
        {
            throw new ArgumentNullException($"Il campo {nameof(model.CategoriaStampa)} è obbligatorio");
        }

        return new CategoriaInputModel
        {
            Id = model.Id,
            IdFesta = model.IdFesta,
            CategoriaVideo = model.CategoriaVideo,
            CategoriaStampa = model.CategoriaStampa
        };
    }
}
