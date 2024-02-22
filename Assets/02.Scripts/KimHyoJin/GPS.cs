using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class GPS : MonoBehaviour
{
    /// <summary>
    /// ����ڿ��� GPS ������ ��û�ϰ�, ��û ������ ���� ��ġ�� �޾ƿ� �� �ִ� ��ũ��Ʈ
    /// </summary>


    public LocationInfo locationInfo;

    public LocationService locationService;
    
    LocationServiceStatus status;



    public void Request(string permission = null)   //����ڿ��� GPS������ ��û�ϰ�, ������ GPS ��������� �����ϴ� ��ũ��Ʈ
    {
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))   startGPS();      //GPS ������ �����Ǿ��ִٸ� ��ġ���� ���� ����

        else
        {
            PermissionCallbacks callbacks = new PermissionCallbacks();  //���� ��û ���信 ���� �ݹ�
            callbacks.PermissionGranted += Request;    //GPS ���� ��û ������ request ���
            Permission.RequestUserPermission(Permission.FineLocation, callbacks);   //�ݹ��� �߰��Ͽ� GPS ���� ��û
        }

    }


    void startGPS() //GPS ���� ����
    {
        locationService = Input.location;
        locationService.Start();
    }

    void stopGPS()  //GPS ���� ����
    {
        locationService.Stop();
    }

    

    public bool GetMyLocation( ref float latitude, ref float longitude)     //������� ��ġ������ �޾ƿ� ���� �Ű������� ��ȯ�ϴ� ��ũ��Ʈ
    {
        if(!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) return false;  //GPS ���� ������ false

        if (!locationService.isEnabledByUser) return false; //GPS�� ���� ���� �� false

        status = locationService.status;    //��ġ���� �Է� ����

        switch (status)
        {
            case LocationServiceStatus.Failed:
            case LocationServiceStatus.Stopped:
            case LocationServiceStatus.Initializing:
                return false;
                //��ġ������ �޾ƿ��µ� �����ϰų�, �����ϰų�, �Է����̶�� false

            default:
                locationInfo = locationService.lastData;
                latitude = locationInfo.latitude;
                longitude = locationInfo.longitude;
                return true;
                //�׷��� �ʴٸ� ���������� �޾ƿ� ��ġ ������ ����, �浵�� ���� �Ű������� ������.
        }
    }


}
