using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class GPS : MonoBehaviour
{
    /// <summary>
    /// 사용자에게 GPS 권한을 요청하고, 요청 수락시 현재 위치를 받아올 수 있는 스크립트
    /// </summary>


    public LocationInfo locationInfo;

    public LocationService locationService;
    
    LocationServiceStatus status;



    public void Request(string permission = null)   //사용자에게 GPS권한을 요청하고, 수락시 GPS 추적기능을 시작하는 스크립트
    {
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))   startGPS();      //GPS 권한이 수락되어있다면 위치정보 추적 시작

        else
        {
            PermissionCallbacks callbacks = new PermissionCallbacks();  //권한 요청 응답에 따른 콜백
            callbacks.PermissionGranted += Request;    //GPS 권한 요청 수락시 request 재귀
            Permission.RequestUserPermission(Permission.FineLocation, callbacks);   //콜백을 추가하여 GPS 권한 요청
        }

    }


    void startGPS() //GPS 추적 시작
    {
        locationService = Input.location;
        locationService.Start();
    }

    void stopGPS()  //GPS 추적 중지
    {
        locationService.Stop();
    }

    

    public bool GetMyLocation( ref float latitude, ref float longitude)     //사용자의 위치정보를 받아와 참조 매개변수에 반환하는 스크립트
    {
        if(!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) return false;  //GPS 권한 거절시 false

        if (!locationService.isEnabledByUser) return false; //GPS가 꺼져 있을 시 false

        status = locationService.status;    //위치정보 입력 상태

        switch (status)
        {
            case LocationServiceStatus.Failed:
            case LocationServiceStatus.Stopped:
            case LocationServiceStatus.Initializing:
                return false;
                //위치정보를 받아오는데 실패하거나, 중지하거나, 입력중이라면 false

            default:
                locationInfo = locationService.lastData;
                latitude = locationInfo.latitude;
                longitude = locationInfo.longitude;
                return true;
                //그렇지 않다면 마지막으로 받아온 위치 정보의 위도, 경도를 참조 매개변수에 보낸다.
        }
    }


}
