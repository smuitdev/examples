using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Многоязычная строка, свой тип ассета.
/// </summary>
[CreateAssetMenu]
public class LocalizedString : ScriptableObject
{
    /// <summary>
    /// Значение текущего языка.
    /// </summary>
    private static LanguageType m_CurrentLanguage;

    /// <summary>
    /// Текущий язык в программе. Синглтон свойство.
    /// </summary>
    public static LanguageType currentLanguage
    {
        get
        {
            return m_CurrentLanguage;
        }

        set
        {
            m_CurrentLanguage = value;

            OnLanguageChanged();
        }
    }


    /// <summary>
    /// Событие изменения языка на которое надо подписываться другим
    /// скриптам. Инициализируется пустым массивом делегатов.
    /// </summary>
    public static event System.Action OnLanguageChanged = delegate { };

    /// <summary>
    /// Type of supported laguages.
    /// </summary>
    public enum LanguageType
    {
        English,
        Russian
    }

    /// <summary>
    /// Контейнер для хранения пар Язык-Значение строки в этом языке.
    /// </summary>
    [System.Serializable]
    public struct Value
    {
        /// <summary>
        /// Тип языка
        /// </summary>
        public LanguageType language;

        /// <summary>
        /// Значение строки в языке language
        /// </summary>
        [Multiline]
        public string stringValue;
    }

    /// <summary>
    /// Массив строковых значений по языкам.
    /// </summary>
    [SerializeField] private Value[] localizedValues;

    /// <summary>
    /// Возвращает значение многоязычной строки для текущего языка
    /// задаваемого при помощи свойства currentLaguage.
    /// 
    /// Задание: оптимизировать выдачу строки для текущего языка с перебора на что то
    /// константное по времени. O(1)
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        foreach(var element in localizedValues)
        {
            if (element.language == currentLanguage)
                return element.stringValue;
        }

        return DefaultString;
    }

    /// <summary>
    /// Значение многоязычной строки если не задано для текущего языка.
    /// </summary>
    public static readonly string DefaultString = "Undefined";
}
