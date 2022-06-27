using System;
using System.Collections.Generic;
using System.Windows.Controls;
using ApplicationForBD.ApplicationDataBases;
namespace ApplicationForBD.Pages
{
    internal static class SaveElementFrame
    {
        public static Frame frame;
        public static Frame frameHub;
        public static Frame frameAdmin;
        public static ListView listViewAdminPanel = null;
        public static MainWindow Window { get; set; }

        public static string NameUser { get; set; }
        public static Client client { get; set; }
        public static string EmailUser { get; set; }
        public static int RoleUser { get; set; }
        public static string QueryString{ get; set; }
        public static string PasswordUser { get; set; }
        public static string SQLQuery { get; set; } = "";
        public static TextBlock NameTextBlock { get; set; } 
        public static TextBlock ReductionTextBlock { get; set; } 
        public static TextBlock EmailTextBlock { get; set; } 
        public static List<Service> listService = new List<Service>();

    }
}
