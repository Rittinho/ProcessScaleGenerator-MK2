using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using ProcessScaleGenerator;
using ProcessScaleGenerator.Shared.ValueObjects;
using ProcessScaleGenerator.View.Modal.Description;
using ProcessScaleGenerator.View.Modal.Forms;
using ProcessScaleGenerator.View.Modal.Warning;
using ProcessScaleGenerator.ViewModel.Modal.Forms.IconPicker;
using ProcessScaleGenerator.ViewModel.Modal.Forms.TableConfigModal;
using ToyotaProcessManager.Services.Injections.Contract;

namespace ToyotaProcessManager.Services.Injections.Implementation;

public class PopServices : IPopServices
{
    public async Task ShowEmployeePopup(ToyotaEmployee toyotaEmployee)
    {
        var page = GetCurrentPage();

        await page!.ShowPopupAsync(new EmployeeDescriptionModal(toyotaEmployee));
    }
    public async Task ShowProcessPopup(ToyotaProcess toyotaProcess)
    {
        var page = GetCurrentPage();

        await page!.ShowPopupAsync(new ProcessDescriptionModal(toyotaProcess));
    }

    public async Task<IconParameters> IconPickerPopup(IconParameters iconParameters)
    {
        var page = GetCurrentPage();

        var vm = MauiProgram.ServiceProvider.GetRequiredService<IconPickerModalViewModel>();

        var popup = new IconSelecterModal(iconParameters, vm)
        {

        };
        IPopupResult<IconParameters> result = await page!.ShowPopupAsync<IconParameters>(popup);

        if (result.WasDismissedByTappingOutsideOfPopup)
        {
            await WaringPopup("Alterações descartadas!", "As alterações foram perdidas");
            return iconParameters;
        }

        if (result.Result is null)
            return iconParameters;

        return result.Result;
    }
    public async Task<ToyotaTableConfiguration> TableConfigPopup()
    {
        var page = GetCurrentPage();

        var vm = MauiProgram.ServiceProvider.GetRequiredService<TableConfigModalViewModel>();

        IPopupResult<ToyotaTableConfiguration> result = await page!.ShowPopupAsync<ToyotaTableConfiguration>(new TableConfigModal(vm));

        if (result.WasDismissedByTappingOutsideOfPopup)
        {
            await WaringPopup("Alterações descartadas!", "As alterações foram perdidas");
            return null;
        }

        if (result.Result is null)
            return null;

        return result.Result;
    }

    public async Task WaringPopup(Tuple<string, string> token)
    {
        var page = GetCurrentPage();

        IPopupResult<bool> result = await page!.ShowPopupAsync<bool>(new ConfirmActionModal(
        new TokenAction(
            token.Item1,
            token.Item2,
            false)),
            PopupOptions.Empty,
            CancellationToken.None);

        if (result.WasDismissedByTappingOutsideOfPopup || !result.Result!)
            return;

        return;
    }
    public async Task WaringPopup(string title, string description)
    {
        var page = GetCurrentPage();

        IPopupResult<bool> result = await page!.ShowPopupAsync<bool>(new ConfirmActionModal(
        new TokenAction(
            title,
            description,
            false)));

        if (result.WasDismissedByTappingOutsideOfPopup || !result.Result!)
            return;

        return;
    }
    public async Task<bool> ConfirmPopup(Tuple<string, string> token)
    {
        var page = GetCurrentPage();

        IPopupResult<bool> result = await page!.ShowPopupAsync<bool>(new ConfirmActionModal(
          new TokenAction(
              token.Item1,
              token.Item2,
              true)),
              PopupOptions.Empty,
              CancellationToken.None);

        if (result.WasDismissedByTappingOutsideOfPopup || !result.Result!)
            return false;

        return true;
    }
    public async Task<bool> ConfirmPopup(string title, string description)
    {
        var page = GetCurrentPage();

        IPopupResult<bool> result = await page!.ShowPopupAsync<bool>(new ConfirmActionModal(
          new TokenAction(
              title,
              description,
              true)),
              PopupOptions.Empty,
              CancellationToken.None);

        if (result.WasDismissedByTappingOutsideOfPopup || !result.Result!)
            return false;

        return true;
    }

    private Page? GetCurrentPage()
    {
        if (Shell.Current?.CurrentPage != null)
            return Shell.Current.CurrentPage;

        var mainWindow = Application.Current?.Windows.FirstOrDefault();
        return mainWindow?.Page;
    }
}