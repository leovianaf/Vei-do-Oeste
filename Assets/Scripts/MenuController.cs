using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPrincipal; // Painel do menu principal
    public GameObject popupControles; // Painel do pop-up de controles
    public GameObject creditos; // Painel do pop-up de creditos

    // Método para abrir o pop-up de controles
    public void AbrirControles()
    {
        menuPrincipal.SetActive(false); // Desativa o menu principal
        creditos.SetActive(false); // Desativa os creditos
        popupControles.SetActive(true); // Ativa o pop-up de controles
    }

    // Método para fechar o pop-up de controles
    public void Fechar()
    {
        popupControles.SetActive(false); // Desativa o pop-up de controles
        creditos.SetActive(false); // Desativa os creditos
        menuPrincipal.SetActive(true); // Reativa o menu principal
    }

    public void AbrirCreditos()
    {
        popupControles.SetActive(false); // Desativa o pop-up de controles
        creditos.SetActive(true); // Ativa os creditos
        menuPrincipal.SetActive(false); // Desativa o menu principal
    }
}
