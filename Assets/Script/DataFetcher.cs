using System;
using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

// 전체 날씨 데이터
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
    0~20: 맑은 하늘(구름이 거의 없음)
    20~40: 약간의 구름(구름이 드문드문 있음)
    40~60: 부분적으로 구름(구름이 넓게 퍼짐)
    60~80: 구름이 많음(하늘의 대부분이 구름으로 덮힘)
    80~100: 거의 완전히 구름으로 덮인 하늘
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
    // 기본 url
private string mainUrl = "https://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid={1}";

// API 키
[SerializeField] private string myKey = "";

// 도시 이름
public static string city_name = "";

// UI 관리 객체
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

            // 유닉스 타임스탬프를 DateTime으로 변환
            DateTime currentTime = UnixTimeStampToDateTime(weatherData.dt);

            // 현재 시간 출력
            //Debug.Log("현재 시간: " + currentTime.ToString("yyyy-MM-dd HH:mm:ss"));


            ui_Manager.setData(weatherData.weather[0].main, weatherData.main.temp, weatherData.name);

        }
        else
        {
            Debug.Log("ERROR");
        }
    }

    // 유닉스 타임스탬프를 DateTime으로 변환하는 함수
    private DateTime UnixTimeStampToDateTime(int unixTimeStamp)
    {
        // 유닉스 타임스탬프는 1970년 1월 1일부터의 초 수이므로 이를 변환
        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return unixStart.AddSeconds(unixTimeStamp).ToLocalTime();  // 로컬 시간으로 변환
    }

}
