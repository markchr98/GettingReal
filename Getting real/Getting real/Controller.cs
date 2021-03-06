﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using MySql.Data.MySqlClient;
using System.Data;

namespace Getting_real
{
    public class Controller
    {
        //change ip to local      
        public const string ip = "localhost";
        public static Token token = new Token();
        private MySqlDataReader reader;

        private List<Department> departmentList;
        private List<Device> DeviceList;
        private List<Patient> patientlist;
        private List<Comment> commentList;
        private List<Observation> ObservationList;
        private List<Statistic> statisticList;
        private List<Livestate> livestateList;

        public void Run()
        {            
            departmentList = new List<Department>();
            DeviceList = new List<Device>();
            patientlist = new List<Patient>();
            commentList = new List<Comment>();
            ObservationList = new List<Observation>();
            statisticList = new List<Statistic>();
            livestateList = new List<Livestate>();

            token.GetNewTokenResponse(ip);
            GetDataFromCloud();
            SynchronizeCloud();                     
        }

        public void GetDataFromCloud()
        {
            DBConnection.Instance().Open();

            MySqlCommand getDepartments = new MySqlCommand("get_departments", DBConnection.Instance().Connection);
            using (reader = getDepartments.ExecuteReader())
            {
                while (reader.Read())
                {
                    departmentList.Add(new Department() { DepartmentId = reader["departmentId"].ToString() });
                }
            }

            MySqlCommand getPatients = new MySqlCommand("get_patient", DBConnection.Instance().Connection);
            using (reader = getPatients.ExecuteReader())
            {
                while (reader.Read())
                {
                    patientlist.Add(new Patient() { PatientId = reader["patientId"].ToString() });
                }
            }

            MySqlCommand getComments = new MySqlCommand("get_comment", DBConnection.Instance().Connection);
            using (reader = getComments.ExecuteReader())
            {
                while (reader.Read())
                {
                    commentList.Add(new Comment() { CommentId = reader["commentId"].ToString() });
                }
            }

            MySqlCommand getObservations = new MySqlCommand("get_observation", DBConnection.Instance().Connection);
            using (reader = getObservations.ExecuteReader())
            {
                while (reader.Read())
                {
                    ObservationList.Add(new Observation() { ObservationId = reader["observationId"].ToString() });
                }
            }

            MySqlCommand getStatistics = new MySqlCommand("get_statistic", DBConnection.Instance().Connection);
            using (reader = getStatistics.ExecuteReader())
            {
                while (reader.Read())
                {
                    statisticList.Add(new Statistic() { PatientId = reader["patientId"].ToString() });
                }
            }

            MySqlCommand getDevices = new MySqlCommand("get_devices", DBConnection.Instance().Connection);
            using (reader = getDevices.ExecuteReader())
            {
                while (reader.Read())
                {
                    DeviceList.Add(new Device() { DeviceId = reader["deviceId"].ToString() });
                }
            }

            MySqlCommand getLivestates = new MySqlCommand("get_livestate", DBConnection.Instance().Connection);
            using (reader = getLivestates.ExecuteReader())
            {
                while (reader.Read())
                {
                    livestateList.Add(new Livestate() { DeviceId = reader["deviceId"].ToString() });
                }
            }

            DBConnection.Instance().Close();
        }

        public void SynchronizeCloud()
        {
            DBConnection.Instance().Open();

            foreach (Department department in Department.GetNewDeparments(ip))
            {
                int countDepartment = 0;
                foreach (Department uDepartment in departmentList)
                {                    
                    //Checks if a department already exists in the cloud database by using the primary key departmentId
                    if (department.DepartmentId == uDepartment.DepartmentId)
                    {
                        countDepartment++;
                        break;
                    }
                }

                if (countDepartment == 0)
                {
                    //inserts new department into cloud database
                    MySqlCommand insertDepartment = new MySqlCommand("insertinto_department", DBConnection.Instance().Connection);
                    insertDepartment.CommandType = CommandType.StoredProcedure;
                    insertDepartment.Parameters.Add(new MySqlParameter("setdepartmentId", department.DepartmentId));
                    insertDepartment.Parameters.Add(new MySqlParameter("setdepartmentName", department.DepartmentName));
                    insertDepartment.ExecuteNonQuery();
                }

                else
                {
                    //updates existing department in cloud database
                    MySqlCommand updateDepartment = new MySqlCommand("update_department", DBConnection.Instance().Connection);
                    updateDepartment.CommandType = CommandType.StoredProcedure;
                    updateDepartment.Parameters.Add(new MySqlParameter("setdepartmentId", department.DepartmentId));
                    updateDepartment.Parameters.Add(new MySqlParameter("setdepartmentName", department.DepartmentName));

                    updateDepartment.ExecuteNonQuery();
                }

                //insert patients inside department loop because departmentId is required
                foreach (Patient patient in Patient.GetNewPatientResponse(ip, department.DepartmentId))
                {
                    int countPatient = 0;
                    foreach (Patient uPatient in patientlist)
                    {
                        if (patient.PatientId == uPatient.PatientId)
                        {
                            countPatient++;
                            break;
                        }
                    }
                    if (countPatient == 0)
                    {
                        //inserts new patient into cloud database
                        MySqlCommand insertPatient = new MySqlCommand("insertinto_patient", DBConnection.Instance().Connection);
                        insertPatient.CommandType = CommandType.StoredProcedure;

                        insertPatient.Parameters.Add(new MySqlParameter("setdepartmentId", patient.DepartmentId));
                        insertPatient.Parameters.Add(new MySqlParameter("setpatientId", patient.PatientId));
                        insertPatient.Parameters.Add(new MySqlParameter("setpatientNumber", patient.PatientNumber));
                        insertPatient.Parameters.Add(new MySqlParameter("setfirstname", patient.Firstname));
                        insertPatient.Parameters.Add(new MySqlParameter("setlastname", patient.Lastname));
                        insertPatient.Parameters.Add(new MySqlParameter("setbirthDate", patient.BirthDate));
                        insertPatient.Parameters.Add(new MySqlParameter("setentryDate", patient.EntryDate));
                        insertPatient.Parameters.Add(new MySqlParameter("setdischargeDate", patient.DischargeDate));
                        insertPatient.Parameters.Add(new MySqlParameter("seteditedOn", patient.EditedOn));
                        insertPatient.Parameters.Add(new MySqlParameter("seteditedBy", patient.EditedBy));

                        insertPatient.ExecuteNonQuery();
                    }

                    else
                    {
                        //updates existing patient in cloud database
                        MySqlCommand updatePatient = new MySqlCommand("update_patient", DBConnection.Instance().Connection);
                        updatePatient.CommandType = CommandType.StoredProcedure;

                        updatePatient.Parameters.Add(new MySqlParameter("setdepartmentId", patient.DepartmentId));
                        updatePatient.Parameters.Add(new MySqlParameter("setpatientId", patient.PatientId));
                        updatePatient.Parameters.Add(new MySqlParameter("setpatientNumber", patient.PatientNumber));
                        updatePatient.Parameters.Add(new MySqlParameter("setfirstname", patient.Firstname));
                        updatePatient.Parameters.Add(new MySqlParameter("setlastname", patient.Lastname));
                        updatePatient.Parameters.Add(new MySqlParameter("setbirthDate", patient.BirthDate));
                        updatePatient.Parameters.Add(new MySqlParameter("setentryDate", patient.EntryDate));
                        updatePatient.Parameters.Add(new MySqlParameter("setdischargeDate", patient.DischargeDate));
                        updatePatient.Parameters.Add(new MySqlParameter("seteditedOn", patient.EditedOn));
                        updatePatient.Parameters.Add(new MySqlParameter("seteditedBy", patient.EditedBy));

                        updatePatient.ExecuteNonQuery();
                    }


                    foreach (Comment comment in Comment.GetComments(ip, patient.PatientId))
                    {
                        int countComment = 0;

                        foreach (Comment uComment in commentList)
                        {
                            if (comment.CommentId == uComment.CommentId)
                            {
                                countComment++;
                                break;
                            }
                        }

                        if (countComment == 0)
                        {
                            //inserts new comment into cloud database
                            MySqlCommand insertComment = new MySqlCommand("insertinto_comment", DBConnection.Instance().Connection);
                            insertComment.CommandType = CommandType.StoredProcedure;

                            insertComment.Parameters.Add(new MySqlParameter("setcommentId", comment.CommentId));
                            insertComment.Parameters.Add(new MySqlParameter("setcommentText", comment.CommentText));
                            insertComment.Parameters.Add(new MySqlParameter("setpatientId", comment.PatientId));
                            insertComment.Parameters.Add(new MySqlParameter("setuserId", comment.UserId));
                            insertComment.Parameters.Add(new MySqlParameter("seteditedOn", comment.EditedOn));
                            insertComment.Parameters.Add(new MySqlParameter("settime", comment.Time));

                            insertComment.ExecuteNonQuery();
                        }

                        else
                        {
                            MySqlCommand updateComment = new MySqlCommand("update_comment", DBConnection.Instance().Connection);
                            updateComment.CommandType = CommandType.StoredProcedure;

                            updateComment.Parameters.Add(new MySqlParameter("setcommentId", comment.CommentId));
                            updateComment.Parameters.Add(new MySqlParameter("setcommentText", comment.CommentText));
                            updateComment.Parameters.Add(new MySqlParameter("setpatientId", comment.PatientId));
                            updateComment.Parameters.Add(new MySqlParameter("setuserId", comment.UserId));
                            updateComment.Parameters.Add(new MySqlParameter("seteditedOn", comment.EditedOn));
                            updateComment.Parameters.Add(new MySqlParameter("settime", comment.Time));

                            updateComment.ExecuteNonQuery();
                        }
                    }

                    foreach (Observation observation in Observation.GetObservations(ip, patient.PatientId))
                    {
                        int countObservation = 0;
                        foreach (Observation uObservation in ObservationList)
                        {
                            if (observation.ObservationId == uObservation.ObservationId)
                            {
                                countObservation++;
                                break;
                            }
                        }

                        if (countObservation == 0)
                        {
                            //inserts new observation into cloud database
                            MySqlCommand updateObservation = new MySqlCommand("insertinto_observation", DBConnection.Instance().Connection);
                            updateObservation.CommandType = CommandType.StoredProcedure;

                            updateObservation.Parameters.Add(new MySqlParameter("setobservationId", observation.ObservationId));
                            updateObservation.Parameters.Add(new MySqlParameter("setdurationInMinutes", observation.DurationInMinutes));
                            updateObservation.Parameters.Add(new MySqlParameter("setpatientId", observation.PatientId));
                            updateObservation.Parameters.Add(new MySqlParameter("settime", observation.Time));
                            updateObservation.Parameters.Add(new MySqlParameter("setuserId", observation.UserId));

                            updateObservation.ExecuteNonQuery();
                        }
                        else
                        {
                            //updates existing observation in cloud database
                            MySqlCommand updateObservation = new MySqlCommand("update_observation", DBConnection.Instance().Connection);
                            updateObservation.CommandType = CommandType.StoredProcedure;

                            updateObservation.Parameters.Add(new MySqlParameter("setobservationId", observation.ObservationId));
                            updateObservation.Parameters.Add(new MySqlParameter("setdurationInMinutes", observation.DurationInMinutes));
                            updateObservation.Parameters.Add(new MySqlParameter("setpatientId", observation.PatientId));
                            updateObservation.Parameters.Add(new MySqlParameter("settime", observation.Time));
                            updateObservation.Parameters.Add(new MySqlParameter("setuserId", observation.UserId));

                            updateObservation.ExecuteNonQuery();
                        }
                    }

                    Statistic statistic = Statistic.GetStatistic(ip, patient.PatientId);
                    int countStatistic = 0;
                    foreach (Statistic uStatistic in statisticList)
                    {
                        if (statistic.PatientId == uStatistic.PatientId)
                        {
                            countStatistic++;
                            break;
                        }
                    }
                    if (countStatistic == 0)
                    {
                        MySqlCommand insertStatistic = new MySqlCommand("insertinto_statistic", DBConnection.Instance().Connection);
                        insertStatistic.CommandType = CommandType.StoredProcedure;
                        insertStatistic.Parameters.Add(new MySqlParameter("setpatientId", statistic.PatientId));
                        insertStatistic.Parameters.Add(new MySqlParameter("setdateFrom", statistic.From));
                        insertStatistic.Parameters.Add(new MySqlParameter("setdateTo", statistic.To));
                        insertStatistic.Parameters.Add(new MySqlParameter("settotalTimeInBed", statistic.TotalTimeInBed));
                        insertStatistic.Parameters.Add(new MySqlParameter("setmaxTimeWithoutMobility", statistic.MaxTimeWithoutMobility));
                        insertStatistic.Parameters.Add(new MySqlParameter("setnumberOfBedExits", statistic.NumberOfBedExits));
                        insertStatistic.Parameters.Add(new MySqlParameter("setnumberOfBedExitWarnings", statistic.NumberOfBedExitWarnings));
                        insertStatistic.Parameters.Add(new MySqlParameter("setnumberOfConfirmedBedExitWarnings", statistic.NumberOfConfirmedBedExitWarnings));
                        insertStatistic.Parameters.Add(new MySqlParameter("setnumberOfImmobilityWarnings", statistic.NumberOfImmobilityWarnings));
                        insertStatistic.Parameters.Add(new MySqlParameter("setnumberOfManualRegisteredRepositionings", statistic.NumberOfManualRegisteredRepositionings));
                        insertStatistic.Parameters.Add(new MySqlParameter("setnumberOfMovementsPerHour", statistic.NumberOfMovementsPerHour));

                        insertStatistic.ExecuteNonQuery();
                    }
                    else
                    {
                        MySqlCommand updateStatistic = new MySqlCommand("update_statistic", DBConnection.Instance().Connection);
                        updateStatistic.CommandType = CommandType.StoredProcedure;
                        updateStatistic.Parameters.Add(new MySqlParameter("setpatientId", statistic.PatientId));
                        updateStatistic.Parameters.Add(new MySqlParameter("setdateFrom", statistic.From));
                        updateStatistic.Parameters.Add(new MySqlParameter("setdateTo", statistic.To));
                        updateStatistic.Parameters.Add(new MySqlParameter("settotalTimeInBed", statistic.TotalTimeInBed));
                        updateStatistic.Parameters.Add(new MySqlParameter("setmaxTimeWithoutMobility", statistic.MaxTimeWithoutMobility));
                        updateStatistic.Parameters.Add(new MySqlParameter("setnumberOfBedExits", statistic.NumberOfBedExits));
                        updateStatistic.Parameters.Add(new MySqlParameter("setnumberOfBedExitWarnings", statistic.NumberOfBedExitWarnings));
                        updateStatistic.Parameters.Add(new MySqlParameter("setnumberOfConfirmedBedExitWarnings", statistic.NumberOfConfirmedBedExitWarnings));
                        updateStatistic.Parameters.Add(new MySqlParameter("setnumberOfImmobilityWarnings", statistic.NumberOfImmobilityWarnings));
                        updateStatistic.Parameters.Add(new MySqlParameter("setnumberOfManualRegisteredRepositionings", statistic.NumberOfManualRegisteredRepositionings));
                        updateStatistic.Parameters.Add(new MySqlParameter("setnumberOfMovementsPerHour", statistic.NumberOfMovementsPerHour));

                        updateStatistic.ExecuteNonQuery();
                    }
                }
            }
            foreach (Device device in Device.GetNewDeviceResponse(ip))
            {
                int countDevice = 0;
                foreach (Device uDevice in DeviceList)
                {
                    if (device.DeviceId == uDevice.DeviceId)
                    {
                        countDevice++;
                        break;
                    }
                }
                if (countDevice == 0)
                {
                    //inserts new device into cloud database
                    MySqlCommand updateDevice = new MySqlCommand("insertinto_device", DBConnection.Instance().Connection);
                    updateDevice.CommandType = CommandType.StoredProcedure;

                    updateDevice.Parameters.Add(new MySqlParameter("setdeviceId", device.DeviceId));
                    updateDevice.Parameters.Add(new MySqlParameter("setassignedPatientId", device.AssignedPatientId));
                    updateDevice.Parameters.Add(new MySqlParameter("setconnectionQuality", device.ConnectionQuality));
                    updateDevice.Parameters.Add(new MySqlParameter("setlastOnlineTime", device.LastOnlineTime));
                    updateDevice.Parameters.Add(new MySqlParameter("setserialNumber", device.SerialNumber));

                    updateDevice.ExecuteNonQuery();
                }

                else
                {
                    //updates existing device in cloud database
                    MySqlCommand updateDevice = new MySqlCommand("update_device", DBConnection.Instance().Connection);
                    updateDevice.CommandType = CommandType.StoredProcedure;

                    updateDevice.Parameters.Add(new MySqlParameter("setdeviceId", device.DeviceId));
                    updateDevice.Parameters.Add(new MySqlParameter("setassignedPatientId", device.AssignedPatientId));
                    updateDevice.Parameters.Add(new MySqlParameter("setconnectionQuality", device.ConnectionQuality));
                    updateDevice.Parameters.Add(new MySqlParameter("setlastOnlineTime", device.LastOnlineTime));
                    updateDevice.Parameters.Add(new MySqlParameter("setserialNumber", device.SerialNumber));

                    updateDevice.ExecuteNonQuery();
                }

                Livestate livestate = Livestate.GetLivestate(ip, device.SerialNumber);
                int countLivestate = 0;
                foreach (Livestate uLivestate in livestateList)
                {
                    if (livestate.DeviceId == uLivestate.DeviceId)
                    {
                        countLivestate++;
                        break;
                    }
                }
                if (countLivestate == 0)
                {
                    //inserts new livestate for a device in cloud database
                    MySqlCommand insertLivestate = new MySqlCommand("insertinto_livestate", DBConnection.Instance().Connection);
                    insertLivestate.CommandType = CommandType.StoredProcedure;
                    insertLivestate.Parameters.Add(new MySqlParameter("setdeviceId", livestate.DeviceId));
                    insertLivestate.Parameters.Add(new MySqlParameter("setbedEmptyTimer", livestate.BedEmptyTimer));
                    insertLivestate.Parameters.Add(new MySqlParameter("setbedExitAlertSetting", livestate.BedExitAlertSetting));
                    insertLivestate.Parameters.Add(new MySqlParameter("setbedExitAlertTimer", livestate.BedExitAlertTimer));
                    insertLivestate.Parameters.Add(new MySqlParameter("setcontrolSignal", livestate.ControlSignal));
                    insertLivestate.Parameters.Add(new MySqlParameter("setimmobilityAlertSetting", livestate.ImmobilityAlertSetting));
                    insertLivestate.Parameters.Add(new MySqlParameter("setimmobilityAlertTimer", livestate.ImmobilityAlertTimer));
                    insertLivestate.Parameters.Add(new MySqlParameter("setsystemError", livestate.SystemError));
                    insertLivestate.Parameters.Add(new MySqlParameter("setsystemErrorTimer", livestate.SystemErrorTimer));

                    insertLivestate.ExecuteNonQuery();
                }
                else
                {
                    //updates an existing livestate in cloud database
                    MySqlCommand updateLivestate = new MySqlCommand("update_livestate", DBConnection.Instance().Connection);
                    updateLivestate.CommandType = CommandType.StoredProcedure;
                    updateLivestate.Parameters.Add(new MySqlParameter("setdeviceId", livestate.DeviceId));
                    updateLivestate.Parameters.Add(new MySqlParameter("setbedEmptyTimer", livestate.BedEmptyTimer));
                    updateLivestate.Parameters.Add(new MySqlParameter("setbedExitAlertSetting", livestate.BedExitAlertSetting));
                    updateLivestate.Parameters.Add(new MySqlParameter("setbedExitAlertTimer", livestate.BedExitAlertTimer));
                    updateLivestate.Parameters.Add(new MySqlParameter("setcontrolSignal", livestate.ControlSignal));
                    updateLivestate.Parameters.Add(new MySqlParameter("setimmobilityAlertSetting", livestate.ImmobilityAlertSetting));
                    updateLivestate.Parameters.Add(new MySqlParameter("setimmobilityAlertTimer", livestate.ImmobilityAlertTimer));
                    updateLivestate.Parameters.Add(new MySqlParameter("setsystemError", livestate.SystemError));
                    updateLivestate.Parameters.Add(new MySqlParameter("setsystemErrorTimer", livestate.SystemErrorTimer));
                    updateLivestate.Parameters.Add(new MySqlParameter("setlastRelevantMovement",livestate.LastRelevantMovement));

                    updateLivestate.ExecuteNonQuery();
                }
            }
            DBConnection.Instance().Close();
        }
    }
}

