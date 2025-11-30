using UnityEngine;
using ChanceTools;

public class Die : MonoBehaviour
{

    private Spinner mySpinner;

    private void Awake()
    {
        mySpinner = GetComponent<Spinner>();
    }
    private void OnEnable()
    {
        mySpinner.SpinnerUpdated += HandleOnSpinnerUpdated;
    }
    private void OnDisable()
    {
        mySpinner.SpinnerUpdated -= HandleOnSpinnerUpdated;
    }

    private void Start()
    {
        //StartCoroutine(Test());
    }

    public void Roll()
    {

    }

    private void HandleOnSpinnerUpdated(SpinnerStatus spinnerStatus)
    {
        if (spinnerStatus.isStopped)
        {
            Debug.Log($"The result is: {spinnerStatus.currentValue + 1}");
        }
        else
        {
            Debug.Log(spinnerStatus.currentValue + 1);
        }
    }

    private System.Collections.IEnumerator Test()
    {
        yield return new WaitForSeconds(0.5f);
        mySpinner.Spin();
    }


    private void OnValidate()
    {
        if (GetComponent<Spinner>() == null) { Debug.LogError("Adda spinner component"); }
    }

}