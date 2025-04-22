using character;
using character.attribute;
using TMPro;
using ui.frame;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace UI
{
    public class SoliderUI : FollowUI
    {
        [Binding("_slider")] public Slider slider { get; set; }
        [Binding("_textValue")] public TextMeshProUGUI textValue { get; set; }

        // public new GoSolidersUI bindingGo;

        private HealthPoints _health;

        public HealthPoints health
        {
            get => _health;
            set
            {
                _health = value;
                _health.onValueChange += onHealthChange;
            }
        }

        private void onHealthChange(float value)
        {
            if (value <= 0)
            {
                return;
            }

            slider.value = value / health.maxValue;
            textValue.text = value + "/" + health.maxValue;
        }
    }
}