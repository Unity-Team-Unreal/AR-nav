using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonWithOrderChange : MonoBehaviour, IPointerEnterHandler
{
    // ���콺�� ��ư ���� �̵����� �� ȣ��˴ϴ�.
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ư ������Ʈ�� ���� ������Ʈ�� �߿��� ���������� �����մϴ�.
        transform.SetAsLastSibling();
    }
}
