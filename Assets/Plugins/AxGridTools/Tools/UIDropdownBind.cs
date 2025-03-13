using AxGrid.Base;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxGrid.Tools.Binders
{
    [RequireComponent(typeof(Dropdown))]
    public class UIDropdownBind : Binder
    {
        private Dropdown _dropdown;

        /// <summary>
		/// Название списка
		/// </summary>
		[SerializeField] private string _dropdownName;
        public string DropdownName => _dropdownName;

        /// <summary>
		/// Название поля в модели, где будет храниться значение
		/// </summary>
		[SerializeField] private string _dropdownValueField;
        public string DropdownValueField => _dropdownValueField;

        public const string VALUE_PART = "Value";

        [OnAwake]
        private void OnAwake()
        {
            _dropdown = GetComponent<Dropdown>();

            //Split-Join должен быть самым быстрым
            if (string.IsNullOrEmpty(_dropdownName))
                _dropdownName = string.Join("", name.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            if (string.IsNullOrEmpty(_dropdownValueField))
                _dropdownValueField = $"{_dropdownName}{VALUE_PART}";
        }

        [OnStart]
        private void OnStart()
        {
            _dropdown.onValueChanged.AddListener(OnValueChanged);
            OnValueChanged(_dropdown.value);
        }

        private void OnValueChanged(int newValue)
        {
            Model.Set(_dropdownValueField, newValue);

            Settings.Fsm?.Invoke("OnDropdownChanged", $"{_dropdownName}_{newValue}");
        }

        [OnDestroy]
        private void OnDestroyInner()
        {
            _dropdown.onValueChanged.RemoveAllListeners();
        }
    }
}
