using UnityEngine;

public class Dilemma : MonoBehaviour
{
    [SerializeField] DilemmaCar dilemmaCar;

    public void OnTrackShown()
    {
        dilemmaCar.UpdateSquirmerAudio();
    }
}
