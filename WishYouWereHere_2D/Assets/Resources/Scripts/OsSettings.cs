using UnityEngine;
using Michsky.DreamOS; // DremOS namespace

public class OsSettings : MonoBehaviour
{
    [SerializeField] private LocalizationManager locManager;

    void Start()
    {
        locManager.SetLanguage("kr-KR");
        LocalizationManager.SetLanguageWithoutNotify("kr-KR");
    }
}