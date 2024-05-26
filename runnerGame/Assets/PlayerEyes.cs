  using UnityEngine;

public class PlayerEyes : MonoBehaviour
{
    public GameObject normalEye;
    public GameObject fearEye;
    public GameObject slapEye;

    const string k_PlayerTag = "Player";

    private bool isFearEyeActive = false;
    private bool isSlapEyeActive = false;
    private bool isNormalSlap = false;

    public  bool isPlayerNear = false;
    public  bool isBossNear = false;
    public  bool isPlayerSlapping = false;
    public bool isSlapping = false;

 

    void Start()
    {
        // Initially, only the normal eye is active
        normalEye.SetActive(true);
        fearEye.SetActive(false);
        slapEye.SetActive(false);
    }

    void Update()
    {
        // Check if player or boss is near
        // For demonstration purposes, let's assume a variable `isPlayerNear` and `isBossNear` are set elsewhere in the code
        // Replace this with actual logic

        if (isPlayerNear || isBossNear)
        {
            ActivateFearEye();
        }
        else
        {
            DeactivateFearEye();
        }

        // Check if player slaps
        // For demonstration purposes, let's assume a variable `isPlayerSlapping` is set elsewhere in the code
       // Replace this with actual logic

        if (isPlayerSlapping)
        {
            ActivateSlapEye();
        }
        else
        {
            DeactivateSlapEye();
        }

        if (isSlapping)
        {
            ActiveNormalEye();
        }
        else
        {

        }
    }
    

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(k_PlayerTag))
        {
          
           
        }
    }

    void ActiveNormalEye()
    {
        if (isFearEyeActive)
        {
            normalEye.SetActive(true);
            fearEye.SetActive(true);
            isFearEyeActive = false;
        }
    }

    void ActivateFearEye()
    {
        if (!isFearEyeActive)
        {
            normalEye.SetActive(false);
            fearEye.SetActive(true);
            isFearEyeActive = true;
        }
    }

    void DeactivateFearEye()
    {
        if (isFearEyeActive)
        {
            normalEye.SetActive(true);
            fearEye.SetActive(false);
            isFearEyeActive = false;
        }
    }

    void ActivateSlapEye()
    {
        if (!isSlapEyeActive)
        {
            normalEye.SetActive(false);
            fearEye.SetActive(false);
            slapEye.SetActive(true);
            isSlapEyeActive = true;
        }
    }

    void DeactivateSlapEye()
    {
        if (isSlapEyeActive)
        {
            normalEye.SetActive(true);
            slapEye.SetActive(false);
            isSlapEyeActive = false;
        }
    }
}



