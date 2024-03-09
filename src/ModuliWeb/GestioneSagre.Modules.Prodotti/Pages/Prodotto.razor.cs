namespace GestioneSagre.Modules.Prodotti.Pages;

public partial class Prodotto
{
    [Inject] public IProdottiService Service { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }
    [Parameter] public Guid Id { get; set; }
    [Parameter] public Guid IdFesta { get; set; }

    protected override async Task OnInitializedAsync() => await base.OnInitializedAsync();

    public string errorMessage;

    private ProdottoViewModel model = new();
    private readonly List<BreadcrumbItem> items = new()
{
    new BreadcrumbItem("Home Page", href: "/"),
    new BreadcrumbItem("Magazzino", href: null, disabled: true),
    new BreadcrumbItem("Prodotti", href: "prodotti"),
    new BreadcrumbItem("Gestione Prodotto", href: null, disabled: true)
};

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        model = await Service.GetProdottoByID(Id, IdFesta);
    }

    private async Task SaveProdottoAsync(ProdottoViewModel model)
    {
        try
        {
            model.Id = Guid.NewGuid();
            model.IdFesta = model.IdFesta;

            var inputModelProdotto = CheckConvertProdottoModelApi(model);

            if (!await Service.CreateNuovoProdottoAsync(inputModelProdotto))
            {
                Snackbar.Add("Errore durante la creazione del prodotto !", Severity.Error);
            }
            else
            {
                Snackbar.Add("Prodotto creato con successo !", Severity.Success);

                Navigation.NavigateTo("/Prodotti");
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

    private async Task UpdateProdottoAsync(ProdottoViewModel model)
    {
        var inputModelProdotto = CheckConvertProdottoModelApi(model);

        try
        {

            if (!await Service.UpdateProdottoAsync(inputModelProdotto))
            {
                Snackbar.Add("Errore durante l'aggiornamento del prodotto !", Severity.Error);
            }
            else
            {
                Snackbar.Add("Prodotto aggiornato con successo !", Severity.Success);
                Navigation.NavigateTo("/Prodotti");
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

    private static ProdottoInputModel CheckConvertProdottoModelApi(ProdottoViewModel model)
    {
        if (model.Descrizione is null)
        {
            throw new ArgumentNullException($"Il campo {nameof(model.Descrizione)} è obbligatorio");
        }

        if (model.IdCategoria == Guid.Parse("00000000-0000-0000-0000-000000000000"))
        {
            throw new ArgumentNullException($"Il campo Categoria è obbligatorio");
        }

        if (model.SinglePrezzo is null || model.SinglePrezzo == "0")
        {
            throw new ArgumentNullException($"Il campo Prezzo è obbligatorio");
        }

        if (model.Quantita == 0)
        {
            throw new ArgumentNullException($"Il campo {nameof(model.Quantita)} non può avere valore zero");
        }

        return new ProdottoInputModel
        {
            Id = model.Id,
            IdFesta = model.IdFesta,
            Descrizione = model.Descrizione,
            ProdottoAttivo = model.ProdottoAttivo,
            IdCategoria = model.IdCategoria,
            Prezzo = new Price(Convert.ToString(CurrencyValue.EUR), Convert.ToDecimal(model.SinglePrezzo)),
            Quantita = model.Quantita,
            QuantitaAttiva = model.QuantitaAttiva,
            QuantitaScorta = model.QuantitaScorta,
            AvvisoScorta = model.AvvisoScorta,
            Prenotazione = model.Prenotazione,
        };
    }
}
