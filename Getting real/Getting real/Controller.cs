using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace Getting_real
{
    class Controller
    {
        //change ip to local      
        public const string ip = "localhost";
        public static Token token = new Token();

        public void Run()
        {
            Console.WriteLine("running");
            //Demo
            token.GetNewTokenResponse(ip);
            string input = "";
            while (input != "exit")
            {
                Console.Clear();
                Console.WriteLine("token/departments/patients/devices/livestates");
                switch (input = Console.ReadLine())
                {
                    case "token":
                        Console.WriteLine(token.AccessToken);
                        break;
                    case "departments":
                        foreach (Department d in Department.GetNewDeparments(ip))
                        {
                            Console.WriteLine("Id: " + d.DepartmentId + " Name: " + d.DepartmentName);
                        }
                        break;
                    case "patients":
                        foreach (Department d in Department.GetNewDeparments(ip))
                        {
                            foreach (Patient p in Patient.GetNewPatientResponse(ip, d.DepartmentId))
                            {
                                string result = "";
                                result += p.PatientNumber ?? "null";
                                result += p.DepartmentId ?? "null";
                                result += p.PatientId ?? "null";
                                result += p.Firstname ?? "null";
                                result += p.Lastname ?? "null";
                                result += p.BirthDate ?? "null";
                                result += p.EntryDate ?? "null";
                                result += p.DichargeDate ?? "null";
                                result += p.EditedOn ?? "null";
                                result += p.EditedBy ?? "null";
                                Console.WriteLine(result);
                            }
                        }
                        break;
                    case "devices":
                        foreach (Device d in Device.GetNewDeviceResponse(ip))
                        {
                            string result = "";
                            result += d.AssignedPatientId ?? "null";
                            result += d.DeviceId ?? "null";
                            result += d.SerialNumber ?? "null";
                            result += d.ConnectionQuality ?? "null";
                            result += d.LastOnlineTime ?? "null";
                            Console.WriteLine(result);
                        }
                        break;
                    case "livestates":
                        foreach (Device d in Device.GetNewDeviceResponse(ip))
                        {
                            foreach (Livestate l in Livestate.GetNewLivestates(ip, d.SerialNumber))
                            {
                                string result = "";
                                result += l.BedEmptyTimer ?? "null";
                                result += l.BedExitAlertSetting ?? "null";
                                result += l.BedExitAlertTimer ?? "null";
                                result += l.ControlSignal ?? "null";
                                result += l.ImmobilityAlertSetting ?? "null";
                                result += l.ImmobilityAlertTimer ?? "null";
                                result += l.SystemError ?? "null";
                                result += l.SystemErrorTimer ?? "null";
                                Console.WriteLine(result);
                            }
                        }
                        break;
                }
                Console.ReadLine();
            }
        }
    }
}

