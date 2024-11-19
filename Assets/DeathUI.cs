using UnityEngine;
using Kutie;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DeathUI : SingletonMonoBehaviour<DeathUI>
{
    [SerializeField] Canvas canvas;
    [SerializeField] Animator animator;
    [SerializeField] TMPro.TMP_Text scoreText;
    [SerializeField] string scoreTemplateString =
        "You solved <color=#4464D1>{0}</color> dilemma{1} and killed <color=#B73147>{2}</color> axolotl{3} out of {4}, " +
        "with {5} of those being preventable. {6}";

    [SerializeField] string percentKilledTemplate = "Of the dilemmas you solved, <color={2}>{0}%</color> involved violence. {1}";
    [SerializeField] List<float> thresholds;
    [SerializeField] List<string> messages;
    [SerializeField] string noAxolotlsMessage;
    [SerializeField] string noDilemmasMessages;
    [SerializeField] string camusEndingText = "There is but one truly serious philosophical problem and that is suicide. And that problem has just been answered... with a trolley.";

    public void Show()
    {
        canvas.enabled = true;
        animator.SetTrigger("Show");

        if (GameManager.Instance.CamusEnding)
        {
            scoreText.text = camusEndingText;
        }
        else
        {
            var score = GameManager.Instance.Score;
            var axolotlsKilled = GameManager.Instance.AxolotlsKilled;
            var axolotlsTotal = axolotlsKilled + GameManager.Instance.AxolotlsSaved;
            var axolotlsPreventablyKilled = GameManager.Instance.AxolotlsDeltaKilled;
            string message;
            if (axolotlsTotal > 0)
            {
                var badDecisions = GameManager.Instance.BadDecisions;
                var totalDecisions = badDecisions + GameManager.Instance.GoodDecisions;
                if (totalDecisions > 0)
                {
                    var percent = (float)(badDecisions) / totalDecisions;
                    string fate = "";
                    for (int i = 0; i < thresholds.Count; ++i)
                    {
                        if (percent <= thresholds[i])
                        {
                            fate = messages[i];
                            break;
                        }
                    }
                    message = string.Format(
                        percentKilledTemplate,
                        (int)(percent * 100),
                        fate,
                        percent < 0.45f
                            ? "#4464D1"
                            : percent > 0.55f
                            ? "#B73147"
                            : "white"
                    );
                }
                else
                {
                    message = noDilemmasMessages;
                }
            }
            else
            {
                message = noAxolotlsMessage;
            }

            scoreText.text = string.Format(
                scoreTemplateString,
                score, score == 1 ? "" : "s",
                axolotlsKilled, axolotlsKilled == 1 ? "" : "s",
                axolotlsTotal, axolotlsPreventablyKilled,
                message
            );
        }
    }

    public void OnRestartPressed()
    {
        GameManager.Instance.RestartGame();
    }
}
