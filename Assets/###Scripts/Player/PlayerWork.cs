using UnityEngine;

public class PlayerWork : MonoBehaviour
{
    [SerializeField] private GameObject _log;

    public void ShowLog()
    {
        _log.gameObject.SetActive(true);
    }

    public void HideLog()
    {
        _log.gameObject.SetActive(false);

    }
}
