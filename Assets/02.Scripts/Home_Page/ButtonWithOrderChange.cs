using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ��ư ȣ������ ������Ʈ ���̾� ��������
/// </summary>
public class ButtonWithOrderChange : MonoBehaviour, IPointerEnterHandler
{
    // ���콺�� ��ư ���� �̵����� �� ȣ��˴ϴ�.
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ư ������Ʈ�� ���� ������Ʈ�� �߿��� ���������� �����մϴ�.
        transform.SetAsLastSibling();
    }
}
