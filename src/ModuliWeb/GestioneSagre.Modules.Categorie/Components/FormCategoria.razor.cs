namespace GestioneSagre.Modules.Categorie.Components;

public partial class FormCategoria
{
    [Inject] public NavigationManager Navigation { get; set; }
    [Parameter] public CategoriaViewModel Model { get; set; } = new();
    [Parameter] public EventCallback<CategoriaViewModel> OnSave { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }

    private readonly string requiredField = "Campo obbligatorio";

    public async Task SubmitAsync() => await OnSave.InvokeAsync(Model);
    public async Task CancelAsync()
    {
        Model = new();
        await OnCancel.InvokeAsync();

        Navigation.NavigateTo("/Categorie");
    }
}