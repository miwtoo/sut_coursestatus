using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace sut_coursestatus
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] text = File.ReadAllLines(@"D:\Users\Documents\GitHub\sut_coursestatus\course.txt", Encoding.UTF8);
            foreach(string t in text)
            {
                GetCourseStatus("2562", "1", t);
            }
        }

        static private void GetCourseStatus(string year,string term, string course)
        {
            string htmlCode;
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("http://reg.sut.ac.th/registrar/class_info_1.asp?coursestatus=reg&facultyid=all&maxrow=100&Acadyear="+ year + "&Semester="+ term + "&CAMPUSID=1&LEVELID=1&coursecode="+ course);
            }
            //Console.WriteLine(htmlCode);
            Regex regex = new Regex(@"coursecode="+course);
            Match match = regex.Match(htmlCode);
            if (match.Success)
            {
                Console.WriteLine(course);
            }
        }

    }
}
