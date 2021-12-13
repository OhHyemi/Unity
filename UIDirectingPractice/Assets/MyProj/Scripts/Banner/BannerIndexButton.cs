using UnityEngine;
using UnityEngine.Events;

public class BannerIndexButton : MonoBehaviour
{
    private int index;
    [SerializeField]
    private GameObject obj_highlight;
    private event UnityAction<int> onSelect;

    public void Initialize(int index, UnityAction<int> onSelect, bool isSelected)
    {
        this.index = index;
        this.onSelect += onSelect;
        obj_highlight.SetActive(isSelected);
    }

    public void OnClickButton()
    {
        onSelect?.Invoke(index);
    }

    public void SelectWithoutNotify(bool on)
    {
        obj_highlight.SetActive(on);
    }
}
