using UnityEngine;

[CreateAssetMenu]
public class AudioSet : ScriptableObject
{
    [SerializeField] AudioClip[] clips;

    public AudioClip GetRandom()
    {
        int i = Random.Range(0, clips.Length);
        return clips[i];
    }
}