using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.UI;

public class SelectPhotoVideo : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Button photoButton;
    [SerializeField] private Button videoButton;

    private bool isPhotoSelected = true; // �ʱ⿡�� ������ ���õ�
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    private float swipeLength = 2f;

    void Start()
    {
        photoButton.onClick.AddListener(SelectPhoto);
        videoButton.onClick.AddListener(SelectVideo);
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        fingerUpPosition = eventData.position;
        CheckSwipe();
    }

    void CheckSwipe()
    {
        float deltaX = fingerUpPosition.x - fingerDownPosition.x;

        if (Mathf.Abs(deltaX) > swipeLength) // �������� �Ÿ��� ���� �̻��̸� ���� ����
        {
            if (deltaX > 0)
            {
                if (!isPhotoSelected)
                    SelectPhoto();
            }
            else
            {
                if (isPhotoSelected)
                    SelectVideo();
            }
        }
    }

    public void OnSwipeStart(BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;
        fingerDownPosition = pointerData.position;
    }

    void SelectPhoto()
    {
        isPhotoSelected = true;
        Debug.Log("Photo selected");
        //������� �����ϰ�, �����Կ� ��ư���� ����
    }

    void SelectVideo()
    {
        isPhotoSelected = false;
        Debug.Log("Video selected");
        //������ ���� �����ϰ�, ������ �Կ� ��ư���� ����
    }
}
