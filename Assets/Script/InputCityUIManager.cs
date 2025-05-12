using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputCityUIManager : MonoBehaviour
{
    [SerializeField] private Dropdown cityDropdown;


    public void OnClickCheckButton()
    {
        DataFetcher.city_name = cityDropdown.options[cityDropdown.value].text;
        SceneManager.LoadScene("WeatherScene");
    }
}
