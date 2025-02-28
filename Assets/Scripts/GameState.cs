using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameState
{
    public static Vector3 lastPlayerPosition;
    public static bool shouldRestorePosition = false;
    public static int diaryDialogueIndex = 0;
    public static int maxDiaryDialogues = 6;
    public static string previousScene;
    public static bool hasOpenedDiary = false;
}
