using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonWithOrderChange : MonoBehaviour, IPointerEnterHandler
{
    // 마우스가 버튼 위로 이동했을 때 호출됩니다.
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 버튼 오브젝트를 형제 오브젝트들 중에서 마지막으로 설정합니다.
        transform.SetAsLastSibling();
    }
}
