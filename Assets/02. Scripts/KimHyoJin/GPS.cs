using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class GPS : MonoBehaviour
{
    public LocationInfo locationInfo;

    public LocationService locationService;
    
    LocationServiceStatus status;



    public void Request(string permission = null)
    {
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation)) startGPS();
        else
        {
            PermissionCallbacks callbacks = new PermissionCallbacks();
            callbacks.PermissionGranted += Request;
            Permission.RequestUserPermission(Permission.FineLocation, callbacks);
        }


    }


    void startGPS()
    {
        locationService = Input.location;
        locationService.Start();
    }

    void stopGPS()
    {
        locationService.Stop();
    }

    

    public bool GetMyLocation( ref float latitude, ref float longitude)
    {
        if(!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) return false;

        if (!locationService.isEnabledByUser) return false;

        status = locationService.status;

        switch (status)
        {
            case LocationServiceStatus.Failed:
            case LocationServiceStatus.Stopped:
            case LocationServiceStatus.Initializing:
                return false;

            default:
                locationInfo = locationService.lastData;
                latitude = locationInfo.latitude;
                longitude = locationInfo.longitude;
                return true;
        }
    }


}
