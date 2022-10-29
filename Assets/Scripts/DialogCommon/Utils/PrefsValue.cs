using UnityEngine;

namespace DialogCommon.Utils
{
    public class PrefsValue<T>
    {
        private readonly string _key;
        private readonly T _defaultValue;
        private bool _isLoaded;
        private T _value;
        
        public T Value
        {
            get
            {
                if (!_isLoaded)
                {
                    _isLoaded = true;
                    _value = ValueGet();
                }

                return _value;
            }

            set
            {
                _value = value;
                ValueSet(value);
            }
        }

        public PrefsValue(string key, T defaultValue)
        {
            _key = key;
            _defaultValue = defaultValue;
            _value = defaultValue;
        }
        
        public static PrefsValue<T>[] CreateArray(string name, T defaultValue, int count)
        {
            PrefsValue<T>[] result = new PrefsValue<T>[count];
            for (int i = 0; i < count; i++)
                result[i] = new PrefsValue<T>(name + "_" + i, defaultValue);
            
            return result;
        }
        
        private void ValueSet(T value)
        {
            switch (value)
            {
                case int i:
                    PlayerPrefs.SetInt(_key, i);
                    break;
                case float f:
                    PlayerPrefs.SetFloat(_key, f);
                    break;
                case string s:
                    PlayerPrefs.SetString(_key, s);
                    break;
                case bool b:
                    PlayerPrefs.SetInt(_key, b ? 1 : 0);
                    break;
            }
            
            PlayerPrefs.Save();
        }
        
        private T ValueGet()
        {
            if (typeof(T) == typeof(int))
                return (T)(object) PlayerPrefs.GetInt(_key, (int)(object) _defaultValue);
            if (typeof(T) == typeof(float))
                return (T)(object) PlayerPrefs.GetFloat(_key, (float)(object) _defaultValue);
            if (typeof(T) == typeof(string))
                return (T) (object) PlayerPrefs.GetString(_key, (string)(object) _defaultValue);
            if (typeof(T) == typeof(bool))
                return (T) (object) (PlayerPrefs.GetInt(_key, (bool)(object) _defaultValue ? 1 : 0) == 1);
            
            return _value;
        }
    }
}