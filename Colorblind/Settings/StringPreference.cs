using Kitchen;
using System;
using UnityEngine;

namespace Colorblind.Settings {

    public class StringPreference : Preference<string> {

        public StringPreference(Pref key, string default_value, Action<string> action = null) : base(key, default_value, action) { }

        public override void Save() {
            PlayerPrefs.SetString(Key, Value);
        }

        public override void Load() {
            if (PlayerPrefs.HasKey(Key)) {
                Value = PlayerPrefs.GetString(Key);
            } else {
                Value = Default;
            }
        }
    }
}
