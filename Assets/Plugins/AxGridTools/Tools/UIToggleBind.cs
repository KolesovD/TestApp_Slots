using AxGrid.Base;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxGrid.Tools.Binders
{
    [RequireComponent(typeof(Toggle))]
    public class UIToggleBind : Binder
    {
        private Toggle _toggle;

        /// <summary>
		/// Название переключателя
		/// </summary>
		[SerializeField] private string _toggleName;
        public string ToogleName => _toggleName;

        /// <summary>
		/// Название поля в модели, где будет храниться значение
		/// </summary>
		[SerializeField] private string _toggleValueField;
        public string ToggleValueField => _toggleValueField;

        public const string VALUE_PART = "Value";

        [OnAwake]
        private void OnAwake()
        {
            _toggle = GetComponent<Toggle>();

            //Split-Join должен быть самым быстрым
            if (string.IsNullOrEmpty(_toggleName))
                _toggleName = string.Join("", name.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            if (string.IsNullOrEmpty(_toggleValueField))
                _toggleValueField = $"{_toggleName}{VALUE_PART}";
        }

        [OnStart]
        private void OnStart()
        {
            _toggle.onValueChanged.AddListener(OnValueChanged);
            OnValueChanged(_toggle.isOn);
        }

        private void OnValueChanged(bool newValue)
        {
            Model.Set(_toggleValueField, newValue);

            if (newValue)
                Settings.Fsm?.Invoke("OnToggleOn", _toggleName);
        }

        [OnDestroy]
        private void OnDestroyInner()
        {
            _toggle.onValueChanged.RemoveAllListeners();
        }
    }
}
