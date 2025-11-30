using UnityEngine;
using System.Collections.Generic;
namespace ChanceTools
{

    public struct SpinnerStatus
    {
        public int currentValue;
        public bool isStopped;

        public SpinnerStatus(int _currentValue, bool _isStopped)
        {
            currentValue = _currentValue;
            isStopped = _isStopped;
        }

    }

    public class Spinner : MonoBehaviour
    {
        public event System.Action<SpinnerStatus> SpinnerUpdated;

        [SerializeField] SpinnerDataSO spinnerData;
        private SpinnerStatus myStatus = new SpinnerStatus(-1, true);
        private string _spinnerName;
        public string SpinnerName { get => _spinnerName; }


        private void Awake()
        {
            _spinnerName = spinnerData.SpinnerName;
        }

        public void Spin()
        {

            if (!myStatus.isStopped) { return; }

            int initialValue = 0;
            if (spinnerData.RandomizeInitialValue) { initialValue = Random.Range(0, spinnerData.NumberOfValues); }
            myStatus = new SpinnerStatus(initialValue, false);

            float startVarianceFactor = 1.0f;
            if (spinnerData.OnSpinVariance > 0.0)
            {
                startVarianceFactor += Random.Range(0.0f, spinnerData.OnSpinVariance);
            }
            SpinnerUpdated?.Invoke(myStatus);
            StartCoroutine(Iterate(spinnerData.StartInterval * startVarianceFactor));
        }

        private void Advance()
        {
            if (spinnerData.WheelMode) { AdvanceWheel(); }
            else { AdvanceDie(); }
        }

        private void AdvanceWheel()
        {
            int newValue = (myStatus.currentValue + 1) % spinnerData.NumberOfValues;
            myStatus = new SpinnerStatus(newValue, false);
            SpinnerUpdated?.Invoke(myStatus);
        }
        private void AdvanceDie()
        {
            int newValue = -1;
            List<int> possibleNewValues = new List<int>();
            for (int i = 0; i < spinnerData.NumberOfValues; i++) { possibleNewValues.Add(i); }
            possibleNewValues.Remove(myStatus.currentValue);
            int randoIdx = Random.Range(0, possibleNewValues.Count);
            newValue = possibleNewValues[randoIdx];

            myStatus = new SpinnerStatus(newValue, false);
            SpinnerUpdated?.Invoke(myStatus);
        }

        private System.Collections.IEnumerator Iterate(float waitTime)
        {
            bool _isStopped = (waitTime >= spinnerData.EndInterval);
            if (_isStopped)
            {
                myStatus = new SpinnerStatus(myStatus.currentValue, true);
                SpinnerUpdated?.Invoke(myStatus);
                yield break;
            }

            else
            {
                yield return new WaitForSeconds(waitTime);
                Advance();
                StartCoroutine(Iterate(waitTime * spinnerData.Slowdown));
            }
        }

        private void OnValidate()
        {
            if (spinnerData == null)
            {
                Debug.LogError("No spinner data configired.\nIn the project window: Chance Tools -> Spinner Data");
            }
        }
    }
}

