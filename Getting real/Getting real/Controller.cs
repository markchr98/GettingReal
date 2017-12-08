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
            //Demo
            token.GetNewTokenResponse(ip);
            string input = "";
            while (input != "exit")
            {
                Console.Clear();
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
                                Console.WriteLine(p.PatientNumber, p.DepartmentId, p.PatientId, p.Firstname, p.Lastname, p.BirthDate, p.EntryDate, p.DichargeDate, p.EditedOn, p.EditedBy);
                            }
                        }
                        break;
                    case "devices":
                        foreach (Device d in Device.GetNewDeviceResponse(ip))
                        {
                            Console.WriteLine(d.AssignedPatientId, d.DeviceId, d.SerialNumber, d.ConnectionQuality, d.LastOnlineTime);
                        }
                        break;
                    case "livestate":
                        foreach (Device d in Device.GetNewDeviceResponse(ip))
                        {
                            foreach (Livestate l in Livestate.GetNewLivestates(ip, d.deviceSerial))
                            {
                                Console.WriteLine();
                            }
                        }
                        break;

                }
            }
        }
    }
}

