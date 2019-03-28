using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Это тестовый скрипт для проверки работы LocalizedString ассета.
/// </summary>
public class LocalizationTest : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            LocalizedString.currentLanguage = LocalizedString.LanguageType.English;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LocalizedString.currentLanguage = LocalizedString.LanguageType.Russian;
        }
    }

    

    /*
    /// <summary>
    /// Поле с многоязычной строкой
    /// </summary>
    [SerializeField] private LocalizedString m_String;
    
    /// <summary>
    /// Начальный язык в приложении.
    /// </summary>
    [SerializeField] LocalizedString.LanguageType m_InitialLanguage;

    private void Start()
    {
        // установка языка
        LocalizedString.currentLanguage = m_InitialLanguage;

        // проверка возвращаемого значения в консоль.
        Debug.Log(m_String.ToString());
    }
    */
}
