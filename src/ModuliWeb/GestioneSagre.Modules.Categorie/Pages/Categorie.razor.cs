namespace GestioneSagre.Modules.Categorie.Pages;

public partial class Categorie
{
    [Inject] public ICategorieService Service { get; set; }
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

    private Guid riferimentoIdFesta;
    private int countItems = 0;

    private bool isLoading = false;
    private bool disableBtncategoria = false;

    private readonly bool disableBtnModificaCategoria = false;
    private readonly bool disableBtnEliminaCategoria = FrontendParameters.DISABLE_BUTTONS; //true;

    private async Task LoadDatiAsync()
    {
        try
        {
            //TODO: Sostituire con chiamata async al microservizio OPERAZIONI AVVIO
            riferimentoIdFesta = Guid.Parse("DD43C76F-0EBA-43D2-843A-C497CF80D6C9");

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

    //TODO: Sostituire con chiamata async al microservizio OPERAZIONI AVVIO
    private string nuovaCategoria = $"/categoria/{Guid.Parse("DD43C76F-0EBA-43D2-843A-C497CF80D6C9")}";

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