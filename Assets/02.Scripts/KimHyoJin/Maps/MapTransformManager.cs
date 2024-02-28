using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapTransformManager : MonoBehaviour
{
    [Header("지도의 최소, 최대 확대 비율")]
    [SerializeField] float minSize;
    [SerializeField] float maxSize;
    [Header("줌인,아웃, 스크롤링 속도")]
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

        float MapScaleX = Mathf.Clamp(MapImage.transform.localScale.x, minSize, maxSize);    //지도의 확대 레벨을 사이로 제한

        float MapScaleY = Mathf.Clamp(MapImage.transform.localScale.y, minSize, maxSize);    //지도의 확대 레벨을 사이로 제한

        MapImage.transform.localScale = new Vector2(MapScaleX, MapScaleY);

    }



    void TouchZoom()
    {
        if (Input.touchCount == 2) //손가락 2개가 눌렸을 때
        {
            Touch touchZero = Input.GetTouch(0); //첫번째 손가락 터치를 저장
            Touch touchOne = Input.GetTouch(1); //두번째 손가락 터치를 저장

            //터치에 대한 이전 위치값을 각각 저장함
            //처음 터치한 위치(touchZero.position)에서 이전 프레임에서의 터치 위치와 이번 프로임에서 터치 위치의 차이를 뺌
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; //deltaPosition는 이동방향 추적할 때 사용
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 각 프레임에서 터치 사이의 벡터 거리 구함
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //magnitude는 두 점간의 거리 비교(벡터)
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 거리 차이 구함(거리가 이전보다 크면(마이너스가 나오면)손가락을 벌린 상태_줌인 상태)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Vector3 mapScale = MapImage.transform.localScale;
            
            mapScale.x += -deltaMagnitudeDiff * ZoomSpeed * Time.deltaTime;
            mapScale.y += -deltaMagnitudeDiff * ZoomSpeed * Time.deltaTime;

            float MapScaleX = Mathf.Clamp(mapScale.x, minSize, maxSize);    //지도의 확대 레벨을 사이로 제한

            float MapScaleY = Mathf.Clamp(mapScale.x, minSize, maxSize);    //지도의 확대 레벨을 사이로 제한

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

        float MapPositionX = Mathf.Clamp(MapImage.rectTransform.anchoredPosition.x, -x, x);    //지도의 이동 제한

        float MapPositionY = Mathf.Clamp(MapImage.rectTransform.anchoredPosition.y, -y, y);    //지도의 이동 제한

        MapImage.rectTransform.anchoredPosition = new Vector2(MapPositionX, MapPositionY);

    }
}
