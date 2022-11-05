using UnityEngine;
public class CanvasEnabler : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    private void Awake()
    {
        _canvas.SetActive(true);
    }
}