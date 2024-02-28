using Google.XR.ARCoreExtensions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    void AnchorSpawn()
    {
        poseGPS.Latitude = latitude;
        poseGPS.Longitude = longitude;

        posi = earthmanager.Convert(poseGPS);
        posi.position.y = 0;

        Instantiate(anchorPrefab, posi.position, posi.rotation);
        line.positionCount = POI.datalist.Count;
                
    }
    
    void CreateLine() 
    {
        pathObjs = new Vector3[ARDirectionsManager.paths.Length + 1];
        pathObjs[0] = new Vector3(0, -1.5f, 0);
        for (int i = 0; i < pathObjs.Length - 1; i++)
        {
            LineRenderer lineRendererObject = Instantiate(line);
            line.SetPosition(0, pathObjs[i]);
            line.SetPosition(1, pathObjs[i + 1]);
            lineRendererObject.transform.eulerAngles = new Vector3(90f, 0, 0);
        }
    } 
}
