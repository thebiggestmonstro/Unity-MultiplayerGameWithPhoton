using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;

public class UI_RespawnTimer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI spawnTime;

    private void Awake()
    {
        UIManager.ResgisterRespawnPanelUI(gameObject.name, this);
    }

    void OnEnable()
    {
        StartCoroutine(SpawnStarting());
    }

    IEnumerator SpawnStarting()
    {
        spawnTime.text = "3";
        yield return new WaitForSeconds(1);
        spawnTime.text = "2";
        yield return new WaitForSeconds(1);
        spawnTime.text = "1";
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
