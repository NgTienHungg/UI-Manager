using Base.UI.Panel;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class PlayPanel : Panel
{
    [Title("Play Panel")]
    public TextMeshProUGUI frameCount;

    public void Setup()
    {
        // setup các data cần thiết cho panel này
        // hàm này được truyền vào như tham số onSetup khi PanelManager.CreatePanel
        frameCount.text = "Panel has been created at frame " + Time.frameCount;
    }

    public void OnHomeButton()
    {
        // show DarkSceen để che game
        // sau đó xử lý: đóng PlayPanel, tạo HomePanel
        // cuối cùng fadeout DarkScreen để tiếp tục game
        DarkTransition.Instance.TransitionAsync(async () =>
        {
            await PanelManager.Instance.ClosePanel("PlayPanel", immediately: true);

            await PanelManager.Instance.CreatePanel<HomePanel>(
                panelName: "HomePanel", canBack: false
            );
        });
    }
}