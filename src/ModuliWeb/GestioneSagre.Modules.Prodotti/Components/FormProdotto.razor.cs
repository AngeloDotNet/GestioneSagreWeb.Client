namespace GestioneSagre.Modules.Prodotti.Components;

public partial class FormProdotto
{
    [Inject] public NavigationManager Navigation { get; set; }
    [Inject] public ILocalStorageService LocalStorage { get; set; }
    [Inject] public IGenericoService GenericoService { get; set; }
    [Parameter] public ProdottoViewModel Model { get; set; } = new();
    [Parameter] public EventCallback<ProdottoViewModel> OnSave { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }

    private CurrencyValue EnumValue { get; set; } = CurrencyValue.EUR;
    private readonly string requiredField = "Campo obbligatorio";
    private readonly string customField = "Campo obbligatorio, usare la virgola per i decimali";

    private bool EnableScorta { get; set; } = false;
    public List<CategoriaMinimalViewModel> ListaCategorie { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        EnableScorta = true;

        var listaCategorie = await LocalStorage.GetItemAsync<List<CategoriaMinimalViewModel>>("ListaCategorie");

        if (listaCategorie is null || listaCategorie.Count == 0)
        {
            ListaCategorie = await GenericoService.GetListCategorieAsync();
            await LocalStorage.SetItemAsync("ListaCategorie", ListaCategorie);
        }
        else
        {
            ListaCategorie = listaCategorie;
        }
    }

    public async Task SubmitAsync() => await OnSave.InvokeAsync(Model);
    public async Task CancelAsync()
    {
        Model = new();
        await OnCancel.InvokeAsync();

        Navigation.NavigateTo("/Prodotti");
    }

    private void ChangeStatusProdottoAttivo(bool isChecked) => Model.ProdottoAttivo = isChecked;
    private void ChangeStatusPrenotazione(bool isChecked) => Model.Prenotazione = isChecked;
    private void ChangeStatusQuantitaAttiva(bool isChecked)
    {
        if (isChecked)
        {
            Model.QuantitaAttiva = isChecked;
            Model.QuantitaScorta = 0;
            EnableScorta = false;
        }
        else
        {
            Model.QuantitaAttiva = isChecked;
            Model.QuantitaScorta = 0;
            EnableScorta = true;
        }
    }

    private void ChangeStatusAvvisoScorta(bool isChecked) => Model.AvvisoScorta = isChecked;
}