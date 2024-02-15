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

    private bool isPhotoSelected = true; // 초기에는 사진이 선택됨
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

        if (Mathf.Abs(deltaX) > swipeLength) // 스와이프 거리가 일정 이상이면 선택 변경
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
        //포토모드로 변경하고, 사진촬영 버튼으로 변경
    }

    void SelectVideo()
    {
        isPhotoSelected = false;
        Debug.Log("Video selected");
        //동영상 모드로 변경하고, 동영상 촬영 버튼으로 변경
    }
}
