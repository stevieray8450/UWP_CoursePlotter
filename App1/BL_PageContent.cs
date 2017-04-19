using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace App1
{
    class BL_PageContent
    {
        // public properties used in both Faculty.xaml.cs and MainPage.xaml.cs
        // AboutCourse, FacultyMember, CreatedBy, used in Faculty.xaml.cs exclusively
        // VarOutput used in MainPage.xaml.cs exclusively
        public static string VarOutput { get; set; }
        public static string CreditsPrereq { get; set; }
        public static string CreatedBy { get; set; }
        public static string FacultyMember { get; set; }
        public static string CourseID { get; set; }
        public static string CourseCode { get; set; }
        public static string CourseTitle { get; set; }
        public static string AboutCourse { get; set; }

        public static void Course1()
        {
            VarOutput = "";
            // storing course titles and catalog codes in an array
            string[] names = new string[3] { "COP3288C", "UWP1", "Universal Windows Application Programming I" };
            // using each element in the array in order to assign them to the CourseID, CourseCode, and CourseTitle properties.
            CourseID = names[0];
            CourseCode = names[1];
            CourseTitle = names[2];

            // storing prerequisite courses in an array
            string[] prereqs = new string[2] { "Enterprise Architecture", "Distributed Application Architecture" };
            AboutCourse = "This course provides students an introduction to the basic features of the Microsoft C# programming language as " +
                "it applies to Universal Windows Application mobile application development. Students will review the history, features, and " +
                "advantages of the C# programming language, utilize the Visual Studio programming environment, demonstrate a mastery of C# " +
                "programming basics, and develop a basic Universal Windows Application.";
            FacultyMember = "Syed Nabeel";

            string credits = "4";

            for (int i = 0; i < names.Length; i++)
            {
                if (VarOutput.Equals(""))
                {
                    VarOutput = names[i];
                }
                else
                {
                    VarOutput = VarOutput + "\n" + names[i];
                }
            }

            // calls the CourseCredits method, passing in the course's credits and prerequisities.
            // assigns the returning value to the public CreditsPrereq property.
            CreditsPrereq = CourseCredits(credits, prereqs);

        }

        public static void Course2()
        {
            VarOutput = "";
            // storing course titles and catalog codes in an array
            string[] names = new string[3] { "COP4474C", "UWP2", "Universal Windows Application Development II" };
            // using each element in the array in order to assign them to the CourseID, CourseCode, and CourseTitle properties.
            CourseID = names[0];
            CourseCode = names[1];
            CourseTitle = names[2];

            // storing prerequisite courses in an array
            string[] prereqs = new string[1] { "Universal Windows Application Programming I" };
            AboutCourse = "This course presents advanced application design and Microsoft C# programming techniques related to Universal Windows " +
                "Application development. Students will analyze user interface design and the Windows features that support it, demonstrate a mastery " +
                "of Microsoft user interface tools, construct a C# database application, and develop a basic C# mobile application that accesses Microsoft " +
                "Azure.";
            FacultyMember = "Cynthia Moonhowler";

            string credits = "4";

            for (int i = 0; i < names.Length; i++)
            {
                if (VarOutput.Equals(""))
                {
                    VarOutput = names[i];
                }
                else
                {
                    VarOutput = VarOutput + "\n" + names[i];
                }
            }

            // calls the CourseCredits method, passing in the course's credits and prerequisities.
            // assigns the returning value to the public CreditsPrereq property.
            CreditsPrereq = CourseCredits(credits, prereqs);
        }

        public static void Course3()
        {
            VarOutput = "";
            // storing course titles and catalog codes in an array
            string[] names = new string[3] { "COP4777C", "UWCI", "Universal Windows Application Cloud Integration" };
            // using each element in the array in order to assign them to the CourseID, CourseCode, and CourseTitle properties.
            CourseID = names[0];
            CourseCode = names[1];
            CourseTitle = names[2];

            // storing prerequisite courses in an array
            string[] prereqs = new string[1] { "Universal Windows Application Programming II" };
            AboutCourse = "This course focuses on the development of Universal Windows mobile applications that access cloud computing resources. " +
                "Students will explore the software development kits (SDKs) available from commercial cloud vendors, demonstrate a mastery of the " +
                "Amazon Web Services Mobile SDK, demonstrate a mastery of the Microsoft Windows Azure Mobile Services SDK, and incorporate AWS or Azure " +
                "functionality into a working Universal Windows mobile application.";
            FacultyMember = "Shad McDillek";

            string credits = "4";

            for (int i = 0; i < names.Length; i++)
            {
                if (VarOutput.Equals(""))
                {
                    VarOutput = names[i];
                }
                else
                {
                    VarOutput = VarOutput + "\n" + names[i];
                }
            }

            // calls the CourseCredits method, passing in the course's credits and prerequisities.
            // assigns the returning value to the public CreditsPrereq property.
            CreditsPrereq = CourseCredits(credits, prereqs);
        }

        // method for formatting a string. Takes in the
        // credits for a particular course and their prerequisites
        public static string CourseCredits(string credits, string[] prereqs)
        {
            string prereqString = "";
                foreach (var item in prereqs)
                {
                    if (prereqs.Length > 1)
                    {
                        // separating multiple prerequisite courses (if any) with a comma 
                        // ????? how to not include the comma after the last element ?????
                        prereqString = prereqString + item + ", ";
                    }
                    else
                    {
                        // if there's only one prerequisite course
                        prereqString = item;
                    }
                }
            return ("Course's credit hours: " + credits + "\n" +
                "Course Prerequisites: " + prereqString);
        }

    } 
}      
           
