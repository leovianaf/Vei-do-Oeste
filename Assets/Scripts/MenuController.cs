using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPrincipal; // Painel do menu principal
    public GameObject popupControles; // Painel do pop-up de controles

    // Método para abrir o pop-up de controles
    public void AbrirControles()
    {
        menuPrincipal.SetActive(false); // Desativa o menu principal
        popupControles.SetActive(true); // Ativa o pop-up de controles
    }

    // Método para fechar o pop-up de controles
    public void FecharControles()
    {
        popupControles.SetActive(false); // Desativa o pop-up de controles
        menuPrincipal.SetActive(true); // Reativa o menu principal
    }
}
