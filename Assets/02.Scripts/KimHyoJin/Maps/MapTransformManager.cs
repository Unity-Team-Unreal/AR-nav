using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapTransformManager : MonoBehaviour
{
    [Header("������ �ּ�, �ִ� Ȯ�� ����")]
    [SerializeField] float minSize;
    [SerializeField] float maxSize;
    [Header("����,�ƿ�, ��ũ�Ѹ� �ӵ�")]
    [SerializeField] float ZoomSpeed;
    [SerializeField] float MoveSpeed;

    RawImage MapImage;

    Vector3 mapPosition;

    Vector2 nowPos, prePos;
    Vector3 movePos;


    void Awake()
    {
        MapImage = GetComponent<RawImage>();

        mapPosition = new Vector3(0, 0, 0);
    }


    void Update()
    {
        TouchZoom();
        TouchMove();
    }

    void MapZoomInOut()
    {
        if (Input.GetKey(KeyCode.Q)) MapImage.transform.localScale *= 1f+(ZoomSpeed*Time.deltaTime);

        else if (Input.GetKey(KeyCode.E)) MapImage.transform.localScale /= 1f+(ZoomSpeed*Time.deltaTime);

        float MapScaleX = Mathf.Clamp(MapImage.transform.localScale.x, minSize, maxSize);    //������ Ȯ�� ������ ���̷� ����

        float MapScaleY = Mathf.Clamp(MapImage.transform.localScale.y, minSize, maxSize);    //������ Ȯ�� ������ ���̷� ����

        MapImage.transform.localScale = new Vector2(MapScaleX, MapScaleY);

    }



    void TouchZoom()
    {
        if (Input.touchCount == 2) //�հ��� 2���� ������ ��
        {
            Touch touchZero = Input.GetTouch(0); //ù��° �հ��� ��ġ�� ����
            Touch touchOne = Input.GetTouch(1); //�ι�° �հ��� ��ġ�� ����

            //��ġ�� ���� ���� ��ġ���� ���� ������
            //ó�� ��ġ�� ��ġ(touchZero.position)���� ���� �����ӿ����� ��ġ ��ġ�� �̹� �����ӿ��� ��ġ ��ġ�� ���̸� ��
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; //deltaPosition�� �̵����� ������ �� ���
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // �� �����ӿ��� ��ġ ������ ���� �Ÿ� ����
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //magnitude�� �� ������ �Ÿ� ��(����)
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // �Ÿ� ���� ����(�Ÿ��� �������� ũ��(���̳ʽ��� ������)�հ����� ���� ����_���� ����)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Vector3 mapScale = MapImage.transform.localScale;
            
            mapScale.x += -deltaMagnitudeDiff * ZoomSpeed * Time.deltaTime;
            mapScale.y += -deltaMagnitudeDiff * ZoomSpeed * Time.deltaTime;

            float MapScaleX = Mathf.Clamp(mapScale.x, minSize, maxSize);    //������ Ȯ�� ������ ���̷� ����

            float MapScaleY = Mathf.Clamp(mapScale.x, minSize, maxSize);    //������ Ȯ�� ������ ���̷� ����

            MapImage.transform.localScale = new Vector2(MapScaleX, MapScaleY);

            DontOutMAP();
        }
    }


    void MapMove()
    {
        mapPosition = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow)) mapPosition += Vector3.up;
        if (Input.GetKey(KeyCode.DownArrow)) mapPosition += Vector3.down;
        if (Input.GetKey(KeyCode.RightArrow)) mapPosition += Vector3.right;
        if (Input.GetKey(KeyCode.LeftArrow)) mapPosition += Vector3.left;
        


        transform.position += mapPosition * Time.deltaTime * MoveSpeed * MapImage.transform.localScale.x;


    }

    void TouchMove()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                prePos = touch.position - touch.deltaPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                nowPos = touch.position - touch.deltaPosition;
                movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * MoveSpeed * MapImage.transform.localScale.x;
                MapImage.transform.Translate(movePos);

                prePos = touch.position - touch.deltaPosition;
            }
            DontOutMAP();
        }


    }

    void DontOutMAP()
    {
        float x = (Screen.width / 2) * (transform.localScale.x - 1);
        float y = (Screen.height / 2) * (transform.localScale.y - 1);

        float MapPositionX = Mathf.Clamp(MapImage.rectTransform.anchoredPosition.x, -x, x);    //������ �̵� ����

        float MapPositionY = Mathf.Clamp(MapImage.rectTransform.anchoredPosition.y, -y, y);    //������ �̵� ����

        MapImage.rectTransform.anchoredPosition = new Vector2(MapPositionX, MapPositionY);

    }
}
