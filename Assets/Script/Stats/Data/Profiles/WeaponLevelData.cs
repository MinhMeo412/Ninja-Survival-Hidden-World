using UnityEngine;

[System.Serializable]
public class WeaponLevelData
{
    [Tooltip("Thời gian giữa các lần bắn (giây)")]
    public float fireRate;
    [Tooltip("Số lượng đạn bắn ra trong 1 lần bắn \n Để 0 nếu là loại bắn liên tục")]
    public int projectileCount;
    [Tooltip("Thời gian tồn tại/duy trì của đạn")]
    public float duration;
    [Tooltip("Sát thương tăng thêm")]
    public float damageIncrease;
}
