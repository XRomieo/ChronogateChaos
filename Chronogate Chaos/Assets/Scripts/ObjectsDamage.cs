using UnityEngine;

public class ObjectsDamage : MonoBehaviour
{
    [SerializeField] int totalHits = 0;
    [SerializeField] GameObject damageEffect;
    [SerializeField] GameObject destroyedObject;
    [SerializeField] SpriteRenderer mainGraphic;
    [SerializeField] Sprite[] DamageStates;
    [SerializeField] AudioClip[] DamageHitSFX;
    [SerializeField] AudioClip[] DestroyedSFX;
    public bool isBreakable = true;

    public void receiveDamage(UnityEngine.Vector2 hitPoint) {
        if (isBreakable) {
            UnityEngine.Quaternion rotation = UnityEngine.Quaternion.identity;
            Instantiate(damageEffect, hitPoint, rotation);
            AudioSource.PlayClipAtPoint(DamageHitSFX[Random.Range(0, DamageHitSFX.Length)], hitPoint);
            totalHits -= 1;
            if (totalHits == 0) {
                AudioSource.PlayClipAtPoint(DestroyedSFX[Random.Range(0, DestroyedSFX.Length)], hitPoint);
                Instantiate(destroyedObject, gameObject.transform.position, rotation);
                Destroy(gameObject);
            } else {
                mainGraphic.sprite = DamageStates[DamageStates.Length - totalHits];
            }
        }
    }

}
