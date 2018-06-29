using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine;

public class RotateGear : MonoBehaviour, ITrackableEventHandler {

    [SerializeField] private GameObject gearOne;
    [SerializeField] private GameObject gearTwo;
    [SerializeField] private GameObject gearThree;
    [SerializeField] private GameObject gearFour;
    [SerializeField] private GameObject gearFive;
    [SerializeField] private GameObject gearSix;
    [SerializeField] private GameObject gearSeven;
    [SerializeField] private GameObject gearEight;
    [SerializeField] private GameObject gearNine;
    [SerializeField] private GameObject gearTen;
    [SerializeField] private GameObject gearEleven;
    [SerializeField] private GameObject gearTwelve;

    [SerializeField] private GameObject cardGearOne;
    [SerializeField] private GameObject cardGearTwo;
    [SerializeField] private GameObject cardGearThree;
    [SerializeField] private GameObject cardGearFour;

    [SerializeField] private GameObject boxMarkerOne;
    [SerializeField] private GameObject boxMarkerTwo;
    [SerializeField] private GameObject boxMarkerThree;
    [SerializeField] private GameObject boxMarkerFour;
    [SerializeField] private GameObject boxMarkerFive;

    [SerializeField] private GameObject cardMarkerOne;
    [SerializeField] private GameObject cardMarkerTwo;
    [SerializeField] private GameObject cardMarkerThree;
    [SerializeField] private GameObject cardMarkerFour;

    Animator anim;

    private float distance;
   

    private TrackableBehaviour mTrackableBehaviour;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("CanOpen", false);

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {

            
        }
        else
        {
             
        }
    } 

    public float MeasureFirstDistance()
    {
        float distance = Vector3.Distance(boxMarkerOne.transform.position, cardMarkerOne.transform.position);
        return distance;
    }

    public float MeasureSecondtDistance()
    {
        float distance = Vector3.Distance(boxMarkerTwo.transform.position, cardMarkerTwo.transform.position);
        return distance;
    }

    public float MeasureThirdDistance()
    {
        float distance = Vector3.Distance(boxMarkerThree.transform.position, cardMarkerThree.transform.position);
        return distance;
    }

    public float MeasureFourthDistance()
    {
        float distance = Vector3.Distance(boxMarkerFour.transform.position, cardMarkerFour.transform.position);
        return distance;
    }

    private void RotateGearOne()
    {
        gearOne.transform.Rotate(0, 0, 1);
    }

    private void RotateGearTwo()
    {
        gearTwo.SetActive(true);
        gearTwo.transform.Rotate(0, 0, -1);
    }

    private void RotateGearThree()
    {
        gearThree.transform.Rotate(0, 0, 1);
    }

    private void RotateGearFour()
    {
        gearFour.transform.Rotate(0, 0, 1);
    }

    private void RotateGearFive()
    {
        gearFive.SetActive(true);
        gearFive.transform.Rotate(0, 0, -1);
    }

    private void RotateGearSix()
    {
        gearSix.transform.Rotate(0, 0, 1);
    }

    private void RotateGearSeven()
    {
        gearSeven.transform.Rotate(0, 0, -1);
    }

    private void RotateGearEight()
    {
        gearEight.SetActive(true);
        gearEight.transform.Rotate(0, 0, 1);
    }

    private void RotateGearNine()
    {
        gearNine.transform.Rotate(0, 0, -1);
    }

    private void RotateGearTen()
    {
        gearTen.transform.Rotate(0, 0, 1);
    }

    private void RotateGearEleven()
    {
        gearEleven.SetActive(true);
        gearEleven.transform.Rotate(0, 0, -1);
    }

    private void RotateGearTwelve()
    {
        gearTwelve.transform.Rotate(0, 0, 1);
    }

    public void OpenChest()
    {
        anim.SetBool("CanOpen", true);
    }

    // Update is called once per frame
    void Update () {


        gearTwo.SetActive(false);
        gearFive.SetActive(false);
        gearEight.SetActive(false);
        gearEleven.SetActive(false);

        RotateGearOne();

        if(MeasureFirstDistance() <= 0.3f)
        {
            cardGearOne.SetActive(false);
   
            RotateGearTwo();
            RotateGearThree();

            if (gearTwo.activeSelf == true)
            {
                RotateGearFour();

                if (MeasureSecondtDistance() <= 0.3f)
                {
                    cardGearTwo.SetActive(false);

                    RotateGearFive();
                    RotateGearSix();

                    if (gearFive.activeSelf == true)
                    {
                        RotateGearSeven();

                        if(MeasureThirdDistance() <= 0.3f)
                        {
                            cardGearThree.SetActive(false);

                            RotateGearEight();
                            RotateGearNine();

                            if (gearEight.activeSelf == true)
                            {
                                RotateGearTen();

                                if (MeasureFourthDistance() <= 0.3f)
                                {
                                    cardGearFour.SetActive(false);

                                    RotateGearEleven();
                                    RotateGearTwelve();

                                    if (gearTwo.activeInHierarchy && gearFive.activeInHierarchy && gearEight.activeInHierarchy && gearEleven.activeInHierarchy)
                                    {
                                        OpenChest();
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
       /* else
        {
            cardGearOne.SetActive(true);
        }
        */
       
    }
}
