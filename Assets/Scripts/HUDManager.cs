using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public OrbSkill orb;
    public MapController map;

    public TMPro.TMP_Text orbCooldownText;
    public TMPro.TMP_Text mapCooldownText;

    void Update()
    {
        float orbCooldown = orb.GetCooldownProgress();
        orbCooldownText.text = Mathf.CeilToInt(orbCooldown * orb.cooldown) + "s";

        float mapCooldown = map.GetCooldownProgress();
        mapCooldownText.text = Mathf.CeilToInt(mapCooldown * map.cooldown) + "s";
    }
}
