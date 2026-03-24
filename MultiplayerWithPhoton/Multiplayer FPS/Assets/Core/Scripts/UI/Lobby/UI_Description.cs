using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Description : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] 
    private GameObject dropdown;

    public void OnPointerEnter(PointerEventData eventData)
    {
        dropdown.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        dropdown.SetActive(false);
    }

    void Start()
    {
        dropdown.SetActive(false);
    }
}
