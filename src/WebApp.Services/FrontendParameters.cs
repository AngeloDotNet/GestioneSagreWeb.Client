namespace GestioneSagre.Web.Services;

public static class FrontendParameters
{
    // Boolean parameters
    public const bool DISABLE_BUTTONS = true;

    // Integer parameters
    public const int NOTIFICATION_NUMBER = 0;

    // String parameters
    public const string TELEGRAM_SUPPORT = "https://t.me/CANALE-SUPPORTO-TELEGRAM";

    // Generic endpoints
    public const string ENDPOINT_GET_HELLOWORLD = "/helloworld/";

    //Endpoint Module Initial Configuration - Festa
    public const string ENDPOINT_GET_FESTE = "/getfeste";
    public const string ENDPOINT_GET_FESTA_ID = "/getfesta";
    public const string ENDPOINT_POST_FESTA = "/festa";
    public const string ENDPOINT_PUT_FESTA = "/festa";

    //Endpoint Module Initial Configuration - Intestazione
    public const string ENDPOINT_GET_INTESTAZIONE_IDFESTA = "/getintestazione";
    public const string ENDPOINT_POST_INTESTAZIONE = "/intestazione";
    public const string ENDPOINT_PUT_INTESTAZIONE = "/intestazione";

    //Endpoint Module Initial Configuration - Impostazione
    public const string ENDPOINT_GET_IMPOSTAZIONE_IDFESTA = "/getimpostazione";
    public const string ENDPOINT_POST_IMPOSTAZIONE = "/impostazione";
    public const string ENDPOINT_PUT_IMPOSTAZIONE = "/impostazione";

    //Endpoint Module Categories
    public const string ENDPOINT_GET_CATEGORIE = "/getcategorie";
    public const string ENDPOINT_GET_CATEGORIA_ID = "/getcategoria";
    public const string ENDPOINT_POST_CATEGORIA = "/categoria";
    public const string ENDPOINT_PUT_CATEGORIA = "/categoria";
    public const string EndPOINT_DELETE_CATEGORIA = "/categoria";
}