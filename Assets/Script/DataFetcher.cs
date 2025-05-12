using System;
using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

// ��ü ���� ������
[System.Serializable]
public class WeatherData
{
    public Coord coord;
    public Weather[] weather;
    public string baseStation;
    public Main main;
    public int visibility;
    public Wind wind;
    public Clouds clouds;
    public int dt;
    public Sys sys;
    public int timezone;
    public int id;
    public string name;
    public int cod;
}

[System.Serializable]
public class Coord
{
    public double lon;
    public double lat;
}

[System.Serializable]
public class Weather
{
    public int id;
    public string main;
    public string description;
    public string icon;
}

[System.Serializable]
public class Main
{
    public float temp;
    public float feels_like;
    public float temp_min;
    public float temp_max;
    public int pressure;
    public int humidity;
    public int sea_level;
    public int grnd_level;
}

[System.Serializable]
public class Wind
{
    public float speed;
    public int deg;
}

[System.Serializable]
public class Clouds
{
    /*
    0~20: ���� �ϴ�(������ ���� ����)
    20~40: �ణ�� ����(������ �幮�幮 ����)
    40~60: �κ������� ����(������ �а� ����)
    60~80: ������ ����(�ϴ��� ��κ��� �������� ����)
    80~100: ���� ������ �������� ���� �ϴ�
    */
    public int all;
}

[System.Serializable]
public class Sys
{
    public int type;
    public int id;
    public string country;
    public int sunrise;
    public int sunset;
}

public class DataFetcher : MonoBehaviour
{
    // �⺻ url
private string mainUrl = "https://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid={1}";

// API Ű
[SerializeField] private string myKey = "";

// ���� �̸�
public static string city_name = "";

// UI ���� ��ü
[SerializeField] private UI_Manager ui_Manager;

    

    private DateTime currentDateTime;

    

    void Start()
    {
        StartCoroutine(FetchData());
    }


    IEnumerator FetchData()
    {
        string url = string.Format(mainUrl, city_name, myKey);

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            string jsonResponse = www.downloadHandler.text;
            WeatherData weatherData = JsonUtility.FromJson<WeatherData>(jsonResponse);

            // ���н� Ÿ�ӽ������� DateTime���� ��ȯ
            DateTime currentTime = UnixTimeStampToDateTime(weatherData.dt);

            // ���� �ð� ���
            //Debug.Log("���� �ð�: " + currentTime.ToString("yyyy-MM-dd HH:mm:ss"));


            ui_Manager.setData(weatherData.weather[0].main, weatherData.main.temp, weatherData.name);

        }
        else
        {
            Debug.Log("ERROR");
        }
    }

    // ���н� Ÿ�ӽ������� DateTime���� ��ȯ�ϴ� �Լ�
    private DateTime UnixTimeStampToDateTime(int unixTimeStamp)
    {
        // ���н� Ÿ�ӽ������� 1970�� 1�� 1�Ϻ����� �� ���̹Ƿ� �̸� ��ȯ
        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return unixStart.AddSeconds(unixTimeStamp).ToLocalTime();  // ���� �ð����� ��ȯ
    }

}
