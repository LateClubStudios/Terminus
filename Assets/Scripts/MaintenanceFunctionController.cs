using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MaintenanceFunctionController : MonoBehaviour {

    //* 0.9216721 players y hight
    bool skip = false;
    public float rotation;

	// Use this for initialization
	void Start () {
        GameObject.Find("Player/PlayerRig/mixamorig:Hips").transform.position = new Vector3(0f, 6.5f, -9f);
        GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().mainSwitch = false;
        GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().ragdollToggle = true;
        GameObject.Find("Player/PlayerRig/mixamorig:Hips").transform.rotation = new Quaternion(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f), 0f);
        GameObject.Find("PlayerVisionCone").SetActive(false);
        StartCoroutine(Delay00());
        skip = false;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            skip = true;
        }
        projectionLoad();
    }



    IEnumerator Delay00()
    {
        Debug.Log("00");
        yield return new WaitForSeconds(1.0f);
        if (skip == true)
        {
            GameObject.Find("Player/PlayerRig").GetComponent<CapsuleCollider>().enabled = true;
            GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().ragdollToggle = false;
            GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().mainSwitch = true;
            GameObject.Find("DropCubeHolder/DropCubeSlide02").SetActive(false);
            GameObject.Find("DropCubeHolder/DropCubeSlide02").SetActive(false);
        }
        else
        {
            StartCoroutine(Delay01());
        }
    }

    IEnumerator Delay01()
    {
        Debug.Log("01");
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("DropCubeHolder/DropCube").SetActive(false);
        GameObject.Find("Player/PlayerRig").GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find("Player/PlayerRig").GetComponent<CapsuleCollider>().enabled = false;
        GameObject.Find("Player/PlayerRig").GetComponent<CapsuleCollider>().isTrigger = true;
        GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().ragdollToggle = true;
        StartCoroutine(Delay02());
    }

    IEnumerator Delay02()
    {
        Debug.Log("02");
        yield return new WaitForSeconds(3.0f);
        GameObject.Find("Player/PlayerRig").GetComponent<CapsuleCollider>().isTrigger = false;
        GameObject.Find("Player/PlayerRig").GetComponent<Rigidbody>().isKinematic = false;
        GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().ragdollToggle = false;
        GameObject.Find("Player/PlayerRig").GetComponent<CapsuleCollider>().enabled = true;
        StartCoroutine(Delay03());
    }

    IEnumerator Delay03()
    {
        Debug.Log("03");
        yield return new WaitForSeconds(7.0f);
        GameObject.Find("DropCubeHolder").SetActive(false);
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("Projectors/Projector01").GetComponent<Animator>().SetBool("On", true);
        yield return new WaitForSeconds(5.0f);
        GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().mainSwitch = true;
        yield return new WaitForSeconds(1.0f);
        Debug.Log("p01.1");
        rotation = GameObject.Find("Player/PlayerRig").transform.rotation.y;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("p01.2");
        StartCoroutine(Projector01());
    }

    IEnumerator Projector01()
    {
        yield return new WaitForSeconds(0.0f);
        Debug.Log("p01.3");
        if (GameObject.Find("Player/PlayerRig").transform.rotation.y > rotation + 0.1 || GameObject.Find("Player/PlayerRig").transform.rotation.y < rotation - 0.1)
        {
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector01").GetComponent<Animator>().SetBool("On", false);
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector02").GetComponent<Animator>().SetBool("On", true);
            StartCoroutine(Projector02());
        }
        else
        {
            StartCoroutine(Projector01());
        }
    }


    IEnumerator Projector02()
    {
        yield return new WaitForSeconds(0.0f);
        Debug.Log("p02.3");
        if (Mathf.Round(Input.GetAxis("Horizontal")) != 0.0f && GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == false || Mathf.Round(Input.GetAxis("HorizontalXbox")) != 0.0f && GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == true)
        { 
            Debug.Log("BOIIIIIIIIyay");
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector02").GetComponent<Animator>().SetBool("On", false);
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector03").GetComponent<Animator>().SetBool("On", true);
            StartCoroutine(Projector03());
        }
        else
        {
            Debug.Log("BOIIIIIIII");
            StartCoroutine(Projector02());
        }
    }

    IEnumerator Projector03()
    {
        yield return new WaitForSeconds(0.0f);
        Debug.Log("p03.3");
        if (Input.GetButtonDown("Grab") && GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == false || Input.GetButtonDown("GrabXbox") && GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == true)
        {
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector03").GetComponent<Animator>().SetBool("On", false);
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector04").GetComponent<Animator>().SetBool("On", true);
            StartCoroutine(Projector04());
        }
        else
        {
            StartCoroutine(Projector03());
        }
    }

    IEnumerator Projector04()
    {
        yield return new WaitForSeconds(0.0f);
        Debug.Log("p04.3");
        if (GameObject.Find("Player/PlayerRig").transform.position.y > 1.5)
        {
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector04").GetComponent<Animator>().SetBool("On", false);
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector05").GetComponent<Animator>().SetBool("On", true);
            StartCoroutine(Projector05());
        }
        else
        {
            StartCoroutine(Projector04());
        }
    }

    IEnumerator Projector05()
    {
        yield return new WaitForSeconds(0.0f);
        Debug.Log("p04.3");
        if (GameObject.Find("Player/PlayerRig").transform.position.y > 3)
        {
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector05").GetComponent<Animator>().SetBool("On", false);
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector06").GetComponent<Animator>().SetBool("On", true);
            StartCoroutine(Projector06());
        }
        else
        {
            StartCoroutine(Projector05());
        }
    }

    IEnumerator Projector06()
    {
        yield return new WaitForSeconds(0.0f);
        Debug.Log("p04.3");
        if (Input.GetButtonDown("Crouch") && GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == false || Input.GetButtonDown("CrouchXbox") && GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == true)
        {
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector06").GetComponent<Animator>().SetBool("On", false);
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector07").GetComponent<Animator>().SetBool("On", true);
            StartCoroutine(Projector07());
        }
        else
        {
            StartCoroutine(Projector06());
        }
    }

    IEnumerator Projector07()
    {
        yield return new WaitForSeconds(0.0f);
        Debug.Log("p04.3");
        if (Input.GetButtonDown("Cover") && GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == false || Input.GetButtonDown("CoverXbox") && GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == true)
        {
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector07").GetComponent<Animator>().SetBool("On", false);
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector08").GetComponent<Animator>().SetBool("On", true);
            StartCoroutine(Projector08());
        }
        else
        {
            StartCoroutine(Projector07());
        }
    }

    IEnumerator Projector08()
    {
        yield return new WaitForSeconds(0.0f);
        Debug.Log("p04.3");
        if (Input.GetButtonDown("Crouch") && GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == false || Input.GetButtonDown("CrouchXbox") && GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == true)
        {
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector08").GetComponent<Animator>().SetBool("On", false);
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Turret/TurretUpper/Projector09").GetComponent<Animator>().SetBool("On", true);
            StartCoroutine(Projector09());
        }
        else
        {
            StartCoroutine(Projector08());
        }
    }

    IEnumerator Projector09()
    {
        yield return new WaitForSeconds(0.0f);
        Debug.Log("p04.3");
        if (Input.GetButtonDown("Cover") && GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == false || Input.GetButtonDown("CoverXbox") && GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == true)
        {
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Turret/TurretUpper/Projector09").GetComponent<Animator>().SetBool("On", false);
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("Projectors/Projector10").GetComponent<Animator>().SetBool("On", true);
            StartCoroutine(Projector10());
        }
        else
        {
            StartCoroutine(Projector09());
        }
    }

    IEnumerator Projector10()
    {
        yield return new WaitForSeconds(0.0f);
    }

        void projectionLoad()
    {
        if (GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == true)
        {
            GameObject.Find("Projectors/Projector01/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use Right Joystick to Turn";
            GameObject.Find("Projectors/Projector02/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use Left Joystick to Move";
            GameObject.Find("Projectors/Projector03/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use RT to Grab";
            GameObject.Find("Projectors/Projector04/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use A to Vault";
            GameObject.Find("Projectors/Projector05/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use A to Climb Up";
            GameObject.Find("Projectors/Projector06/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use One of the keys to Climb Down";
            GameObject.Find("Projectors/Projector07/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use One of the keys to Dismout";
            GameObject.Find("Projectors/Projector08/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use One of the keys to Crouch";
            GameObject.Find("Turret/TurretUpper/Projector09/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use One of the keys to Cover";
            GameObject.Find("Projectors/Projector10/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use A to Jump";
        }
        else if (GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().controllerSwitch == false)
        {
            GameObject.Find("Projectors/Projector01/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use Mouse to Turn";
            GameObject.Find("Projectors/Projector02/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use A & D to Move";
            GameObject.Find("Projectors/Projector03/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use E to Grab";
            GameObject.Find("Projectors/Projector04/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use Space to Vault";
            GameObject.Find("Projectors/Projector05/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use Space to Climb Up";
            GameObject.Find("Projectors/Projector06/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use Ctrl to Climb Down";
            GameObject.Find("Projectors/Projector07/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use Q to Dismount";
            GameObject.Find("Projectors/Projector08/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use Ctrl to Crouch";
            GameObject.Find("Turret/TurretUpper/Projector09/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use Q to Cover";
            GameObject.Find("Projectors/Projector10/Projection/ProjectionUiCanvas/ProjectionText").GetComponent<Text>().text = "Use Space to Jump";
        }
    }

}
