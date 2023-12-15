using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Audio
{
    //Serializable creates a class which we can edit in the inspector
    [Serializable]
    public class WwiseBus
    {
        //reference to our slider
        public Slider _slider;
        //and our RTPC as a string
        public string _rtpcName;
        //we need to add a listener to set and save the value when it is changed
        public void AddListener()
        {
            _slider.onValueChanged.AddListener(delegate
            {
                AkSoundEngine.SetRTPCValue(_rtpcName, _slider.value);
                SaveVolume(_slider.value);
            });
        }
        //this loads all the volues and if the key does not exist, set the initial volume as 0.8
        public void LoadVolume()
        {
            if (!PlayerPrefs.HasKey(_rtpcName))
                PlayerPrefs.SetFloat(_rtpcName, 0.8f);
            
            _slider.value = PlayerPrefs.GetFloat(_rtpcName);
        }
        //remove the listener when it is no longer needed
        public void RemoveListener()
        {
            _slider.onValueChanged.RemoveAllListeners();
        }
        //save the volume
        private void SaveVolume(float value)
        {
            PlayerPrefs.SetFloat(_rtpcName, value);
        }
        //and finally delete the key, for debugging or resetting the user data
        public void DeleteData()
        {
            PlayerPrefs.DeleteKey(_rtpcName);
        }
    }
    public class ClassStyleSettings : MonoBehaviour
    {
        //we can now create a list of our brand new WwiseBus classes
        public List <WwiseBus> buses;
        
        //when this object is enabled, it loads the volumes and adds the listener
        private void OnEnable()
        {
            foreach (var bus in buses)
            {
                bus.LoadVolume();
                bus.AddListener();
            }
        }
        //when this object it disabled, it removes the listener
        private void OnDisable()
        {
            foreach (var bus in buses)
            {
                bus.RemoveListener();
            }
        }
        //and finally a method we can call to remove all the user data!
        public void DeleteUserData()
        {
            foreach (var bus in buses)
            {
                bus.DeleteData();
                bus.LoadVolume();
            }
        }
    }
}