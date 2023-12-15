using UnityEngine;
using UnityEngine.Serialization;
namespace Audio
{
    public class SettingsBehaviour : MonoBehaviour
    {
        //we need an array that contains our RTPC names to get our player preferences
        public string[] playerPrefsNames;
        //this is so the method is run as soon as the scene starts
        private void Start()
        {
            LoadPlayerPrefs();
        }
        
        //this method simple loads the player preferences based on the name
        private void LoadPlayerPrefs()
        {
            foreach (var pref in playerPrefsNames)
            {
                if(PlayerPrefs.HasKey(pref))
                    AkSoundEngine.SetRTPCValue(pref, PlayerPrefs.GetFloat(pref));
                else
                {
                    //if the key does not exist, set it to 0.8 and save it
                    PlayerPrefs.SetFloat(pref, 0.8f);
                    AkSoundEngine.SetRTPCValue(pref, 0.8f);
                }
            }
        }
    }
}