using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GameState")]
public class GameState : ScriptableObject
{
    public int currentState;
    /*
    0 = regular
    1 = ghost
    2 = steam
    3 = cat
    */

    public VoidEvent OnGameStateChanged;
    public VoidEvent OnGhostMaskReleased;
    public VoidEvent OnSteamMaskReleased;
    public VoidEvent OnCatMaskReleased;

    public void UnlockNextState()
    {
        if (currentState >= 3)
        {
            currentState = 3;
            return;
        }
        currentState++;
        OnGameStateChanged.Raise(); 
        switch (currentState)
        {
            case 1:
                OnGhostMaskReleased.Raise(); 
                break;
            case 2:
                OnSteamMaskReleased.Raise(); 
                break;
            case 3:
                OnCatMaskReleased.Raise();
                break;
        }
    }
}
