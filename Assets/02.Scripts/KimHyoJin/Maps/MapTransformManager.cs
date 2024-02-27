using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapTransformManager : MonoBehaviour
{
    [SerializeField] float minSize;
    [SerializeField] float maxSize;
    [SerializeField] float ZoomSpeed;
    [SerializeField] float MoveSpeed;

    RawImage MapImage;

    Vector3 mapPosition;

    Touch touch;
    Vector3 touchedPos;
    void Start()
    {
        MapImage = GetComponent<RawImage>();

        mapPosition = new Vector3(0, 0, 0);
    }


    void Update()
    {
        MapZoomInOut();
        MapMove();
    }

    void MapZoomInOut()
    {
        if (Input.GetKey(KeyCode.Q)) MapImage.transform.localScale *= 1f+(ZoomSpeed*Time.deltaTime);

        else if (Input.GetKey(KeyCode.E)) MapImage.transform.localScale /= 1f+(ZoomSpeed*Time.deltaTime);

        float MapScaleX = Mathf.Clamp(MapImage.transform.localScale.x, minSize, maxSize);    //지도의 확대 레벨을 사이로 제한

        float MapScaleY = Mathf.Clamp(MapImage.transform.localScale.y, minSize, maxSize);    //지도의 확대 레벨을 사이로 제한

        MapImage.transform.localScale = new Vector2(MapScaleX, MapScaleY);

    }



    void TouchMoveTest()
    {
        if(Input.touchCount>0)
            for(int i = 0; i < Input.touchCount; i++)
            {
                Touch tempTouchs = Input.GetTouch(i);
                if(tempTouchs.phase == TouchPhase.Began)
                {
                    touchedPos = Camera.main.ScreenToWorldPoint(tempTouchs.position);
                }
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

        float x = (Screen.width / 2) * (transform.localScale.x-1);
        float y = (Screen.height / 2) * (transform.localScale.y - 1);

        float MapPositionX = Mathf.Clamp(MapImage.rectTransform.anchoredPosition.x, -x, x);    //지도의 확대 레벨을 사이로 제한

        float MapPositionY = Mathf.Clamp(MapImage.rectTransform.anchoredPosition.y, -y, y);    //지도의 확대 레벨을 사이로 제한

        MapImage.rectTransform.anchoredPosition = new Vector2(MapPositionX, MapPositionY);

    }
}
