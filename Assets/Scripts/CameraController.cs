using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    
    [Header("Configurações")]
    [SerializeField] private float zoomInicial = 3.15f;
    [SerializeField] private float incrementoPorNivel = 0.5f;
    
    private CinemachineCamera vcam;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            vcam = GetComponent<CinemachineCamera>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Chamado quando compra um upgrade do chapéu
    public void UpdateVisibility(int nivelAtual)
    {
        float novoZoom = zoomInicial + (incrementoPorNivel * nivelAtual);
               
        vcam.Lens.OrthographicSize = novoZoom;
        Debug.Log($"Zoom atualizado: {vcam.Lens.OrthographicSize}");        
    }

    void ResetZoom()
    {
        if(vcam != null)
        {
            vcam.Lens.OrthographicSize = zoomInicial;
        }
    }
}