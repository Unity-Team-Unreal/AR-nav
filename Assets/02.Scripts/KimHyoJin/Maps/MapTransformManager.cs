using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;





/// <summary>
/// ������ Ȯ��, ���, �������� ����ϴ� ��ũ��Ʈ
/// </summary>
public class MapTransformManager : MonoBehaviour
{
    [Header("������ �ּ�, �ִ� Ȯ�� ����")]
    [SerializeField] float minSize;
    [SerializeField] float maxSize;
    [Header("����,�ƿ�, ��ũ�Ѹ� �ӵ�")]
    [SerializeField] float ZoomSpeed;
    [SerializeField] float MoveSpeed;

    [Header("����")]
    RawImage MapImage;


    [Header("������ ��ġ ���� ����")]
    Vector3 mapPosition;

    [Header("��ġ ���� ����")]
    Vector2 nowPos, prePos;
    Vector3 movePos;


    void Awake()
    {
        MapImage = GetComponent<RawImage>();

        mapPosition = Vector3.zero; //�������� ���� �ʱ�ȭ
    }


    void Update()
    {
        TouchZoom();
        TouchMove();
    }

    void TouchZoom()    //Ȯ��, ��Ҹ� ����ϴ� �޼���
    {
        if (Input.touchCount == 2) //�հ��� 2���� ������ ��
        {
            Touch touchZero = Input.GetTouch(0); //ù��° �հ��� ��ġ�� ����
            Touch touchOne = Input.GetTouch(1); //�ι�° �հ��� ��ġ�� ����

            //��ġ�� ���� ���� ��ġ���� ���� ����
            //ó�� ��ġ�� ��ġ(touchZero.position)���� ���� �����ӿ����� ��ġ ��ġ�� �̹� �����ӿ��� ��ġ ��ġ�� ���̸� ��
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // �� �����ӿ��� ��ġ ������ ���� �Ÿ� ����
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // �Ÿ� ���� ����(�Ÿ��� �������� ũ��(���̳ʽ��� ������)�հ����� ���� ����_���� ����)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Vector3 mapScale = MapImage.transform.localScale;   //������ ���� �������� ����
            
            mapScale.x += -deltaMagnitudeDiff * ZoomSpeed * Time.deltaTime;
            mapScale.y += -deltaMagnitudeDiff * ZoomSpeed * Time.deltaTime;
            //������ �������� �󸶳� �ٲ� ������ ���

            float MapScaleX = Mathf.Clamp(mapScale.x, minSize, maxSize);    
            float MapScaleY = Mathf.Clamp(mapScale.x, minSize, maxSize); 
            //������ Ȯ�� ������ ����

            MapImage.transform.localScale = new Vector2(MapScaleX, MapScaleY);
            //���� ũ�� ���� ����

            DontOutMAP();   //���� ũ�Ⱑ �ٲ�鼭 ĵ������ ����� �ʵ��� ����
        }
    }

    void TouchMove()    //������ �������� ����ϴ� �޼���
    {
        if (Input.touchCount == 1)  //�հ��� �ϳ��� ������ ��
        {
            Touch touch = Input.GetTouch(0);    //��ġ ����

            if (touch.phase == TouchPhase.Began)    //��ġ�� �� �������� ��
            {
                prePos = touch.position - touch.deltaPosition;  //��ġ�� ���� ��ġ�� ����
            }
            else if (touch.phase == TouchPhase.Moved)   //��ġ�� ������ ��
            {
                nowPos = touch.position - touch.deltaPosition;  //��ġ�� ���� ��ġ���� ����
                movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * MoveSpeed * MapImage.transform.localScale.x;
                //�󸶳� ������������ ������ġ-������ġ�� ���, ���� ������ �ӵ��� ������ �����ϸ�ŭ ����
                MapImage.transform.Translate(movePos);  //������ �����δ�.

                prePos = touch.position - touch.deltaPosition;  //��ġ�� ���� ��ġ�� ����
            }
            DontOutMAP();   //���� ũ�Ⱑ �ٲ�鼭 ĵ������ ����� �ʵ��� ����
        }


    }

    void DontOutMAP()   //������ ĵ���� ���� ����� �ʵ��� �����ϴ� �޼���
    {
        float x = (Screen.width / 2) * (transform.localScale.x - 1);
        float y = (Screen.height / 2) * (transform.localScale.y - 1);
        // ȭ���� ����, ���ΰ��� ���ݿ� ������ ������ ��-1��ŭ ��
        // ������ ��� ĵ������ ��Ŀ�� �� �������� �ְ�, ������ �ǹ��� ���߾��̴�.

        float MapPositionX = Mathf.Clamp(MapImage.rectTransform.anchoredPosition.x, -x, x);    //������ �̵� ����

        float MapPositionY = Mathf.Clamp(MapImage.rectTransform.anchoredPosition.y, -y, y);    //������ �̵� ����

        MapImage.rectTransform.anchoredPosition = new Vector2(MapPositionX, MapPositionY);  //������ ��Ŀ�� �������� ����

    }
}
