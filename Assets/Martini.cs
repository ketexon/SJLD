using UnityEngine;

public class Martini : MonoBehaviour
{
    [SerializeField] GameObject root;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled)
        {
            return;
        }
        GameManager.Instance.Martinis++;
        enabled = false;
        Destroy(root);
        MusicManager.Instance.MartiniSound.Play();
    }
}