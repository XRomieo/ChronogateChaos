using System.Collections;
using UnityEngine;

public class SelfDisable : MonoBehaviour
{
    [SerializeField] private float time;
    private bool coroutineMade = false;
    void Update()
    {
        if (!coroutineMade) {
            StartCoroutine(DisableSelf());
            coroutineMade = true;
        }
    }

    private IEnumerator DisableSelf() {
        yield return new WaitForSeconds(time);
        StopAllCoroutines();
        coroutineMade = false;
        gameObject.SetActive(false);
    }

}
