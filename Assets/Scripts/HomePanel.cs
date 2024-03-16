using Base.UI.Panel;
using Cysharp.Threading.Tasks;

public class HomePanel : Panel
{
    public void OnSettingButton()
    {
        PanelManager.Instance.CreatePanel<PopupPanel>(
            panelName: "SettingsPanel", canBack: true
        ).Forget();
    }

    public void OnPlayButton()
    {
        // fade screen to dark
        DarkTransition.Instance.TransitionAsync(async () =>
        {
            // close immediately home panel
            await PanelManager.Instance.ClosePanel(
                panelName: "HomePanel",
                immediately: true
            );

            // create play panel when screen is full dark
            await PanelManager.Instance.CreatePanel<PlayPanel>(
                panelName: "PlayPanel", 
                canBack: false,
                onSetup: panel => panel.Setup()
            );
        });
    }
}