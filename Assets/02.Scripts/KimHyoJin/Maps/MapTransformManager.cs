using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;





/// <summary>
/// 지도의 확대, 축소, 움직임을 담당하는 스크립트
/// </summary>
public class MapTransformManager : MonoBehaviour
{
    [Header("지도의 최소, 최대 확대 비율")]
    [SerializeField] float minSize;
    [SerializeField] float maxSize;
    [Header("줌인,아웃, 스크롤링 속도")]
    [SerializeField] float ZoomSpeed;
    [SerializeField] float MoveSpeed;

    [Header("지도")]
    RawImage MapImage;


    [Header("지도의 위치 계산용 벡터")]
    Vector3 mapPosition;

    [Header("터치 계산용 벡터")]
    Vector2 nowPos, prePos;
    Vector3 movePos;


    void Awake()
    {
        MapImage = GetComponent<RawImage>();

        mapPosition = Vector3.zero; //지도계산용 벡터 초기화
    }


    void Update()
    {
        TouchZoom();
        TouchMove();
    }

    void TouchZoom()    //확대, 축소를 담당하는 메서드
    {
        if (Input.touchCount == 2) //손가락 2개가 눌렸을 때
        {
            Touch touchZero = Input.GetTouch(0); //첫번째 손가락 터치를 저장
            Touch touchOne = Input.GetTouch(1); //두번째 손가락 터치를 저장

            //터치에 대한 이전 위치값을 각각 저장
            //처음 터치한 위치(touchZero.position)에서 이전 프레임에서의 터치 위치와 이번 프레임에서 터치 위치의 차이를 뺌
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 각 프레임에서 터치 사이의 벡터 거리 구함
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 거리 차이 구함(거리가 이전보다 크면(마이너스가 나오면)손가락을 벌린 상태_줌인 상태)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Vector3 mapScale = MapImage.transform.localScale;   //지도의 현재 스케일을 저장
            
            mapScale.x += -deltaMagnitudeDiff * ZoomSpeed * Time.deltaTime;
            mapScale.y += -deltaMagnitudeDiff * ZoomSpeed * Time.deltaTime;
            //지도의 스케일을 얼마나 바꿀 것인지 계산

            float MapScaleX = Mathf.Clamp(mapScale.x, minSize, maxSize);    
            float MapScaleY = Mathf.Clamp(mapScale.x, minSize, maxSize); 
            //지도의 확대 수준을 제한

            MapImage.transform.localScale = new Vector2(MapScaleX, MapScaleY);
            //지도 크기 변경 적용

            DontOutMAP();   //지도 크기가 바뀌면서 캔버스를 벗어나지 않도록 조절
        }
    }

    void TouchMove()    //지도의 움직임을 담당하는 메서드
    {
        if (Input.touchCount == 1)  //손가락 하나만 눌렀을 때
        {
            Touch touch = Input.GetTouch(0);    //터치 저장

            if (touch.phase == TouchPhase.Began)    //터치를 막 시작했을 떄
            {
                prePos = touch.position - touch.deltaPosition;  //터치의 이전 위치값 저장
            }
            else if (touch.phase == TouchPhase.Moved)   //터치가 움직일 때
            {
                nowPos = touch.position - touch.deltaPosition;  //터치의 현재 위치값을 저장
                movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * MoveSpeed * MapImage.transform.localScale.x;
                //얼마나 움직였는지를 이전위치-현재위치로 계산, 이후 움직임 속도와 지도의 스케일만큼 보정
                MapImage.transform.Translate(movePos);  //지도를 움직인다.

                prePos = touch.position - touch.deltaPosition;  //터치의 이전 위치값 저장
            }
            DontOutMAP();   //지도 크기가 바뀌면서 캔버스를 벗어나지 않도록 조절
        }


    }

    void DontOutMAP()   //지도가 캔버스 밖을 벗어나지 않도록 제한하는 메서드
    {
        float x = (Screen.width / 2) * (transform.localScale.x - 1);
        float y = (Screen.height / 2) * (transform.localScale.y - 1);
        // 화면의 가로, 세로값의 절반에 지도의 스케일 값-1만큼 곱
        // 지도가 담긴 캔버스의 앵커가 각 꼭지점에 있고, 지도의 피벗은 정중앙이다.

        float MapPositionX = Mathf.Clamp(MapImage.rectTransform.anchoredPosition.x, -x, x);    //지도의 이동 제한

        float MapPositionY = Mathf.Clamp(MapImage.rectTransform.anchoredPosition.y, -y, y);    //지도의 이동 제한

        MapImage.rectTransform.anchoredPosition = new Vector2(MapPositionX, MapPositionY);  //지도의 앵커드 포지션을 설정

    }
}
