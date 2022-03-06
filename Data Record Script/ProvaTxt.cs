﻿using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using System;
using System.IO;
using ViveSR.anipal.Eye;
using ViveSR.anipal;
using ViveSR;

/// <summary>
/// Eye recording script, download online. not tested yet
/// </summary>

public class ProvaTxt : MonoBehaviour
{
    public static string UserID = "EyeRecording";
    public static string Path = Directory.GetCurrentDirectory();
    string File_Path = Directory.GetCurrentDirectory() + "\\StandardSaccade_" + UserID + ".txt";

    public static int cnt_callback = 0;
    private static long MeasureTime, CurrentTime, MeasureEndTime = 0;

    private static float time_stamp;
    private static int frame;

    //  Parameters for eye data.
    private static EyeData_v2 eyeData = new EyeData_v2();
    public EyeParameter eye_parameter = new EyeParameter();
    public GazeRayParameter gaze = new GazeRayParameter();
    private static bool eye_callback_registered = false;

    private const int maxframe_count = 120 * 30;                  // Maximum number of samples for eye tracking (120 Hz * time in seconds).
    private static UInt64 eye_valid_L, eye_valid_R;                 // The bits explaining the validity of eye data.
    private static float openness_L, openness_R;                    // The level of eye openness.
    private static float pupil_diameter_L, pupil_diameter_R;        // Diameter of pupil dilation.
    private static Vector2 pos_sensor_L, pos_sensor_R;              // Positions of pupils.
    private static Vector3 gaze_origin_L, gaze_origin_R;            // Position of gaze origin.
    private static Vector3 gaze_direct_L, gaze_direct_R;            // Direction of gaze ray.
    private static float frown_L, frown_R;                          // The level of user's frown.
    private static float squeeze_L, squeeze_R;                      // The level to show how the eye is closed tightly.
    private static float wide_L, wide_R;                            // The level to show how the eye is open widely.
    private static double gaze_sensitive;                           // The sensitive factor of gaze ray.
    private static float distance_C;                                // Distance from the central point of right and left eyes.
    private static bool distance_valid_C;                           // Validity of combined data of right and left eyes.

    private static int track_imp_cnt = 0;
    private static TrackingImprovement[] track_imp_item;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SystemCheck", 0.5f);                // System check.
        Invoke("Measurement", 0.5f);                // Start the measurement of ocular movements in a separate callback function.
    }


    //  Create a text file and header names of each column to store the measured data of eye movements.

    void Data_txt()
    {
        string variable =
        "time(100ns)" + "   " +
        "time_stamp(ms)" + "    " +
        "frame" + " " +
        "eye_valid_L" + "   " +
        "eye_valid_R" + "   " +
        "openness_L" + "    " +
        "openness_R" + "    " +
        "pupil_diameter_L(mm)" + "  " +
        "pupil_diameter_R(mm)" + "  " +
        "pos_sensor_L.x" + "    " +
        "pos_sensor_L.y" + "    " +
        "pos_sensor_R.x" + "    " +
        "pos_sensor_R.y" + "    " +
        "gaze_origin_L.x(mm)" + "   " +
        "gaze_origin_L.y(mm)" + "   " +
        "gaze_origin_L.z(mm)" + "   " +
        "gaze_origin_R.x(mm)" + "   " +
        "gaze_origin_R.y(mm)" + "   " +
        "gaze_origin_R.z(mm)" + "   " +
        "gaze_direct_L.x" + "   " +
        "gaze_direct_L.y" + "   " +
        "gaze_direct_L.z" + "   " +
        "gaze_direct_R.x" + "   " +
        "gaze_direct_R.y" + "   " +
        "gaze_direct_R.z" + "   " +
        "gaze_sensitive" + "    " +
        "frown_L" + "   " +
        "frown_R" + "   " +
        "squeeze_L" + " " +
        "squeeze_R" + " " +
        "wide_L" + "    " +
        "wide_R" + "    " +
        "distance_valid_C" + "  " +
        "distance_C(mm)" + "    " +
        "track_imp_cnt" +
        Environment.NewLine;

        File.AppendAllText("StandardSaccade_" + UserID + ".txt", variable);

    }

    //  Measure eye movements in a callback function that HTC SRanipal provides.

    void Measurement()
    {
        EyeParameter eye_parameter = new EyeParameter();
        SRanipal_Eye_API.GetEyeParameter(ref eye_parameter);
        Data_txt();

        if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == true && eye_callback_registered == false)
        {
            SRanipal_Eye_v2.WrapperRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye_v2.CallbackBasic)EyeCallback));
            eye_callback_registered = true;
        }

        else if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == false && eye_callback_registered == true)
        {
            SRanipal_Eye_v2.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye_v2.CallbackBasic)EyeCallback));
            eye_callback_registered = false;
        }
    }

    //  Callback function to record the eye movement data.
    //  Note that SRanipal_Eye_v2 does not work in the function below. It only works under UnityEngine.
    //
    // ********************************************************************************************************************
    private static void EyeCallback(ref EyeData_v2 eye_data)
    {
        EyeParameter eye_parameter = new EyeParameter();
        SRanipal_Eye_API.GetEyeParameter(ref eye_parameter);
        eyeData = eye_data;


        //  Measure eye movements at the frequency of 120Hz until framecount reaches the maxframe count set.
        // ----------------------------------------------------------------------------------------------------------------
        while (cnt_callback < maxframe_count)
        {
            ViveSR.Error error = SRanipal_Eye_API.GetEyeData_v2(ref eyeData);

            if (error == ViveSR.Error.WORK)
            {
                // --------------------------------------------------------------------------------------------------------
                //  Measure each parameter of eye data that are specified in the guideline of SRanipal SDK.
                // --------------------------------------------------------------------------------------------------------
                MeasureTime = DateTime.Now.Ticks;
                time_stamp = eyeData.timestamp;
                frame = eyeData.frame_sequence;
                eye_valid_L = eyeData.verbose_data.left.eye_data_validata_bit_mask;
                eye_valid_R = eyeData.verbose_data.right.eye_data_validata_bit_mask;
                openness_L = eyeData.verbose_data.left.eye_openness;
                openness_R = eyeData.verbose_data.right.eye_openness;
                pupil_diameter_L = eyeData.verbose_data.left.pupil_diameter_mm;
                pupil_diameter_R = eyeData.verbose_data.right.pupil_diameter_mm;
                pos_sensor_L = eyeData.verbose_data.left.pupil_position_in_sensor_area;
                pos_sensor_R = eyeData.verbose_data.right.pupil_position_in_sensor_area;
                gaze_origin_L = eyeData.verbose_data.left.gaze_origin_mm;
                gaze_origin_R = eyeData.verbose_data.right.gaze_origin_mm;
                gaze_direct_L = eyeData.verbose_data.left.gaze_direction_normalized;
                gaze_direct_R = eyeData.verbose_data.right.gaze_direction_normalized;
                gaze_sensitive = eye_parameter.gaze_ray_parameter.sensitive_factor;
                frown_L = eyeData.expression_data.left.eye_frown;
                frown_R = eyeData.expression_data.right.eye_frown;
                squeeze_L = eyeData.expression_data.left.eye_squeeze;
                squeeze_R = eyeData.expression_data.right.eye_squeeze;
                wide_L = eyeData.expression_data.left.eye_wide;
                wide_R = eyeData.expression_data.right.eye_wide;
                distance_valid_C = eyeData.verbose_data.combined.convergence_distance_validity;
                distance_C = eyeData.verbose_data.combined.convergence_distance_mm;
                track_imp_cnt = eyeData.verbose_data.tracking_improvements.count;
                ////track_imp_item = eyeData.verbose_data.tracking_improvements.items;

                //  Convert the measured data to string data to write in a text file.
                string value =
                    MeasureTime.ToString() + "  " +
                    time_stamp.ToString() + "   " +
                    frame.ToString() + "    " +
                    eye_valid_L.ToString() + "  " +
                    eye_valid_R.ToString() + "  " +
                    openness_L.ToString() + "   " +
                    openness_R.ToString() + "   " +
                    pupil_diameter_L.ToString() + " " +
                    pupil_diameter_R.ToString() + " " +
                    pos_sensor_L.x.ToString() + "   " +
                    pos_sensor_L.y.ToString() + "   " +
                    pos_sensor_R.x.ToString() + "   " +
                    pos_sensor_R.y.ToString() + "   " +
                    gaze_origin_L.x.ToString() + "  " +
                    gaze_origin_L.y.ToString() + "  " +
                    gaze_origin_L.z.ToString() + "  " +
                    gaze_origin_R.x.ToString() + "  " +
                    gaze_origin_R.y.ToString() + "  " +
                    gaze_origin_R.z.ToString() + "  " +
                    gaze_direct_L.x.ToString() + "  " +
                    gaze_direct_L.y.ToString() + "  " +
                    gaze_direct_L.z.ToString() + "  " +
                    gaze_direct_R.x.ToString() + "  " +
                    gaze_direct_R.y.ToString() + "  " +
                    gaze_direct_R.z.ToString() + "  " +
                    gaze_sensitive.ToString() + "   " +
                    frown_L.ToString() + "  " +
                    frown_R.ToString() + "  " +
                    squeeze_L.ToString() + "    " +
                    squeeze_R.ToString() + "    " +
                    wide_L.ToString() + "   " +
                    wide_R.ToString() + "   " +
                    distance_valid_C.ToString() + " " +
                    distance_C.ToString() + "   " +
                    track_imp_cnt.ToString() +
                    //track_imp_item.ToString() +
                    Environment.NewLine;

                File.AppendAllText("StandardSaccade_" + UserID + ".txt", value);

                cnt_callback++;
            }
        }
    }
}



