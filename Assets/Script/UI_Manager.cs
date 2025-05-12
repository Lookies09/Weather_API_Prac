using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text mainWeatherTxt;
    [SerializeField] private Text tempTxt;
    [SerializeField] private Text cityTxt;
    [SerializeField] private Text timeTxt;
    
    // ��� ȭ���
    [SerializeField] private GameObject earlyMorningBackground; // �̸� ��ħ ���
    [SerializeField] private GameObject morningBackground;   // ��ħ ���       
    [SerializeField] private GameObject afternoonBackground; // ���� ���
    [SerializeField] private GameObject sunsetBackground; // ���� ���
    [SerializeField] private GameObject nightBackground; // �� ���
    [SerializeField] private GameObject dawnBackground;  // ���� ���

    // ���� ��ƼŬ
    [SerializeField] private GameObject rainParticle;
    [SerializeField] private GameObject lightRainParticle;
    [SerializeField] private GameObject snowParticle;

    public void Awake()
    {
        UpdateBackground(DateTime.Now);
    }
    private void Start()
    {
        // ���ʸ��� �ð��� �����ϴ� ���
        InvokeRepeating("UpdateTime", 0f, 1f); // 0�� �� ����, ���� 1�ʸ��� ȣ��
    }

    void UpdateTime()
    {
        // ���� �ð��� "HH:mm:ss" �������� ������
        string currentTime = System.DateTime.Now.ToString("HH:mm:ss");
        timeTxt.text = currentTime;  // UI �ؽ�Ʈ�� ���� �ð� ǥ��
    }

    public void setData(string mainWeater, float temp, string city)
    {
        // ���� �ؽ�Ʈ ó��
        if (mainWeater == "Clear")
        {
            mainWeater = "���� �ϴ�";
        }
        else if (mainWeater == "Clouds")
        {
            mainWeater = "����";
        }
        else if (mainWeater == "Rain")
        {
            mainWeater = "��";
            rainParticle.SetActive(true);
            rainParticle.GetComponent<ParticleSystem>().Play();
        }
        else if (mainWeater == "Drizzle")
        {
            mainWeater = "������ ��";
            lightRainParticle.SetActive(true);
            lightRainParticle.GetComponent<ParticleSystem>().Play();
        }
        else if (mainWeater == "Thunderstorm")
        {
            mainWeater = "õ�հ� ����";
        }
        else if (mainWeater == "Snow")
        {
            mainWeater = "��";
            snowParticle.SetActive(true);
            snowParticle.GetComponent<ParticleSystem>().Play();
        }
        else if (mainWeater == "Mist")
        {
            mainWeater = "�Ȱ�";
        }
        else
        {
            mainWeater = "�� �� ���� ���� ����";
        }

        mainWeatherTxt.text = mainWeater;
        tempTxt.text = $"{temp.ToString()}��";
        cityTxt.text = city.ToString();

        
    }

    // ��� ������Ʈ
    private void UpdateBackground(DateTime currentTime)
    {
        // �� ����� ��Ȱ��ȭ
        afternoonBackground.SetActive(false);
        dawnBackground.SetActive(false);
        nightBackground.SetActive(false);
        earlyMorningBackground.SetActive(false);
        sunsetBackground.SetActive(false);
        morningBackground.SetActive(false);

        // �ð��� �������� ��� ����
        int hour = currentTime.Hour;
        int minute = currentTime.Minute;


        if (hour >= 12 && hour < 18)
        {
            // �� ��� (12:00 ~ 18:00)
            afternoonBackground.SetActive(true);
        }
        else if (hour >= 7 && hour < 9)
        {
            // �̸� ��ħ ��� (07:00 ~ 8:00)
            earlyMorningBackground.SetActive(true);
        }
        else if (hour >= 9 && hour < 12)
        {
            morningBackground.SetActive(true);
        }
        else if (hour >= 5 && hour < 7)
        {
            // ���� ��� (05:00 ~ 07:00)
            dawnBackground.SetActive(true);
        }
        else if (hour >= 18 && hour < 20)
        {
            // ���� ��� (18:00 ~ 20:00)
            sunsetBackground.SetActive(true);
        }
        else
        {
            // �� ��� (20:00 ~ 05:00)
            nightBackground.SetActive(true);

            mainWeatherTxt.color = Color.white;
            tempTxt.color = Color.white;
            cityTxt.color = Color.white;
            timeTxt.color = Color.white;
        }
    }

}
