using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Getting_real
{
    class Comment
    {
        private string commentId;
        private string patientId;
        private string commentText;
        private string time;
        private string userId;
        private string editedOn;

        public string CommentId { get => commentId; set => commentId = value; }
        public string PatientId { get => patientId; set => patientId = value; }
        public string CommentText { get => commentText; set => commentText = value; }
        public string Time { get => time; set => time = value; }
        public string UserId { get => userId; set => userId = value; }
        public string EditedOn { get => editedOn; set => editedOn = value; }

        public static List<Comment> GetComments(string ip, string patientId)
        {
            using (WebClient client = new WebClient())
            {
                List<Comment> comments = new List<Comment>();
                string URL = "http://" + ip + ":5000/api/Patients/" + patientId + "/Comments";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + Controller.token.AccessToken;

                JArray response = JArray.Parse(client.DownloadString(URL));

                foreach (var comment in response.Children())
                {
                    comments.Add(new Comment()
                    {
                        commentId = (string)comment["commentId"],
                        patientId = (string)comment["patientId"],
                        commentText = (string)comment["commentText"],
                        time = (string)comment["time"],
                        userId = (string)comment["userId"],
                        editedOn = (string)comment["editedOn"]
                    });
                }
                return comments;
            }
        }
    }
}
