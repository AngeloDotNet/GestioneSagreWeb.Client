﻿namespace GestioneSagre.Web.Shared.Components;

public partial class EditorForm<TContent>
{
    [Parameter] public TContent Model { get; set; } = default!;
    [Parameter] public RenderFragment<TContent> ContentInfo { get; set; } = default!;
    [Parameter] public EventCallback<TContent> OnSave { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }

    [Inject] public IDialogService Dialog { get; set; } = default!;

    private EditContext editContext;
    private readonly bool disableBtnSave = FrontendParameters.DISABLE_EDIT_BUTTONS;

    protected override void OnParametersSet()
    {
        editContext = new EditContext(Model);
    }

    public async Task SubmitAsync() => await OnSave.InvokeAsync(Model);
    public async Task CancelAsync() => await OnCancel.InvokeAsync();
}