using Newtonsoft.Json.Linq;
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

        public static void Run()
        {
            MySqlDataReader reader;
            token.GetNewTokenResponse(ip);
            //insert departments. write check ifExists and update ifExists is true

            foreach (Department department in Department.GetNewDeparments(ip))
            {
                int countDepartment = 0;
                MySqlCommand getDepartments = new MySqlCommand("get_departments", DBConnection.Instance().Connection);
                using (reader = getDepartments.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Check dep id først hvis eksisterer check på alle data. evt kun check på alle data i en ny timer
                        if (department.DepartmentId == reader["departmentId"].ToString())
                        {
                            countDepartment++;
                            break;
                        }                        
                    }
                    if (countDepartment == 0)
                    {
                        //inserts department into cloud database
                        MySqlCommand insertDepartment = new MySqlCommand("insertinto_department", DBConnection.Instance().Connection);
                        insertDepartment.CommandType = CommandType.StoredProcedure;
                        insertDepartment.Parameters.Add(new MySqlParameter("departmentId", department.DepartmentId));
                        insertDepartment.Parameters.Add(new MySqlParameter("departmentName", department.DepartmentName));
                        DBConnection.Instance().Open();
                        insertDepartment.ExecuteNonQuery();
                        DBConnection.Instance().Close();
                    }
                    else
                    {
                        MySqlCommand updateDepartment = new MySqlCommand("update_department", DBConnection.Instance().Connection);
                        updateDepartment.CommandType = CommandType.StoredProcedure;
                        updateDepartment.Parameters.Add(new MySqlParameter("departmentId", department.DepartmentId));
                        updateDepartment.Parameters.Add(new MySqlParameter("departmentName", department.DepartmentName));
                        DBConnection.Instance().Open();
                        updateDepartment.ExecuteNonQuery();
                        DBConnection.Instance().Close();
                    }
                }
                //insert patients inside department loop because dep param is needed
                foreach (Patient patient in Patient.GetNewPatientResponse(ip, department.DepartmentId))
                {
                    int countPatient = 0;
                    MySqlCommand getPatients = new MySqlCommand("get_patients", DBConnection.Instance().Connection);
                    using (reader = getPatients.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (patient.PatientId == reader["patientId"].ToString())
                            {
                                countPatient++;
                                break;
                            }
                        }
                        if (countPatient == 0)
                        {
                            MySqlCommand insertPatient = new MySqlCommand("insertinto_patient", DBConnection.Instance().Connection);
                            insertPatient.CommandType = CommandType.StoredProcedure;

                            insertPatient.Parameters.Add(new MySqlParameter("departmentId", patient.DepartmentId));
                            insertPatient.Parameters.Add(new MySqlParameter("patientId", patient.PatientId));
                            insertPatient.Parameters.Add(new MySqlParameter("patientNumber", patient.PatientNumber));
                            insertPatient.Parameters.Add(new MySqlParameter("firstname", patient.Firstname));
                            insertPatient.Parameters.Add(new MySqlParameter("lastname", patient.Lastname));
                            insertPatient.Parameters.Add(new MySqlParameter("birthDate", patient.BirthDate));
                            insertPatient.Parameters.Add(new MySqlParameter("entryDate", patient.EntryDate));
                            insertPatient.Parameters.Add(new MySqlParameter("dischargeDate", patient.DischargeDate));
                            insertPatient.Parameters.Add(new MySqlParameter("editedOn", patient.EditedOn));
                            insertPatient.Parameters.Add(new MySqlParameter("editedBy", patient.EditedBy));

                            DBConnection.Instance().Open();
                            insertPatient.ExecuteNonQuery();
                            DBConnection.Instance().Close();
                        }
                        else
                        {
                            MySqlCommand updatePatient = new MySqlCommand("update_patient", DBConnection.Instance().Connection);
                            updatePatient.CommandType = CommandType.StoredProcedure;

                            updatePatient.Parameters.Add(new MySqlParameter("departmentId", patient.DepartmentId));
                            updatePatient.Parameters.Add(new MySqlParameter("patientId", patient.PatientId));
                            updatePatient.Parameters.Add(new MySqlParameter("patientNumber", patient.PatientNumber));
                            updatePatient.Parameters.Add(new MySqlParameter("firstname", patient.Firstname));
                            updatePatient.Parameters.Add(new MySqlParameter("lastname", patient.Lastname));
                            updatePatient.Parameters.Add(new MySqlParameter("birthDate", patient.BirthDate));
                            updatePatient.Parameters.Add(new MySqlParameter("entryDate", patient.EntryDate));
                            updatePatient.Parameters.Add(new MySqlParameter("dischargeDate", patient.DischargeDate));
                            updatePatient.Parameters.Add(new MySqlParameter("editedOn", patient.EditedOn));
                            updatePatient.Parameters.Add(new MySqlParameter("editedBy", patient.EditedBy));

                            DBConnection.Instance().Open();
                            updatePatient.ExecuteNonQuery();
                            DBConnection.Instance().Close();
                        }
                    }
                    foreach (Comment comment in Comment.GetComments(ip, patient.PatientId))
                    {
                        int countComment = 0;
                        MySqlCommand getComments = new MySqlCommand("get_comment", DBConnection.Instance().Connection);
                        using(reader = getComments.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (comment.CommentId == reader["commentId"].ToString())
                                {
                                    countComment++;
                                    break;
                                }
                            }
                            if(countComment == 0)
                            {
                                MySqlCommand insertComment = new MySqlCommand("insertinto_comment", DBConnection.Instance().Connection);
                                insertComment.CommandType = CommandType.StoredProcedure;

                                insertComment.Parameters.Add(new MySqlParameter("commentId", comment.CommentId));
                                insertComment.Parameters.Add(new MySqlParameter("commentText", comment.CommentText));
                                insertComment.Parameters.Add(new MySqlParameter("patientId", comment.PatientId));
                                insertComment.Parameters.Add(new MySqlParameter("userId", comment.UserId));
                                insertComment.Parameters.Add(new MySqlParameter("editedOn", comment.EditedOn));
                                insertComment.Parameters.Add(new MySqlParameter("time", comment.Time));

                                DBConnection.Instance().Open();
                                insertComment.ExecuteNonQuery();
                                DBConnection.Instance().Close();
                            }
                            else
                            {
                                MySqlCommand updateComment = new MySqlCommand("update_comment", DBConnection.Instance().Connection);
                                updateComment.CommandType = CommandType.StoredProcedure;

                                updateComment.Parameters.Add(new MySqlParameter("commentId", comment.CommentId));
                                updateComment.Parameters.Add(new MySqlParameter("commentText", comment.CommentText));
                                updateComment.Parameters.Add(new MySqlParameter("patientId", comment.PatientId));
                                updateComment.Parameters.Add(new MySqlParameter("userId", comment.UserId));
                                updateComment.Parameters.Add(new MySqlParameter("editedOn", comment.EditedOn));
                                updateComment.Parameters.Add(new MySqlParameter("time", comment.Time));                               

                                DBConnection.Instance().Open();
                                updateComment.ExecuteNonQuery();
                                DBConnection.Instance().Close();
                            }
                        }
                    }
                    foreach (Observation observation in Observation.GetObservations(ip, patient.PatientId))
                    {
                        int countObservation = 0;
                        MySqlCommand getObservations = new MySqlCommand("get_observation", DBConnection.Instance().Connection);
                        using (reader = getObservations.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (observation.ObservationId == reader["observationId"].ToString())
                                {
                                    countObservation++;
                                    break;
                                }
                            }
                            if (countObservation == 0)
                            {
                                MySqlCommand updateObservation = new MySqlCommand("insertinto_observation", DBConnection.Instance().Connection);
                                updateObservation.CommandType = CommandType.StoredProcedure;

                                updateObservation.Parameters.Add(new MySqlParameter("observationId", observation.ObservationId));
                                updateObservation.Parameters.Add(new MySqlParameter("durationInMinutes", observation.DurationInMinutes));
                                updateObservation.Parameters.Add(new MySqlParameter("patientId", observation.PatientId));
                                updateObservation.Parameters.Add(new MySqlParameter("time", observation.Time));
                                updateObservation.Parameters.Add(new MySqlParameter("userId", observation.UserId));

                                DBConnection.Instance().Open();
                                updateObservation.ExecuteNonQuery();
                                DBConnection.Instance().Close();
                            }
                            else
                            {
                                MySqlCommand updateObservation = new MySqlCommand("update_observation", DBConnection.Instance().Connection);
                                updateObservation.CommandType = CommandType.StoredProcedure;

                                updateObservation.Parameters.Add(new MySqlParameter("observationId", observation.ObservationId));
                                updateObservation.Parameters.Add(new MySqlParameter("durationInMinutes", observation.DurationInMinutes));
                                updateObservation.Parameters.Add(new MySqlParameter("patientId", observation.PatientId));
                                updateObservation.Parameters.Add(new MySqlParameter("time", observation.Time));
                                updateObservation.Parameters.Add(new MySqlParameter("userId", observation.UserId));                                

                                DBConnection.Instance().Open();
                                updateObservation.ExecuteNonQuery();
                                DBConnection.Instance().Close();
                            }
                        }
                    }

                    //check this
                    Statistic statistic = Statistic.GetStatistic(ip,patient.PatientId);
                    MySqlCommand updateStatistic = new MySqlCommand("update_statistic", DBConnection.Instance().Connection);
                    updateStatistic.CommandType = CommandType.StoredProcedure;
                    updateStatistic.Parameters.Add(new MySqlParameter("patientId", statistic.PatientId));
                    updateStatistic.Parameters.Add(new MySqlParameter("from", statistic.From));
                    updateStatistic.Parameters.Add(new MySqlParameter("to", statistic.To));
                    updateStatistic.Parameters.Add(new MySqlParameter("totalTimeInBed", statistic.TotalTimeInBed));
                    updateStatistic.Parameters.Add(new MySqlParameter("maxTimeWithoutMobility", statistic.MaxTimeWithoutMobility));
                    updateStatistic.Parameters.Add(new MySqlParameter("numberOfBedExits", statistic.NumberOfBedExits));
                    updateStatistic.Parameters.Add(new MySqlParameter("numberOfBedExitWarnings", statistic.NumberOfBedExitWarnings));
                    updateStatistic.Parameters.Add(new MySqlParameter("numberOfConfirmedBedExitWarnings", statistic.NumberOfConfirmedBedExitWarnings));
                    updateStatistic.Parameters.Add(new MySqlParameter("numberOfImmobilityWarnings", statistic.NumberOfImmobilityWarnings));
                    updateStatistic.Parameters.Add(new MySqlParameter("numberOfManualRegisteredRepositionings", statistic.NumberOfManualRegisteredRepositionings));
                    updateStatistic.Parameters.Add(new MySqlParameter("numberOfMovementsPerHour", statistic.NumberOfMovementsPerHour));
                    
                    DBConnection.Instance().Open();
                    updateStatistic.ExecuteNonQuery();
                    DBConnection.Instance().Close();
                }
            }
            foreach (Device device in Device.GetNewDeviceResponse(ip))
            {
                int countDevice = 0;
                MySqlCommand getDevices = new MySqlCommand("get_devices", DBConnection.Instance().Connection);
                using (reader = getDevices.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (device.DeviceId == reader["deviceId"].ToString())
                        {
                            countDevice++;
                            break;
                        }
                    }
                    if (countDevice == 0)
                    {
                        MySqlCommand updateDevice = new MySqlCommand("insertinto_device", DBConnection.Instance().Connection);
                        updateDevice.CommandType = CommandType.StoredProcedure;

                        updateDevice.Parameters.Add(new MySqlParameter("deviceId", device.DeviceId));
                        updateDevice.Parameters.Add(new MySqlParameter("assignedPatientId", device.AssignedPatientId));
                        updateDevice.Parameters.Add(new MySqlParameter("connectionQuality", device.ConnectionQuality));
                        updateDevice.Parameters.Add(new MySqlParameter("lastOnlineTime", device.LastOnlineTime));
                        updateDevice.Parameters.Add(new MySqlParameter("serialNumber", device.SerialNumber));

                        DBConnection.Instance().Open();
                        updateDevice.ExecuteNonQuery();
                        DBConnection.Instance().Close();
                    }
                
                    else
                    {
                        MySqlCommand updateDevice = new MySqlCommand("update_device", DBConnection.Instance().Connection);
                        updateDevice.CommandType = CommandType.StoredProcedure;

                        updateDevice.Parameters.Add(new MySqlParameter("deviceId", device.DeviceId));
                        updateDevice.Parameters.Add(new MySqlParameter("assignedPatientId", device.AssignedPatientId));
                        updateDevice.Parameters.Add(new MySqlParameter("connectionQuality", device.ConnectionQuality));
                        updateDevice.Parameters.Add(new MySqlParameter("lastOnlineTime", device.LastOnlineTime));
                        updateDevice.Parameters.Add(new MySqlParameter("serialNumber", device.SerialNumber));                        

                        DBConnection.Instance().Open();
                        updateDevice.ExecuteNonQuery();
                        DBConnection.Instance().Close();
                    }
                }

                Livestate livestate = Livestate.GetLivestate(ip,device.SerialNumber);
                MySqlCommand updateLivestate = new MySqlCommand("update_livestate", DBConnection.Instance().Connection);
                updateLivestate.CommandType = CommandType.StoredProcedure;
                updateLivestate.Parameters.Add(new MySqlParameter("deviceId", livestate.DeviceId));
                updateLivestate.Parameters.Add(new MySqlParameter("from", livestate.BedEmptyTimer));
                updateLivestate.Parameters.Add(new MySqlParameter("to", livestate.BedExitAlertSetting));
                updateLivestate.Parameters.Add(new MySqlParameter("totalTimeInBed", livestate.BedExitAlertTimer));
                updateLivestate.Parameters.Add(new MySqlParameter("maxTimeWithoutMobility", livestate.ControlSignal));
                updateLivestate.Parameters.Add(new MySqlParameter("numberOfBedExits", livestate.ImmobilityAlertSetting));
                updateLivestate.Parameters.Add(new MySqlParameter("numberOfBedExitWarnings", livestate.ImmobilityAlertTimer));
                updateLivestate.Parameters.Add(new MySqlParameter("numberOfConfirmedBedExitWarnings", livestate.SystemError));
                updateLivestate.Parameters.Add(new MySqlParameter("numberOfImmobilityWarnings", livestate.SystemErrorTimer));                

                DBConnection.Instance().Open();
                updateLivestate.ExecuteNonQuery();
                DBConnection.Instance().Close();
            }            
        }      
    }
}

