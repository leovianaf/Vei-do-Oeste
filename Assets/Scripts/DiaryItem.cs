// using UnityEngine;

// public class DiaryItem : MonoBehaviour
// {
//     private bool isPlayerNearby = false;

//     void Update()
//     {
//         if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
//         {
//             StartCutscene();
//         }
//     }

//     private void StartCutscene()
//     {
//         FindObjectOfType<CutsceneManager>()?.PlayCutscene();

//         FindObjectOfType<PlayerController>().enabled = false;
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             isPlayerNearby = true;
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             isPlayerNearby = false;
//         }
//     }
// }
