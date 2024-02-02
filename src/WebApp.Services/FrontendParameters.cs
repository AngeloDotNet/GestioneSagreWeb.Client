namespace GestioneSagre.Web.Services;

public static class FrontendParameters
{
    // PARAMETRI BOOL
    public const bool DISABLE_EDIT_BUTTONS = true; //Se TRUE disabilita il salvataggio delle form

    // PARAMETRI INTEGER
    public const int NOTIFICATION_NUMBER = 0;

    // PARAMETRI STRING
    public const string TELEGRAM_SUPPORT = "https://t.me/CANALE-SUPPORTO-TELEGRAM";

    // ENDPOINT GENERICI
    public const string ENDPOINT_GET_HELLOWORLD = "/helloworld/";

    //ENDPOINT MODULO CONFIGURAZIONE INIZIALE - FESTA
    public const string ENDPOINT_GET_FESTE = "/getfeste";
    public const string ENDPOINT_GET_FESTA_ID = "/getfesta";
    public const string ENDPOINT_POST_FESTA = "/festa";
    public const string ENDPOINT_PUT_FESTA = "/festa";

    //ENDPOINT MODULO CONFIGURAZIONE INIZIALE - INTESTAZIONE
    public const string ENDPOINT_GET_INTESTAZIONE_IDFESTA = "/getintestazione";
    public const string ENDPOINT_POST_INTESTAZIONE = "/intestazione";
    public const string ENDPOINT_PUT_INTESTAZIONE = "/intestazione";

    //ENDPOINT MODULO CONFIGURAZIONE INIZIALE - IMPOSTAZIONE
    public const string ENDPOINT_GET_IMPOSTAZIONE_IDFESTA = "/getimpostazione";
    public const string ENDPOINT_POST_IMPOSTAZIONE = "/impostazione";
    public const string ENDPOINT_PUT_IMPOSTAZIONE = "/impostazione";

    //ENDPOINT MODULO CATEGORIE
    public const string ENDPOINT_GET_CATEGORIE = "/getcategorie";
    public const string ENDPOINT_GET_CATEGORIA_ID = "/getcategoria";
    public const string ENDPOINT_GET_CATEGORIA_IDFESTA = "/getidfesta";
    public const string ENDPOINT_POST_CATEGORIA = "/categoria";
    public const string ENDPOINT_PUT_CATEGORIA = "/categoria";
    public const string ENDPOINT_DELETE_CATEGORIA = "/categoria";

    //ENDPOINT MODULO PRODOTTI
    public const string ENDPOINT_GET_PRODOTTI = "/getprodotti";
    public const string ENDPOINT_GET_PRODOTTO_ID = "/getprodotto";
    public const string ENDPOINT_POST_PRODOTTO = "/prodotto";
    public const string ENDPOINT_PUT_PRODOTTO = "/prodotto";
    public const string ENDPOINT_DELETE_PRODOTTO = "/prodotto";
}