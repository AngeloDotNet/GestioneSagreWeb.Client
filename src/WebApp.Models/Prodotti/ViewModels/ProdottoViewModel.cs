﻿namespace GestioneSagre.Web.Models.Prodotti.ViewModels;

public class ProdottoViewModel
{
    public Guid Id { get; set; }
    public Guid IdFesta { get; set; }
    public string Descrizione { get; set; }
    public bool ProdottoAttivo { get; set; }
    public Guid IdCategoria { get; set; }
    public string SinglePrezzo { get; set; }
    public virtual Price Prezzo { get; set; }
    public int Quantita { get; set; }
    public bool QuantitaAttiva { get; set; }
    public int QuantitaScorta { get; set; }
    public bool AvvisoScorta { get; set; }
    public bool Prenotazione { get; set; }
}