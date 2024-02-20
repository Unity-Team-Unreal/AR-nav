using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.UI;

public class SelectPhotoVideo : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Button photoModeButton;
    [SerializeField] private Button videoModeButton;

    [SerializeField] private Button photoBtn;
    [SerializeField] private Button videoBtn;

    [SerializeField] private Animator swipeAnimator;

    private bool isPhotoSelected = true; // �ʱ⿡�� ������ ���õ�
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    private float swipeLength = 0.1f;

    private void Awake()
    {
        photoModeButton.onClick.AddListener(SelectPhoto);
        videoModeButton.onClick.AddListener(SelectVideo);
    }

    void Start()
    {
        photoBtn.gameObject.SetActive(true);
        videoBtn.gameObject.SetActive(false);
        
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
        if (isPhotoSelected)
            return;

        isPhotoSelected = true;

        //������� �����ϰ�, �����Կ� ��ư���� ����
        swipeAnimator.SetTrigger("DoPhotoSwipe");
        photoBtn.gameObject.SetActive(true);
        videoBtn.gameObject.SetActive(false);
    }

    void SelectVideo()
    {
        if(!isPhotoSelected)
            return;

        isPhotoSelected = false;

        //������ ���� �����ϰ�, ������ �Կ� ��ư���� ����
        swipeAnimator.SetTrigger("DoVideoSwipe");
        photoBtn.gameObject.SetActive(false);
        videoBtn.gameObject.SetActive(true);
    }
}
