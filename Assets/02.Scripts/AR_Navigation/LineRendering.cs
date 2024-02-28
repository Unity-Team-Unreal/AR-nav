using Google.XR.ARCoreExtensions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Anchor와 LineRenderer 생성 (미완성)
/// </summary>
public class LineRendering : MonoBehaviour
{
    [SerializeField] AREarthManager earthmanager;
    GeospatialPose poseGPS = new GeospatialPose();
    Pose posi;
    Vector3[] pathObjs;

    float latitude;
    float longitude;

    [SerializeField] GameObject anchorPrefab;

    [SerializeField] LineRenderer line;

    void Start()
    {
        AnchorSpawn();
        CreateLine();
    }

    void AnchorSpawn() // Anchor 생성
    {
        poseGPS.Latitude = latitude;
        poseGPS.Longitude = longitude;

        posi = earthmanager.Convert(poseGPS); // 지구상 좌표값을 유니티상 좌표값으로 변경
        posi.position.y = 0;

        Instantiate(anchorPrefab, posi.position, posi.rotation);
        line.positionCount = POI.datalist.Count;
                
    }
    
    void CreateLine() // LineRenderer 생성
    {
        pathObjs = new Vector3[ARDirectionsManager.paths.Length + 1]; // POI로 받아온 위도와 경도를 합친 좌표를 가져옴
        pathObjs[0] = new Vector3(0, -1.5f, 0); // 첫 번째 길은 현재 좌표 조금 아래에서 나오도록 설정
        for (int i = 0; i < pathObjs.Length - 1; i++)
        {
            LineRenderer lineRendererObject = Instantiate(line);
            line.SetPosition(0, pathObjs[i]);
            line.SetPosition(1, pathObjs[i + 1]);
            lineRendererObject.transform.eulerAngles = new Vector3(90f, 0, 0);
        }
    } 
}
