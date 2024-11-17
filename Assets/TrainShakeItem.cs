using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class TrainShakeItem : MonoBehaviour
{
    [SerializeField] float minDisplacement = 0.05f;
    [SerializeField] float maxDisplacement = 0.5f;
    [SerializeField] float minInterval = 0.3f;
    [SerializeField] float maxInterval = 1.0f;
    [SerializeField] float displacementDuration = 0.2f;
    [SerializeField] AnimationCurve displacementCurve;

    Rigidbody2D playerRB;
    Vector2 restingPosition;

    Coroutine coroutine;

    void Start()
    {
        restingPosition = Train.Instance.transform.position;
        coroutine = StartCoroutine(ShakeTrain());
        playerRB = PlayerMovement.Instance.GetComponent<Rigidbody2D>();
    }

    IEnumerator ShakeTrain()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            StartCoroutine(CreateShake(Random.Range(minDisplacement, maxDisplacement)));
        }
    }

    IEnumerator CreateShake(float displacement)
    {
        float startTime = Time.time;
        float t;
        float lastT = 0;
        while((t = (Time.time - startTime) / displacementDuration) < 1)
        {
            var deltaT = t - lastT;
            if(deltaT > 0)
            {
                var lastDisp = displacementCurve.Evaluate(lastT) * displacement;
                var curDisp = displacementCurve.Evaluate(t) * displacement;
                var deltaDisp = curDisp - lastDisp;
                Train.Instance.transform.position += deltaDisp * Vector3.up;
                playerRB.position += deltaDisp * Vector2.up;
            }
            lastT = t;
            yield return new WaitForFixedUpdate();
        }
        Train.Instance.transform.position = restingPosition;
    }
}
