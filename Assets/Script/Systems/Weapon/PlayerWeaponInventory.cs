using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeaponInventory : MonoBehaviour
{
    public static PlayerWeaponInventory Instance;
    private Dictionary<ProjectileProfile, int> ownedWeapons = new Dictionary<ProjectileProfile, int>();
    private Dictionary<ProjectileProfile, PlayerWeaponHandler> activeHandlers = new Dictionary<ProjectileProfile, PlayerWeaponHandler>();
    private ProjectileList weaponLists = null;

    public Dictionary<ProjectileProfile, int> OwnedWeapons => this.ownedWeapons;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        //Tạm thời chỉ có 1 wp Kunai nên cho update luôn khi lên level
        PlayerLevelManager.OnLevelUp += UpgradeKunai;
    }

    private void OnDisable()
    {
        PlayerLevelManager.OnLevelUp -= UpgradeKunai;
    }

    private void Start()
    {
        weaponLists = PoolManager.Instance.ProjectileList;
        string character = gameObject.GetComponent<Player>().profile.characterName;
        if (character == "Ninja")
        {
            ProjectileProfile profile = null;
            foreach (ProjectileProfile x in weaponLists.profiles)
            {
                if (x.projectileName == "Kunai")
                {
                    profile = x;
                }    
            }    
            AddWeapon(profile);
        }    
    }

    public void AddWeapon(ProjectileProfile profile)
    {
        if (!ownedWeapons.ContainsKey(profile))
        {
            ownedWeapons.Add(profile, 1);

            PlayerWeaponHandler handler = gameObject.AddComponent<PlayerWeaponHandler>();
            handler.Init(profile, 1);
            activeHandlers.Add(profile, handler);
        }
        else
        {
            UpgradeWeapon(profile);
        }
    }

    public void UpgradeWeapon(ProjectileProfile profile)
    {
        if (ownedWeapons.ContainsKey(profile))
        {
            int newLevel = ownedWeapons[profile] + 1;

            if(activeHandlers[profile].UpdateLevel(newLevel))
            {
                ownedWeapons[profile]++;
            }    
        }
    }

    public void UpgradeKunai()
    {
        ProjectileProfile kunaiProfile = ownedWeapons.Keys.FirstOrDefault(profile => profile.projectileName == "Kunai");
        if (kunaiProfile != null)
        {
            UpgradeWeapon(kunaiProfile);
        }
    }    
}
