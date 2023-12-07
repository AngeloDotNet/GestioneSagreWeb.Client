namespace GestioneSagre.Web.App.Shared;

public partial class UserDisplay
{
    private readonly int numberNotifications = FrontendParameters.NOTIFICATION_NUMBER;

    protected override async Task OnInitializedAsync() => await base.OnInitializedAsync();
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        //OnChangeNumber(0);
    }

    private void OpenAboutPage() => Navigation.NavigateTo("/about");

    //private void StartSupportChat() => Navigation.NavigateTo(FrontendParameters.TELEGRAM_SUPPORT, true);

    // private void OnChangeNumber(int value)
    // {
    //     NumberNotifications = value;
    //     StateHasChanged();
    // }
}