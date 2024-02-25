using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float time;

    void Start()
    {
        Invoke(nameof(DestroySelf), time);
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }

}
