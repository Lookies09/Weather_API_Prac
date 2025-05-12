using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text mainWeatherTxt;
    [SerializeField] private Text tempTxt;
    [SerializeField] private Text cityTxt;
    [SerializeField] private Text timeTxt;
    
    // 배경 화면들
    [SerializeField] private GameObject earlyMorningBackground; // 이른 아침 배경
    [SerializeField] private GameObject morningBackground;   // 아침 배경       
    [SerializeField] private GameObject afternoonBackground; // 오후 배경
    [SerializeField] private GameObject sunsetBackground; // 노을 배경
    [SerializeField] private GameObject nightBackground; // 밤 배경
    [SerializeField] private GameObject dawnBackground;  // 새벽 배경

    // 날씨 파티클
    [SerializeField] private GameObject rainParticle;
    [SerializeField] private GameObject lightRainParticle;
    [SerializeField] private GameObject snowParticle;

    public void Awake()
    {
        UpdateBackground(DateTime.Now);
    }
    private void Start()
    {
        // 매초마다 시간을 갱신하는 방법
        InvokeRepeating("UpdateTime", 0f, 1f); // 0초 후 시작, 이후 1초마다 호출
    }

    void UpdateTime()
    {
        // 현재 시간을 "HH:mm:ss" 포맷으로 가져옴
        string currentTime = System.DateTime.Now.ToString("HH:mm:ss");
        timeTxt.text = currentTime;  // UI 텍스트에 현재 시간 표시
    }

    public void setData(string mainWeater, float temp, string city)
    {
        // 날씨 텍스트 처리
        if (mainWeater == "Clear")
        {
            mainWeater = "맑은 하늘";
        }
        else if (mainWeater == "Clouds")
        {
            mainWeater = "구름";
        }
        else if (mainWeater == "Rain")
        {
            mainWeater = "비";
            rainParticle.SetActive(true);
            rainParticle.GetComponent<ParticleSystem>().Play();
        }
        else if (mainWeater == "Drizzle")
        {
            mainWeater = "가벼운 비";
            lightRainParticle.SetActive(true);
            lightRainParticle.GetComponent<ParticleSystem>().Play();
        }
        else if (mainWeater == "Thunderstorm")
        {
            mainWeater = "천둥과 번개";
        }
        else if (mainWeater == "Snow")
        {
            mainWeater = "눈";
            snowParticle.SetActive(true);
            snowParticle.GetComponent<ParticleSystem>().Play();
        }
        else if (mainWeater == "Mist")
        {
            mainWeater = "안개";
        }
        else
        {
            mainWeater = "알 수 없는 날씨 상태";
        }

        mainWeatherTxt.text = mainWeater;
        tempTxt.text = $"{temp.ToString()}℃";
        cityTxt.text = city.ToString();

        
    }

    // 배경 업데이트
    private void UpdateBackground(DateTime currentTime)
    {
        // 각 배경을 비활성화
        afternoonBackground.SetActive(false);
        dawnBackground.SetActive(false);
        nightBackground.SetActive(false);
        earlyMorningBackground.SetActive(false);
        sunsetBackground.SetActive(false);
        morningBackground.SetActive(false);

        // 시간을 기준으로 배경 변경
        int hour = currentTime.Hour;
        int minute = currentTime.Minute;


        if (hour >= 12 && hour < 18)
        {
            // 낮 배경 (12:00 ~ 18:00)
            afternoonBackground.SetActive(true);
        }
        else if (hour >= 7 && hour < 9)
        {
            // 이른 아침 배경 (07:00 ~ 8:00)
            earlyMorningBackground.SetActive(true);
        }
        else if (hour >= 9 && hour < 12)
        {
            morningBackground.SetActive(true);
        }
        else if (hour >= 5 && hour < 7)
        {
            // 새벽 배경 (05:00 ~ 07:00)
            dawnBackground.SetActive(true);
        }
        else if (hour >= 18 && hour < 20)
        {
            // 노을 배경 (18:00 ~ 20:00)
            sunsetBackground.SetActive(true);
        }
        else
        {
            // 밤 배경 (20:00 ~ 05:00)
            nightBackground.SetActive(true);

            mainWeatherTxt.color = Color.white;
            tempTxt.color = Color.white;
            cityTxt.color = Color.white;
            timeTxt.color = Color.white;
        }
    }

}
