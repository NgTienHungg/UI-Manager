using Base.UI.Panel;
using Sirenix.OdinInspector;

/// <summary>
/// Những panel có dạng Popup như này thường sẽ được kế thừa từ PopupPanel
/// Xử lý animation tách ra thành 2 thứ: fade BG và scale popup (đã xử lý trong PopupPanel)
/// </summary>
public class SettingsPanel : PopupPanel
{
    [Title("Settings Panel")]
    public bool customAnimation;

    public override void Open()
    {
        if (customAnimation)
        {
            // custom your animation... (DOTween)
            return;
        }
        else
        {
            base.Open();
        }
    }
}