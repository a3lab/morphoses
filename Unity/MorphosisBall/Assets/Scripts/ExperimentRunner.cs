using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Facilitates the running of experiments.
public class ExperimentRunner : MonoBehaviour
{

  bool isStarted = false;
  System.String id = null;
  double startTime;

  // Use this for initialization
  void Start()
  {
    NewExperiment();
    System.Guid.NewGuid();
  }

  // Update is called once per frame
  void Update()
  {
    // We may want to run the simulation without the joystick,
    // so we will support using keys instead
    if (Input.GetButtonDown("Fire2") || Input.GetKeyDown("q"))
    {
      Debug.Log("new experiment");
      NewExperiment();
    }

    if (Input.GetButtonDown("Fire3") || Input.GetKeyDown("w"))
    {
      Debug.Log("Starting experiment. Will now send data.");
      StartExperiment();
    }
  }

  void NewExperiment()
  {
    // Give the sphere a random position for the new experiment.
    transform.position = Random.insideUnitCircle * 20;
    transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    transform.rotation = Random.rotation;
    id = System.DateTime.Now.ToString("yyyy-MM-ddTHH:MM:ss");
    isStarted = false;

    // Reset speed an steering.
    GetComponent<BallController>().speed = 0; //up & down
    GetComponent<BallController>().steering = 0; // left and right

  }

  void StartExperiment()
  {
    isStarted = true;
    startTime = Time.time;
    if (GetComponent("ReceiveOSC"))
    {
      Debug.Log("Listening for incoming OSC");
      ReceiveOSC recv = GetComponent("ReceiveOSC") as ReceiveOSC;
      recv.newExperiment();
    }
  }

  public bool IsStarted() { return isStarted; }

  public System.String GetId() { return id; }

  public double GetTime()
  {
    // Debug.Log(Time.time - startTime);
    return Time.time - startTime;
  }

}
