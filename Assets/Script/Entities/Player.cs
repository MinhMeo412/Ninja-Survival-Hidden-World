using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour, IPoolable<CharacterProfile>
{
    public CharacterProfile profile { get; private set; }

    private bool initialized = false;

    public Transform playerTarget = null;

    private SpriteRenderer sr;
    private CharacterStats stats;

    //OnHit flash
    private Material materialInstance;
    private Tween flashTween;
    private static readonly int FlashAmount = Shader.PropertyToID("_FlashAmount");

    public System.Action<Player> OnDeath;

    public void OnSpawn(CharacterProfile profile)
    {
        if (!initialized)
        {
            this.profile = profile;

            sr = gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = profile.worldSprite;
                materialInstance = sr.material;
            }

            Animator anim = gameObject.GetComponent<Animator>();
            if (anim != null)
            {
                anim.runtimeAnimatorController = profile.animatorController;
            }

            stats = gameObject.GetComponent<CharacterStats>();
            if (stats != null)
            {
                stats.Init(profile);
            }

            initialized = true;
        }
    }

    public void OnDespawn()
    {

    }

    public void OnHitFlash()
    {
        flashTween?.Kill();

        // Set trắng hoàn toàn
        materialInstance.SetFloat(FlashAmount, 1f);

        // Fade trắng -> bình thường
        flashTween = DOTween.To(
            () => materialInstance.GetFloat(FlashAmount),
            x => materialInstance.SetFloat(FlashAmount, x),
            0f,
            0.2f
        );
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Enemy
        if (collision.TryGetComponent(out EnemyStats enemyStats))
        {
            //Trừ HP
            if (stats.damageTriggerInterval == 0)
            {
                stats.OnHPChanged(-enemyStats.damage);
                OnHitFlash();
            }
        }

        //Item

        //Projectile
        if (collision.TryGetComponent(out ProjectileStats projectileStats))
        {
            if(collision.CompareTag("Enemy"))
            //Trừ HP
            if (stats.damageTriggerInterval == 0)
            {
                stats.OnHPChanged(-projectileStats.damage);
                OnHitFlash(); 
            }       
        }
    }
}
