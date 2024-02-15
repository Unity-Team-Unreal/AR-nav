using System;

// POI ������ ������ ���� �����̳�
[Serializable]
public struct POIData
{
    public string name;
    public string description;
    public float latitude;
    public float longitude;
    public float altitude;
    public string guide;

    public POIData(string name, string description, float latitude, float longtitude, float altitude, string guide)
    {
        this.name = name;
        this.description = description;
        this.latitude = latitude;
        this.longitude = longtitude;
        this.altitude = altitude;
        this.guide = guide;
    }
}
