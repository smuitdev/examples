using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultilangText : MonoBehaviour
{
    [SerializeField] private LocalizedString m_String;

    /// <summary>
    /// Ссылка на скрипт текста на интерфейсе пользователя.
    /// </summary>
    [SerializeField] private UnityEngine.UI.Text m_Text;

    private void Start()
    {
        // подписка на событие изменения языка
        LocalizedString.OnLanguageChanged += LocalizedString_OnLanguageChanged;
    }

    /// <summary>
    /// Отписка от оповещения при уничтожении скрипта.
    /// </summary>
    private void OnDestroy()
    {
        LocalizedString.OnLanguageChanged -= LocalizedString_OnLanguageChanged;
    }

    private void LocalizedString_OnLanguageChanged()
    {
        // установка многоязычной строки текста 
        m_Text.text = m_String.ToString();
    }
}
