using UnityEngine;
namespace ChanceTools
{
    [CreateAssetMenu(fileName = "SpinnerDataSO", menuName = "Chance Tools/Spinner Data")]
    public class SpinnerDataSO : ScriptableObject
    {

        [SerializeField] private string spinnerName = "Generic Spinner";
        [Min(0.005f)][SerializeField] private float startInterval = 0.05f;
        [Min(0.05f)][SerializeField] private float endInterval = 0.5f;
        [Min(1.005f)][SerializeField] private float slowdown = 1.1f;
        [Range(0.0f, 1.0f)][SerializeField] private float onSpinVariance = 0.0f;
        [Min(2)][SerializeField] private int numberOfValues = 6;
        [SerializeField] private bool randomizeInitialValue = false;
        [SerializeField] private bool wheelMode = false; // whether it's a roulette wheel or a die

        public string SpinnerName { get => spinnerName; }
        public float StartInterval { get => startInterval; }
        public float EndInterval { get => endInterval; }
        public float Slowdown { get => slowdown; }
        public float OnSpinVariance { get => onSpinVariance; }
        public int NumberOfValues { get => numberOfValues; }
        public bool RandomizeInitialValue { get => randomizeInitialValue; }
        public bool WheelMode { get => wheelMode; } // whether it acts like a die or a roulette wheel

        private void OnValidate()
        {
            if (endInterval <= startInterval) Debug.LogError("End interval must be greater than start interval");
            if (startInterval * (1.0 + onSpinVariance) > endInterval) Debug.LogError("On Spin Variance is too high");
        }
    }
}