﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp5
{
    public static class ClassChangePage
    {
        public static Frame frame1 = new Frame();
    }
    public class GlobalVar
    {
        public static string PanelLogin;
        public static bool ThemeNegr = false;
        public static bool FlagToAction = true;
        public static List<DateTime> selectionChangedInitTimes = new List<DateTime>();
        public static List<string> selectionChangedInitData = new List<string>();
        public static List<string> selectionChangedInitFirstName = new List<string>();
        public static List<string> selectionChangedInitSurrName = new List<string>();
        public static List<string> selectionChangedInitLastName = new List<string>();
        public static List<string> selectionChangedInitRootPass = new List<string>();
        public static bool StatusAuth;
    }
}
