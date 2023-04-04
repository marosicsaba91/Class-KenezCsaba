using UnityEngine;
using UnityEngine.UI;

class TargetLifebar : MonoBehaviour
{
    [SerializeField] Image positiveImage;
    [SerializeField] LaserTarget target;

    void OnValidate()
    {
        if (target == null)
            target = GetComponentInParent<LaserTarget>();
    }

    void Start()
    {
        if (target == null)
            target = GetComponentInParent<LaserTarget>();

        target.HPChanged += HPChanged;
    }


    public void HPChanged()
    {
        positiveImage.fillAmount = target.HPRate;
    }

}