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
            Console.Write("Year : ");
            string year = Console.ReadLine();

            Console.Write("Term : ");
            string term = Console.ReadLine();

            Console.Write("File Location: ");
            string file = Console.ReadLine();

            string[] text = File.ReadAllLines(@file, Encoding.UTF8);
            foreach(string t in text)
            {
                GetCourseStatus(year, term, t);
            }
            Console.WriteLine("Succeed");
            Console.ReadLine();
        }

        static private void GetCourseStatus(string year,string term, string course)
        {
            string htmlCode;
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("http://reg.sut.ac.th/registrar/class_info_1.asp?coursestatus=reg&facultyid=all&maxrow=100&Acadyear="+ year + "&Semester="+ term + "&CAMPUSID=1&LEVELID=1&coursecode="+ course);
            }
            //Console.WriteLine(htmlCode);
            Regex regexName = new Regex(@"(?<="+course+ " - .<.A>&nbsp;<.TD><TD BGCOLOR=#F0F0F.><FONT SIZE=2>)(.+?)(?=<br>|<FONT SIZE=1 color=#407060>)"); 
            Regex regex = new Regex(@"coursecode="+course); //ถ้าใช้แบบบนหาอาจจะไม่เจอ (กันพลาด)
            Match match = regex.Match(htmlCode);
            if (match.Success)
            {
                match = regexName.Match(htmlCode);
                Console.WriteLine(course + " - "+ match.Value);
            }
        }

    }
}
