using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System;
using System.Globalization;
using System.Threading;

using UnityEngine.SceneManagement;

public class NewMissSensorAndEvent : MonoBehaviour
{
    [Header("Sensors")]
    public float sensorLength = 2f;
    public Vector3 frontCenterSensorPos = new Vector3(0f, 0.6f, 0f);
    public float frontSideSensorPos = 1f;
    public float frontSideAngle = 45f;

    [Header("Event")]
    public Text NearMissObject;
    public Text NearMissDistance;
    public Text TimeTagText;

    // This bool indicate if sensor is in ready status, when sensor status, the Sensor() in FixedUpdate is enabled.
    // This bool give the sensor some cool down ability, can use a timer to control sensor's ability to detect or record.
    // So the sensor don't record one event so many times.
    public bool sensorReadyStatus;

    //Sensor cool down time;
    public float sensorCDtime = 1.5f;
    // Timer
    private float timeRemaining;

    // Get speed from speedometer
    private Speedometer speedometer;

    // Use "TakeScreenShot" script to take screen shot
    //private TakeScreenShot takeSreenShot;


    // Start is called before the first frame update
    void Start()
    {
        speedometer = GameObject.Find("Speedometer").GetComponent<Speedometer>();

        //Take screen shot, can be disabled by commenting out
        //takeSreenShot = GameObject.Find("ScreenShotCamera").GetComponent<TakeScreenShot>();

        Scene scene = SceneManager.GetActiveScene();

        //Write the head of the csv file, adjust for different purpose accordingly
        DateTime localDate = DateTime.Now;
        var culture = new CultureInfo("en-US");
        WriteToFile("\n" + "UserID" + "," + "SpeedLimit(MPH)" + "," + "TestDate" + "," + "SceneName");
        WriteToFile("\n" + PlayerPrefs.GetString("ID") + "," + PlayerPrefs.GetInt("SpeedLimit") + "," + localDate.ToString(culture) + "," + scene.name);


        //Write the head of the csv file, adjust for different purpose accordingly
        WriteToFile("\n" + "Sytem Time" + "," + "NearMissObject" + "," + "NearMissDistance(m)" + "," + "ObjectTag" + "," + "UserPositionX" + "," + "UserPositionY" + "," + "UserPositionZ" + "," + "Speed");

        // On start, the sensor ready status is set to true
        sensorReadyStatus = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (sensorReadyStatus == false)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                //Debug.Log("Sensor is ready");
                timeRemaining = sensorCDtime;
                sensorReadyStatus = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if(sensorReadyStatus)
        {
            Sensors();
        }

    }

    private void Sensors()
    {
        RaycastHit hit;
        // front sensor start position, the location will be changed later
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * frontCenterSensorPos.z;
        sensorStartPos += transform.up * frontCenterSensorPos.y;

        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength * 0.8f))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            //NearMissObject.text = "Near Miss Object: " + hit.rigidbody.name.ToString();
            NearMissObject.text = "Near Missed: " + hit.collider;
            NearMissDistance.text = "Near Miss Distance: " + hit.distance.ToString("0.00") + " m";
            TimeTag();

            // The front sensor currently is not recording, so no "write to file"
        }

        //front right angle sensor

        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSideAngle, transform.up) * transform.forward, out hit, sensorLength))
        {

            Debug.DrawLine(sensorStartPos, hit.point);
            //NearMissObject.text = "Near Miss Object: " + hit.rigidbody.name.ToString();
            NearMissObject.text = "Near Missed: " + hit.collider;
            NearMissDistance.text = "Near Miss Distance: " + hit.distance.ToString("0.00") + " m";
            TimeTag();

            // Get system time
            System.DateTime time = System.DateTime.Now;

            // Covnert target location to string, with selected digits
            string userPositionX = transform.position.x.ToString("f3");
            string userPositionY = transform.position.y.ToString("f3");
            string userPositionZ = transform.position.z.ToString("f3");

            WriteToFile("\n" + time.ToLongTimeString() + "," + hit.collider.name + "," + hit.distance.ToString("0.00") + "," + hit.collider.tag + "," + userPositionX + "," + userPositionY + "," + userPositionZ + "," + speedometer.speed);

            // After detecting and recording an event, the sensor ready status is changed to false
            sensorReadyStatus = false;

            // Take screen shot for this event
            //takeSreenShot.TakeHiResShot();
        }

        //front left angle sensor

        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSideAngle, transform.up) * transform.forward, out hit, sensorLength))
        {

            Debug.DrawLine(sensorStartPos, hit.point);
            //NearMissObject.text = "Near Miss Object: " + hit.rigidbody.name.ToString();
            NearMissObject.text = "Near Missed: " + hit.collider;
            NearMissDistance.text = "Near Miss Distance: " + hit.distance.ToString("0.00") + " m";
            TimeTag();

            // Get system time
            System.DateTime time = System.DateTime.Now;

            // Covnert target location to string, with selected digits
            string userPositionX = transform.position.x.ToString("f3");
            string userPositionY = transform.position.y.ToString("f3");
            string userPositionZ = transform.position.z.ToString("f3");

            WriteToFile("\n" + time.ToLongTimeString() + "," + hit.collider.name + "," + hit.distance.ToString("0.00") + "," + hit.collider.tag + "," + userPositionX + "," + userPositionY + "," + userPositionZ + "," + speedometer.speed);

            // After detecting and recording an event, the sensor ready status is changed to false
            sensorReadyStatus = false;

            // Take screen shot for this event
            //takeSreenShot.TakeHiResShot();

        }
    }

    private void TimeTag()
    {
        TimeTagText.text = "Event Time: " + Time.time.ToString("0.00") + " s";

    }

    public void WriteToFile(string message)
    {
        // The path is in assets folder, can be changed to other path
        string path = Application.dataPath + "/TestData.csv";
        try
        {
            StreamWriter filewriter = new StreamWriter(path, true);
            filewriter.Write(message);
            filewriter.Close();
        }
        catch
        {
            Debug.LogError("cannot write to the file");
        }

    }


}
