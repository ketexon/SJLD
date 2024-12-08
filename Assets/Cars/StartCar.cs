using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(StartCar))]
[CanEditMultipleObjects]
public class StartCarEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new();

        VisualElement defaultInspector = new();
        InspectorElement.FillDefaultInspector(defaultInspector, serializedObject, this);

        root.Add(defaultInspector);

        Label timesPlayedLabel = new($"Times Played: {PlayerPrefs.GetInt("TimesPlayed")}");
        Button resetTimesPlayedButton = new(() =>
        {
            PlayerPrefs.SetInt("TimesPlayed", 0);
            timesPlayedLabel.MarkDirtyRepaint();
        });
        resetTimesPlayedButton.Add(new Label("Reset Times Played"));

        root.Add(timesPlayedLabel);
        root.Add(resetTimesPlayedButton);

        return root;
    }
}

#endif

public class StartCar : Car
{
    [SerializeField] GoArrow goArrow;
    [SerializeField] int easterEggMinTimesPlayed = 4;
    [SerializeField] float easterEggChance = 0.02f;

    public override void OnEnable()
    {
        base.OnEnable();

        goArrow.Button.onClick.AddListener(OnGoArrowPress);
        var timesPlayed = PlayerPrefs.GetInt("TimesPlayed", 0);
        if (timesPlayed >= easterEggMinTimesPlayed && Random.value < easterEggChance)
        {
            GameManager.Instance.CamusEnding = true;
            goArrow.PreventDefault = true;
        }
        MusicManager.Instance.PlayingFast = true;
    }

    void OnGoArrowPress()
    {
        var timesPlayed = PlayerPrefs.GetInt("TimesPlayed", 0);
        PlayerPrefs.SetInt("TimesPlayed", timesPlayed + 1);

        if(GameManager.Instance.CamusEnding)
        {
            PlayerController.Instance.Health = 0;
        }
    }
}
