namespace GestioneSagre.Modules.Categorie.Components;

public partial class FormEditCategoria
{
    [Inject] public ICategorieService Service { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    [Parameter, EditorRequired] public Guid IdDetail { get; set; }
    [Parameter, EditorRequired] public Guid IdFesta { get; set; }
    [Parameter] public EventCallback<CategoriaViewModel> OnSave { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }

    public CategoriaViewModel Model = new();
    private readonly string requiredField = "Campo obbligatorio";

    protected override void OnInitialized() => base.OnInitialized();
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        Model = await Service.GetCategoriaByID(IdDetail, IdFesta);
    }

    public async Task SubmitAsync() => await OnSave.InvokeAsync(Model);
    public void CancelAsync() => Navigation.NavigateTo("/Categorie");
}
