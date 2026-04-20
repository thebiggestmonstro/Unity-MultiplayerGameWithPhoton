using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_NickName : MonoBehaviour
{
    public TextMeshProUGUI[] names;
    public Image[] healthbars;

    private void Awake()
    {
        UIManager.RegisterNickNameUI(gameObject.name, this);
    }

    private void Start()
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i].gameObject.SetActive(false);
            healthbars[i].gameObject.SetActive(false);
        }
    }
}
