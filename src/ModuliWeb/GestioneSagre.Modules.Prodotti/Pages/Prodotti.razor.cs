namespace GestioneSagre.Modules.Prodotti.Pages;

public partial class Prodotti
{
    [Inject] public IProdottiService Service { get; set; }
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
    new BreadcrumbItem("Prodotti", href: null, disabled: true)
};

    private List<ProdottoViewModel> listItems = new();
    private string errorMessage;
    private string nuovoProdotto;

    private Guid riferimentoIdFesta;
    private int countItems = 0;

    private bool isLoading = false;
    private bool disableBtnProdotto = false;

    private readonly bool disableBtnModificaProdotto = false;
    private readonly bool disableBtnEliminaProdotto = FrontendParameters.DISABLE_EDIT_BUTTONS;

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
            nuovoProdotto = $"/prodotto/{Guid.Parse(idFestaAttiva)}";

            isLoading = true;
            listItems = await Service.GetListaProdotti(riferimentoIdFesta);
            countItems = await Service.CountProdottiByIDFesta(riferimentoIdFesta);

            StateHasChanged();

            switch (countItems)
            {
                case >= 6:
                    disableBtnProdotto = true;
                    StateHasChanged();
                    break;
                default:
                    disableBtnProdotto = false;
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

    private void ModificaProdottoAsync(ProdottoViewModel model)
        => Navigation.NavigateTo(errorMessage is null ? $"/prodotto/{model.Id}/{model.IdFesta}" : "/prodotti");

    private async Task EliminaProdottoAsync(ProdottoViewModel model)
    {
        try
        {
            isLoading = true;
            if (!await Service.DeleteProdottoAsync(model.Id, model.IdFesta))
            {
                Snackbar.Add("Errore durante l'eliminazione del prodotto !", Severity.Error);
            }

            Snackbar.Add("Prodotto eliminato con successo !", Severity.Success);

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