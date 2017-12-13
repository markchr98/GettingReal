using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using MySql.Data.MySqlClient;

namespace Getting_real
{
    public class Controller
    {
        //change ip to local      
        public const string ip = "localhost";
        public static Token token = new Token();

        public static void Run()
        {



       
            //DBConnection.Instance().Open();
            //string cmdText = "INSERT INTO department VALUES (@departmentId, @name)";
            //MySqlCommand cmd = new MySqlCommand(cmdText, DBConnection.Instance().Connection);
            //cmd.Parameters.AddWithValue("@departmentId", "Test");
            //cmd.Parameters.AddWithValue("@name", "Test");
            //cmd.ExecuteNonQuery();
            //DBConnection.Instance().Close();

            //Console.WriteLine("running");
            ////Demo
            //token.GetNewTokenResponse(ip);
            //string input = "";
            //while (input != "exit")
            //{
            //    Console.Clear();
            //    Console.WriteLine("token/departments/patients/devices/livestates");
            //    switch (input = Console.ReadLine())
            //    {
            //        case "token":
            //            Console.WriteLine(token.AccessToken);
            //            break;
            //        case "departments":
            //            foreach (Department d in Department.GetNewDeparments(ip))
            //            {
            //                Console.WriteLine("Id: " + d.DepartmentId + " Name: " + d.DepartmentName);
            //            }
            //            break;
            //        case "patients":
            //            foreach (Department d in Department.GetNewDeparments(ip))
            //            {
            //                foreach (Patient p in Patient.GetNewPatientResponse(ip, d.DepartmentId))
            //                {
            //                    string result1 = "";
            //                    result1 += p.PatientNumber ?? "null";
            //                    result1 += p.DepartmentId ?? "null";
            //                    result1 += p.PatientId ?? "null";
            //                    result1 += p.Firstname ?? "null";
            //                    result1 += p.Lastname ?? "null";
            //                    result1 += p.BirthDate ?? "null";
            //                    result1 += p.EntryDate ?? "null";
            //                    result1 += p.DichargeDate ?? "null";
            //                    result1 += p.EditedOn ?? "null";
            //                    result1 += p.EditedBy ?? "null";
            //                    Console.WriteLine(result1);
            //                }
            //            }
            //            break;
            //        case "devices":
            //            foreach (Device d in Device.GetNewDeviceResponse(ip))
            //            {
            //                string result1 = "";
            //                result1 += d.AssignedPatientId ?? "null";
            //                result1 += d.DeviceId ?? "null";
            //                result1 += d.SerialNumber ?? "null";
            //                result1 += d.ConnectionQuality ?? "null";
            //                result1 += d.LastOnlineTime ?? "null";
            //                Console.WriteLine(result1);
            //            }
            //            break;
            //        case "livestates":
            //            foreach (Device d in Device.GetNewDeviceResponse(ip))
            //            {
            //                Livestate l = Livestate.GetNewLivestates(ip, "1910190");

            //                string result = "";
            //                result += l.BedEmptyTimer ?? "null";
            //                result += l.BedExitAlertSetting ?? "null";
            //                result += l.BedExitAlertTimer ?? "null";
            //                result += l.ControlSignal ?? "null";
            //                result += l.ImmobilityAlertSetting ?? "null";
            //                result += l.ImmobilityAlertTimer ?? "null";
            //                result += l.SystemError ?? "null";
            //                result += l.SystemErrorTimer ?? "null";
            //                Console.WriteLine(result);

            //            }
            //            break;
            //    }
            //    Console.ReadLine();
        }        
    }
}

