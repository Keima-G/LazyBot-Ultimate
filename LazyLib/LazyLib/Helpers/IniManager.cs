namespace LazyLib.Helpers
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class IniManager
    {
        public string FilePath;

        public IniManager(string fileName)
        {
            if (!Directory.Exists(Directory.GetParent(fileName).FullName))
            {
                Directory.CreateDirectory(Directory.GetParent(fileName).FullName);
            }
            this.FilePath = fileName;
        }

        public bool GetBoolean(string section, string key, bool def)
        {
            try
            {
                return Convert.ToBoolean(this.IniReadValue(section, key));
            }
            catch
            {
                return def;
            }
        }

        public int GetInt(string section, string key, int def)
        {
            try
            {
                return Convert.ToInt32(this.IniReadValue(section, key));
            }
            catch
            {
                return def;
            }
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public string GetString(string section, string key, string def) => 
            !this.IniReadValue(section, key).Equals("") ? this.IniReadValue(section, key) : def;

        public string IniReadValue(string section, string key)
        {
            StringBuilder retVal = new StringBuilder(0xff);
            GetPrivateProfileString(section, key, "", retVal, 0xff, this.FilePath);
            return retVal.ToString();
        }

        public void IniWriteValue(string section, string key, bool value)
        {
            this.IniWriteValue(section, key, value.ToString());
        }

        public void IniWriteValue(string section, string key, double value)
        {
            this.IniWriteValue(section, key, value.ToString());
        }

        public void IniWriteValue(string section, string key, int value)
        {
            this.IniWriteValue(section, key, value.ToString());
        }

        public void IniWriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, this.FilePath);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}

