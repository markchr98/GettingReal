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

        public static void RunInsert()
        {
            MySqlDataReader reader;
            token.GetNewTokenResponse(ip);
            //insert departments. write check ifExists and update ifExists is true

            foreach(Department department in Department.GetNewDeparments(ip))
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
                            //midlertidig break, skal fjernes hvis der skal opdateres data
                            break;
                        }
                        if(countDepartment == 0)
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
                    }
                }
                //insert patients inside department loop because dep param is needed
                foreach(Patient patient in Patient.GetNewPatientResponse(ip, department.DepartmentId))
                {
                    int countPatient = 0;
                    MySqlCommand getPatients = new MySqlCommand("get_patients", DBConnection.Instance().Connection);
                    using(reader = getDepartments.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (patient.PatientId == reader["patientId"].ToString())
                            {
                                countPatient++;
                                //midlertidig break, skal fjernes hvis der skal opdateres data
                                break;
                            }
                            if(countPatient==0)
                            { 
                                MySqlCommand insertPatient = new MySqlCommand("insertinto_patient", DBConnection.Instance().Connection);
                                insertPatient.CommandType = CommandType.StoredProcedure;
                                insertPatient.Parameters.Add(new MySqlParameter("departmentId", patient.DepartmentId));
                                insertPatient.Parameters.Add(new MySqlParameter("patientId",patient.PatientId));
                                insertPatient.Parameters.Add(new MySqlParameter("patientNumber",patient.PatientNumber));
                                insertPatient.Parameters.Add(new MySqlParameter("firstname",patient.Firstname));
                                insertPatient.Parameters.Add(new MySqlParameter("lastname",patient.Lastname));
                                insertPatient.Parameters.Add(new MySqlParameter("birthDate", patient.BirthDate));
                                insertPatient.Parameters.Add(new MySqlParameter("entryDate",patient.EntryDate));
                                insertPatient.Parameters.Add(new MySqlParameter("dischargeDate", patient.DischargeDate));
                                insertPatient.Parameters.Add(new MySqlParameter("editedOn", patient.EditedOn));
                                insertPatient.Parameters.Add(new MySqlParameter("editedBy", patient.EditedBy));
                                DBConnection.Instance().Open();
                                insertPatient.ExecuteNonQuery();
                                DBConnection.Instance().Close();
                            }
                        }
                    }
                }
            }
            

            //do this in foreach dep
            //MySqlCommand insertDep = new MySqlCommand("insertinto_department", DBConnection.Instance().Connection);
            //insertDep.CommandType = CommandType.StoredProcedure;
            //insertDep.Parameters.Add(new MySqlParameter("departmentId", department.DepartmentId));
            //insertDep.Parameters.Add(new MySqlParameter("departmentName", department.DepartmentName));
            //DBConnection.Instance().Open();
            //insertDep.ExecuteNonQuery();
            //DBConnection.Instance().Close();



            //DBConnection.Instance().Open();
            //string cmdText = "INSERT INTO department VALUES (@departmentId, @name)";
            //MySqlCommand cmd = new MySqlCommand(cmdText, DBConnection.Instance().Connection);
            //cmd.Parameters.AddWithValue("@departmentId", "Test");
            //cmd.Parameters.AddWithValue("@name", "Test");
            //cmd.ExecuteNonQuery();
            //DBConnection.Instance().Close();

        }
        public static void RunUpdate()
        {

        }
    }
}

