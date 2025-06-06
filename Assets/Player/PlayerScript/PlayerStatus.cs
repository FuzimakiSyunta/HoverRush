using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float moveSpeed = 18.0f;
    public float DamegeCoolTime;

    public bool isHeal;
    public bool isDamaged;
    public bool isTouchingLaser;
    public bool tookDamage;
    public bool isMoveActive;
    public bool isShieldActive;

    public bool isSinglePoweredUp;
    public bool isPenetrationPoweredUp;
    public bool isLaserPoweredUp;

    public int maxHp = 500;
    public int currentHp;
    public UnityEngine.UI.Slider hpSlider;

    void Start()
    {
        currentHp = maxHp;
        if (hpSlider != null)
        {
            hpSlider.value = 1.0f;
        }
    }

    public void SetHp(int value)
    {
        currentHp = Mathf.Clamp(value, 0, maxHp);
        if (hpSlider != null)
        {
            hpSlider.value = (float)currentHp / maxHp;
        }
    }

    public void IncreaseHp(int amount)
    {
        SetHp(currentHp + amount);
    }

    public void DecreaseHp(int amount)
    {
        SetHp(currentHp - amount);
    }

    public int GetHp() => currentHp;
    public int GetMaxHp() => maxHp;

    public float Speed() => moveSpeed * Time.deltaTime;
    public float DamegeCoolTimer() => DamegeCoolTime;
    public bool IsHeal() => isHeal;
    public bool IsDamage() => isDamaged;
    public bool IsTouchingLaser() => isTouchingLaser;
    public bool IsTookDamage() => tookDamage;
    public bool IsMoveActive() => isMoveActive;
    public bool IsSheildActive() => isShieldActive;

    public bool IsSinglePoweredUp() => isSinglePoweredUp;
    public bool IsPenetrationPoweredUp() => isPenetrationPoweredUp;
    public bool IsLaserPoweredUp() => isLaserPoweredUp;

    public void ResetDamageFlag() => tookDamage = false;
}