namespace GestioneSagre.Modules.Categorie.Pages;

public partial class Categorie
{
    [Inject] public ICategorieService Service { get; set; }
    [Inject] public IGenericoService GenericoService { get; set; }
    [Inject] public ILocalStorageService LocalStorage { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await LoadDatiAsync();
    }

    private readonly List<BreadcrumbItem> items = new List<BreadcrumbItem>
    {
        new BreadcrumbItem("Home Page", href: "/"),
        new BreadcrumbItem("Magazzino", href: null, disabled: true),
        new BreadcrumbItem("Categorie", href: null, disabled: true)
    };

    private List<CategoriaViewModel> listItems = new();
    private string errorMessage;
    private string nuovaCategoria;

    private Guid riferimentoIdFesta;
    private int countItems = 0;

    private bool isLoading = false;
    private bool disableBtncategoria = false;

    private readonly bool disableBtnModificaCategoria = false;
    private readonly bool disableBtnEliminaCategoria = FrontendParameters.DISABLE_EDIT_BUTTONS;

    private async Task LoadDatiAsync()
    {
        try
        {
            var memoryIdFestaAttiva = await LocalStorage.GetItemAsync<string>("IdFestaAttiva");

            if (memoryIdFestaAttiva == null)
            {
                memoryIdFestaAttiva = await GenericoService.GetIdFestaAttivaAsync();
                await LocalStorage.SetItemAsync("IdFestaAttiva", memoryIdFestaAttiva);
            }
            else
            {
                memoryIdFestaAttiva = await LocalStorage.GetItemAsync<string>("IdFestaAttiva");
            }

            var idFestaAttiva = memoryIdFestaAttiva;

            riferimentoIdFesta = Guid.Parse(idFestaAttiva);
            nuovaCategoria = $"/categoria/{Guid.Parse(idFestaAttiva)}";

            isLoading = true;
            listItems = await Service.GetListaCategorie(riferimentoIdFesta);
            countItems = await Service.CountCategorieByIDFesta(riferimentoIdFesta);

            StateHasChanged();

            switch (countItems)
            {
                case >= 6:
                    disableBtncategoria = true;
                    StateHasChanged();
                    break;
                default:
                    disableBtncategoria = false;
                    StateHasChanged();
                    break;
            }
        }
        catch (ApplicationException ex)
        {
            errorMessage = ex.Message;
        }
        finally
        {
            isLoading = false;
        }
    }

    private void ModificaCategoriaAsync(CategoriaViewModel model)
        => Navigation.NavigateTo(errorMessage is null ? $"/categoria/{model.Id}/{model.IdFesta}" : "/categorie");

    private async Task EliminaCategoriaAsync(CategoriaViewModel model)
    {
        try
        {
            isLoading = true;
            if (!await Service.DeleteCategoriaAsync(model.Id, model.IdFesta))
            {
                Snackbar.Add("Errore durante l'eliminazione della categoria !", Severity.Error);
            }

            Snackbar.Add("Categoria eliminata con successo !", Severity.Success);

            await ExecuteLastStepsAsync();
        }
        catch (ApplicationException ex)
        {
            errorMessage = ex.Message;
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task ExecuteLastStepsAsync()
    {
        await LoadDatiAsync();

        await Task.Delay(1500);
        Navigation.NavigateTo("/", true);
    }
}