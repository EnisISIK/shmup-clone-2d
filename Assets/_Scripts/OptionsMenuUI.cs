using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlienInvasion
{
    public class OptionsMenuUI : View
    {
        [SerializeField]
        private Button _backButton;

        public override void Initialize()
        {
            _backButton.onClick.AddListener(() => ViewManager.ShowLast());
        }

    }
}
