using UnityEngine;
using UnityEngine.UI;

public class BossStatus : MonoBehaviour
{
    public int bossHP = 1000;
    public int nowHP;

    public Slider bossSlider;

    void Start()
    {
        nowHP = bossHP;
        if (bossSlider != null)
            bossSlider.maxValue = bossHP;
    }

    public void ApplyDamage(int amount)
    {
        nowHP -= amount;
        if (bossSlider != null)
            bossSlider.value = nowHP;
    }

    public bool IsDead()
    {
        return nowHP <= 0;
    }
}